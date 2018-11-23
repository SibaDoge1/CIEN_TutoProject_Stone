using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneSet : MonoBehaviour
{
    public enum stoneType { I, J, L, O, S, Z, T };
    protected Transform[] children;
    protected GameManager GM;
    protected Map mp;
    protected int degree;
    protected int[,,] rotationData = new int[4, 4, 2];
    protected int[,,] wallKickData = new int[4, 5, 2]
        {
            {{0,0},{-1,0},{-1,1},{0,-2},{-1,-2}},
            {{0,0},{1,0},{1,-1},{0,2},{1,2}},
            {{0,0},{1,0},{1,1},{0,-2},{1,-2}},
            {{0,0},{-1,0},{-1,-1},{0,2},{-1,2}}
        };
    // Use this for initialization

    protected virtual void Awake()
    {
        children = gameObject.GetComponentsInChildren<Transform>();
        mp = GameObject.Find("Main Camera").GetComponent<Map>();
        GM = GameObject.Find("Main Camera").GetComponent<GameManager>();
        degree = 0;
    }

    void Start()
    {
        firstSet();
        StartCoroutine("fall");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void clicked(string str)
    {
        switch (str)
        {
            case "left": Move("left"); break;
            case "right": Move("right"); break;
            case "down": resetFall(); Move("down");  break;
            case "up": rotate(); break;
        }
    }

    public virtual void rotate()
    {
        bool isValid = true;
        for (int _case = 0; _case < 5; _case++)
        {
            isValid = true;
            for (int idx = 0; idx < 4; idx++)
            {
                Vector2 vec = new Vector2(rotationData[degree, idx, 0] + wallKickData[degree, _case, 0], rotationData[degree, idx, 1] + wallKickData[degree, _case, 1]);
                if (!transform.GetChild(idx).GetComponent<Stone>().isValidPos(vec)) isValid = false;
            }
            for (int idx = 0; idx < 4; idx++)
            {
                Vector2 vec = new Vector2(rotationData[degree, idx, 0] + wallKickData[degree, _case, 0], rotationData[degree, idx, 1] + wallKickData[degree, _case, 1]);
                if (isValid) transform.GetChild(idx).GetComponent<Stone>().Move(vec);
            }
            if (isValid)
            {
                degree = (degree + 1) % 4; break;
            }
        }
    }

    public void resetFall()
    {
        StopCoroutine("fall");
        StartCoroutine("fall");
    }

    public bool isValidPosSet(Vector2 vec)
    {
        foreach (Transform child in children)
        {
            if (child.gameObject == gameObject || !child.CompareTag("Stone"))
                continue;
            if (!(child.gameObject.GetComponent<Stone>().isValidPos(vec)))
                return false;
        }
        return true;
    }

    public bool isStuckedSet()
    {
        foreach (Transform child in children)
        {
            if (child.gameObject == gameObject || !child.CompareTag("Stone"))
                continue;
            if ((child.gameObject.GetComponent<Stone>().isStucked()))
                return true;
        }
        return false;
    }

    public void Move(string dir)
    {
        Vector2 vec = Vector2.zero;
        switch (dir)
        {
            case "left": vec = Vector2.left; break;
            case "right": vec = Vector2.right; break;
            case "down":
                vec = Vector2.down;
                if (isStuckedSet())
                {
                    Debug.Log("stop");
                    stopMove();
                    return;
                }
                break;
        }
        if (!isValidPosSet(vec)) return;
        Debug.Log("valid");
        foreach (Transform child in children)
        {
            if (child.gameObject == gameObject || !child.CompareTag("Stone"))
                continue;
            child.gameObject.GetComponent<Stone>().Move(vec);
        }
    }

    public void firstSet()
    {
        foreach (Transform child in children)
        {
            if (child.gameObject == gameObject || !child.CompareTag("Stone"))
                continue;
            child.transform.GetComponent<Stone>().mapPos = new Vector2(-1, -1);
            mp.updateStone(child.gameObject, Map.roundVec2(child.transform.position));
        }
    }
    public void stopMove()
    {
        StopCoroutine("fall");
        foreach (Transform child in children)
        {
            if (child.gameObject == gameObject || !child.CompareTag("Stone"))
                continue;
            child.gameObject.GetComponent<Stone>().startMove();
            child.parent = null;
        }
        GM.doNext();
        Destroy(gameObject);
    }


    IEnumerator fall()
    {
        while (true)
        {
            yield return new WaitForSeconds(GM.fallDelay);
            Debug.Log("move");
            Move("down");

        }
    }

}
