using System.Collections;
using UnityEngine;

public class Stone : MonoBehaviour
{
    public bool isMoving = true;
    public char myCol;
    public Vector2 mapPos;
    private SpriteRenderer sprite;
    // Use this for initialization

    void Awake()
    {
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
        vec = mapPos + Vec2Math.roundVec2(vec);
        if (!Map.Instance.isInside(vec))
                return false;
        else if (Map.Instance.getStone(vec) != null && Map.Instance.getStone(vec).transform.parent != transform.parent)
                return false;
        return true;
    }

    public bool isStucked()
    {
        Vector2 vec = Vec2Math.roundVec2(mapPos);
        if ((int)vec.y == 0)
            return true;
        else if (Map.Instance.getStone(vec + Vector2.down) != null && !(Map.Instance.getStone(vec + Vector2.down).GetComponent<Stone>().isMoving))
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
        vec = Vec2Math.roundVec2(vec);
        if (isValidPos(vec))
        {
            Map.Instance.updateStone(this, mapPos + vec);
        }
    }

    public void stopMove()
    {
        isMoving = false;
        StopCoroutine("fall");
    }

    public void checkMatch()
    {
        StageManager.Instance.matchQueue.Add(this);
        Vector2 tempVec;
        Stone tempStone;
        for (int i = 0; i < 4; i++)
        {
            tempVec = Vec2Math.roundVec2(mapPos + Vec2Math.rotate(Vector2.up, (-90f) * i));
            tempStone = Map.Instance.getStone(tempVec);
            if (Map.Instance.getStone(tempVec) == null) continue;
            if (!tempStone.isMoving && tempStone.myCol == myCol && !StageManager.Instance.matchQueue.Contains(tempStone))
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
        Map.Instance.deleteStone(this);
        SoundManager.get("stone").Play();
        Destroy(gameObject);
    }

    IEnumerator fall()
    {
        while (true)
        {
            yield return new WaitForSeconds(StageManager.Instance.fallDelay*0.1f);
            Move(Vector2.down);

        }
    }

}
