using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public float spawnPointX;
    public float spawnPointY;
    public float fallDelay;
    public string[] spawnArray;
    public int stageNum;
    public Queue<string> spawnQueue = new Queue<string> { };
    public List<Stone> matchQueue = new List<Stone>{ };
    public StaticCoroutine.func stopMoves;
    private GameObject stoneSet;
    private bool isSuccess = false;
    private Map mp;
    private GameObject win;
    private GameObject loose;
    // Use this for initialization
    void awake ()
    {
        

    }
    void Start ()
    {
        win = GameObject.Find("Canvas").transform.Find("Win").gameObject;
        loose = GameObject.Find("Canvas").transform.Find("Loose").gameObject;
        stopMoves = () => { };
        mp = gameObject.GetComponent<Map>();

    }
	
	// Update is called once per frame
	void Update ()
    {

    }

    public void startGame()
    {
        foreach (KeyValuePair<string, string> dic in StageData.Instance.stageData[stageNum.ToString()])
        {
            if (!dic.Value.Equals("-1") && !dic.Value.Equals("")) spawnQueue.Enqueue(dic.Value);
        }
        Debug.Log(spawnQueue.Count);
        spawnNext();
        StartCoroutine("checkRoutine");
    }

    public void spawnNext()
    {
        for (int i = 0; i < mp.Width; i++)
        {
            if (mp.map[i, mp.successH] == null) continue;
            else if (!mp.map[i, mp.successH].GetComponent<Stone>().isMoving)
            {
                Debug.Log("GAME OVER");
                success(i);
                return;
            }
        }
        if (spawnQueue.Count == 0 && !isSuccess)
        {
            Loose();
            return;
        }
        string block = spawnQueue.Dequeue();
        string name = block.Substring(0, 1);
        string colors = block.Substring(2);
        GameObject prefab = Resources.Load("Prefabs/StoneSet" + name) as GameObject;
        stoneSet = Instantiate(prefab, new Vector3(spawnPointX, spawnPointY, 2), Quaternion.identity);
        for (int i = 0; i < prefab.transform.childCount; i++)
        {
            stoneSet.transform.GetChild(i).GetComponent<Stone>().changeAppear(colors[i]);
        }
    }

    public void Loose()
    {
        loose.SetActive(true);
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
        win.SetActive(true);
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
