using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    public bool isMoving = true;
    public char myCol;
    public Vector2 mapPos;
    private SpriteRenderer sprite;
    private GameManager GM;
    private Map mp;
    // Use this for initialization

    void Awake()
    {
        GM = GameObject.Find("Main Camera").GetComponent<GameManager>();
        mp = GameObject.Find("Main Camera").GetComponent<Map>();
        sprite = transform.GetComponent<SpriteRenderer>();
        transform.localScale = new Vector3(0.78f, 0.97f, 1);
        isMoving = true;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStucked() && !isMoving)
        {
            startMove();
        }

        if (isStucked() && isMoving)
        {
            stopMove();
        }
    }

   public bool isValidPos(Vector2 vec)
    {
        vec = mapPos + Map.roundVec2(vec);
        if (!mp.isInside(vec))
                return false;
        else if (mp.map[(int)vec.x, (int)vec.y] != null && mp.map[(int)vec.x, (int)vec.y].transform.parent != transform.parent)
                return false;
        return true;
    }

    public bool isStucked()
    {
        Vector2 vec = Map.roundVec2(mapPos);
        if ((int)vec.y == 0)
            return true;
        else if (mp.map[(int)vec.x, (int)vec.y - 1] != null && !(mp.map[(int)vec.x, (int)vec.y - 1].GetComponent<Stone>().isMoving))
            return true;
        return false;
    }

    public void startMove()
    {
        isMoving = true;
        StartCoroutine("fall");
    }

    public void Move(Vector2 vec)
    {
        vec = Map.roundVec2(vec);
        if (isValidPos(vec))
        {
            mp.updateStone(gameObject, mapPos + vec);
        }
    }

    public void stopMove()
    {
        isMoving = false;
        StopCoroutine("fall");
    }

    public void checkMatch()
    {
        GM.matchQueue.Add(this);
        Vector2 tempVec;
        Stone tempStone;
        for (int i = 0; i < 4; i++)
        {
            tempVec = mapPos + Map.rotate(Vector2.up, (-90f) * i);
            tempVec = Map.roundVec2(tempVec);
            //Debug.Log(Map.rotate(Vector2.up, (-90f) * i));
            Debug.Log(mapPos + "+" + Map.rotate(Vector2.up, (-90f) * i) + " = " + (int)tempVec.y);
            if (!mp.isInside(tempVec) || mp.map[(int)tempVec.x, (int)tempVec.y] == null) continue;
            tempStone = mp.map[(int)tempVec.x, (int)tempVec.y].GetComponent<Stone>();
            if (tempStone.myCol != myCol) Debug.Log(tempStone.mapPos + "NULL!!" + tempStone.myCol + myCol);
            if (!tempStone.isMoving && tempStone.myCol == myCol && !GM.matchQueue.Contains(tempStone))
            {
                tempStone.checkMatch();
            }
        }
        return;
    }

    public void changeAppear(char col)
    {
        myCol = col;
        sprite.sprite = Resources.Load<Sprite>("Images/Stone_" + col) as Sprite;
    }


    public void destroy()
    {
        changeAppear('g');
        mp.deleteStone(gameObject);
        SoundManager.get("stone").Play();
        Destroy(gameObject);
    }

    IEnumerator fall()
    {
        while (true)
        {
            yield return new WaitForSeconds(GM.fallDelay*0.2f);
            Move(Vector2.down);

        }
    }

}
