using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{

    public bool isMoving;
    private char _color;
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
        if (isStucked() && isMoving)
        {
            Debug.Log("stop");
            stopMove();
        }
        if (!isStucked() && !isMoving)
        {
            startMove();
        }
    }

   public bool isValidPos()
    {
        Vector2 vec = mp.roundVec2(transform.position);
        if (!mp.isInside(vec))
                return false;
        else if (mp.map[(int)vec.x, (int)vec.y] != null && mp.map[(int)vec.x, (int)vec.y].transform.parent != transform.parent)
                return false;
        return true;
    }

    public bool isStucked()
    {
        Vector2 vec = mp.roundVec2(transform.position);
        if (vec.y <= 0)
            return true;
        else if (mp.map[(int)vec.x, (int)vec.y - 1] != null && !(mp.map[(int)vec.x, (int)vec.y - 1].GetComponent<Stone>().isMoving))
            return true;
        return false;
    }

    public void startMove()
    {
        GM.stopMoves += stopMove;
        StartCoroutine("fall");
        isMoving = true;
    }

    public void Move(string dir)
    {
        Vector2 vec = Vector2.zero;
        switch (dir)
        {
            case "left": vec = Vector2.left; break;
            case "right": vec = Vector2.right; break;
            case "down": vec = Vector2.down; break;
        }
        transform.position += (Vector3)vec;
        if (isValidPos())
        {
            mp.updateStone(gameObject);
            if (isStucked())
            {
                Debug.Log("stop");
                stopMove();
            }
        }
        else
        {
            Debug.Log("not");
            transform.position -= (Vector3)vec;
        }
        posRound();
    }

    public void posRound()
    {
        Vector2 origin = mp.roundVec2(transform.position);
        transform.position = origin;
    }

    public void stopMove()
    {
        isMoving = false;
        GM.stopMoves -= stopMove;
        StopCoroutine("fall");
    }

    public void checkMatch()
    {
        GM.matchQueue.Add(this);
        Vector2 tempVec;
        Stone tempStone;
        for (int i = 0; i < 4; i++)
        {
            tempVec = mapPos + mp.rotate(Vector2.up, (-90f) * i);
            if (!mp.isInside(tempVec) || mp.map[(int)tempVec.x, (int)tempVec.y] == null) continue;
            tempStone = mp.map[(int)tempVec.x, (int)tempVec.y].GetComponent<Stone>();
            if (!tempStone.isMoving && tempStone._color == _color && !GM.matchQueue.Contains(tempStone))
            {
                tempStone.checkMatch();
            }
        }
        return;
    }

    public void changeAppear(char col)
    {
        _color = col;
        sprite.sprite = Resources.Load<Sprite>("Images/Stone_" + col) as Sprite;
    }


    public void destroy()
    {
        changeAppear('g');
        mp.deleteStone(gameObject);
        SoundManager.get("stone").Play();
        Destroy(gameObject, 0.5f);
    }

    IEnumerator fall()
    {
        while (true)
        {
            yield return new WaitForSeconds(GM.fallDelay*0.1f);
            Debug.Log("move");
            Move("down");
            mp.printMap();

        }
    }

}
