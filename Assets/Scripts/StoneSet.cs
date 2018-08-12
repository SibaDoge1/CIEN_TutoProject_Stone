using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneSet : MonoBehaviour
{
    private Transform[] children;
    private GameManager GM;
    private Map mp;

    // Use this for initialization

    void Awake()
    {
    }

    void Start()
    {
        children = gameObject.GetComponentsInChildren<Transform>();
        mp = GameObject.Find("Main Camera").GetComponent<Map>();
        GM = GameObject.Find("Main Camera").GetComponent<GameManager>();
        updateMap();
        posRoundSet();
        StartCoroutine("fall");
    }

    // Update is called once per frame
    void Update()
    {
        if (isStuckedSet())
        {
            Debug.Log("stop");
            stopMove();
        }
    }

    public void clicked(string str)
    {
        switch (str)
        {
            case "left": Move("left"); break;
            case "right": Move("right"); break;
            case "down": Move("down"); break;
            case "up":
                if (gameObject.CompareTag("Ostone"))
                    transform.RotateAround(transform.Find("rotate").transform.position, Vector3.forward, -90f);
                else
                    transform.Rotate(0, 0, -90);
                if (isValidPosSet())
                    updateMap();
                else
                {
                    if (gameObject.CompareTag("Ostone"))
                        transform.RotateAround(transform.Find("rotate").transform.position, Vector3.forward, 90f);
                    transform.Rotate(0, 0, 90);
                }
                break;
        }
    }

    public bool isValidPosSet()
    {
        foreach (Transform child in children)
        {
            if (child.gameObject == gameObject || !child.CompareTag("Stone"))
                continue;
            if (!(child.gameObject.GetComponent<Stone>().isValidPos()))
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
            case "down": vec = Vector2.down; break;
        }
        transform.position += (Vector3)vec;
        if (isValidPosSet())
        {
            updateMap();
        }
        else
        {
            Debug.Log("not");
            transform.position -= (Vector3)vec;
        }
        posRoundSet();
    }

    public void posRoundSet()
    {
        Vector2 origin = mp.roundVec2(transform.position);
        transform.position = origin;
        foreach (Transform child in children)
        {
            if (child.gameObject == gameObject || !child.CompareTag("Stone"))
                continue;
            child.gameObject.GetComponent<Stone>().posRound();
        }
    }

    public void updateMap()
    {
        foreach (Transform child in children)
        {
            if (child.gameObject == gameObject || !child.CompareTag("Stone"))
                continue;
            mp.updateStone(child.gameObject);
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
