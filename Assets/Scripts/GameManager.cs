using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public float spawnPointX;
    public float spawnPointY;
    public float fallDelay;
    public string[] spawnArray;
    public Queue<string> spawnQueue = new Queue<string> { };
    public List<Stone> matchQueue = new List<Stone>{ };
    public StaticCoroutine.func stopMoves;
    private GameObject stoneSet;
    private bool isSuccess = false;
    private Map mp;
    // Use this for initialization
    void awake()
    {
        Screen.SetResolution(Screen.width, Screen.width * (16 / 9),true);
    }
    void Start ()
    {
        stopMoves = () => { };
        mp = gameObject.GetComponent<Map>();
        foreach (string str in spawnArray)
        {
            spawnQueue.Enqueue(str);
        }
        spawnNext();
        StartCoroutine("checkRoutine");
    }
	
	// Update is called once per frame
	void Update ()
    {
        for (int i = 0; i < mp.Width; i++)
        {
            if (mp.map[i, mp.successH] == null) continue;
            else if (!mp.map[i, mp.successH].GetComponent<Stone>().isMoving)
            {
                Debug.Log("GAME OVER");
                success(i);
            }
        }
    }

    public void spawnNext()
    {

        if (spawnQueue.Count == 0 || isSuccess) return;
        string next = spawnQueue.Dequeue();
        GameObject prefab = Resources.Load("Prefabs/StoneSet" + next) as GameObject;
        stoneSet = Instantiate(prefab, new Vector3(spawnPointX, spawnPointY, 0), Quaternion.identity);
    }

    public void doNext()
    {
        StaticCoroutine.DoCoroutine(spawnNext, 0.5f);
    }

    public void clicked(string str) {
        if (stoneSet == null) return;
        Debug.Log("clicked");
        stoneSet.GetComponent<StoneSet>().clicked(str);
    }

    public void success(int successX)
    {
        isSuccess = true;
        stopMoves();
        StopCoroutine("checkRoutine");
        for (int y = 12; y < mp.Height; y++)
        {
            for (int x = 0; x < mp.Width; x++)
            {
                if (mp.map[x, y] != null)
                {
                    mp.map[x, y].GetComponent<Stone>().destroy();
                    mp.map[x, y] = null;
                }
            }
        }
    }

    public void checkMatchs() {
        for (int y = 0; y < mp.successH; y++)
        {
            for (int x = 0; x < mp.Width; x++)
            {
                if (mp.map[x, y] == null) continue;
                else if (mp.map[x, y].GetComponent<Stone>().isMoving) continue;
                mp.map[x, y].GetComponent<Stone>().checkMatch();
                if (matchQueue.Count >= 5)
                {
                    foreach (Stone stone in matchQueue)
                    {
                        stone.destroy();
                    }
                }
                matchQueue.Clear();
            }
        }
    }

    IEnumerator checkRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            checkMatchs();
        }

    }
}
