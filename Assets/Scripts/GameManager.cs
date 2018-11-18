using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public float spawnPointX;
    public float spawnPointY;
    public float fallDelay;
    public float visualPointX;
    public float visualPointY;
    public int stageNum;
    public int maxMatchCount;
    public Queue<string> spawnQueue = new Queue<string> { };
    public List<Stone> matchQueue = new List<Stone>{ };
    public StaticCoroutine.func stopMoves;
    private GameObject stoneSet;
    private bool isSuccess = false;
    private Map mp;
    private GameObject win;
    private GameObject loose;
    public GameObject alpaca;
    private bool camMove;
    private GameObject UI;
    private string next;
    private GameObject nextStone1;
    private GameObject nextStone2;
    private GameObject nextPanel;
    // Use this for initialization
    void OnEnable ()
    {
        SoundManager.get("main").Play();
        foreach (KeyValuePair<string, string> dic in StageData.Instance.stageData[stageNum.ToString()])
        {
            if (!dic.Value.Equals("-1") && !dic.Value.Equals("")) spawnQueue.Enqueue(dic.Value);
        }
        Debug.Log(stageNum+"스테이지임");
        stopMoves = () => { };
        if(spawnQueue.Count != 0)
            next = spawnQueue.Dequeue();
        spawnNext();
        //StartCoroutine("checkRoutine");
        camMove = false;
    }
    void Awake ()
    {
        win = GameObject.Find("Canvas").transform.Find("Win").gameObject;
        loose = GameObject.Find("Canvas").transform.Find("Loose").gameObject;
        mp = gameObject.GetComponent<Map>();
        UI = GameObject.Find("Canvas").transform.Find("UI").gameObject;
        nextPanel = GameObject.Find("NextPanel").gameObject;

    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (camMove)
        {
            if (gameObject.transform.position.y < 7.5f)
                gameObject.transform.Translate(Vector3.up * Time.fixedDeltaTime * (2.5f / 0.8f));
            else
            {
                camMove = false;
                alpaca.SetActive(true);
            }
        }
        
    }

    public void startGame()
    {

    }

    public void spawnNext()
    {
        for (int i = 0; i < mp.Width; i++)
        {
            if (mp.map[i, mp.successH] == null) continue;
            else if (!mp.map[i, mp.successH].GetComponent<Stone>().isMoving)
            {
                Debug.Log("GAME OVER");
                success(mp.map[i, 0].transform.position);
                return;
            }
        }
        if (spawnQueue.Count == 0 && next == null &&!isSuccess)
        {
            Loose();
            return;
        }
        string block = next;
        if(spawnQueue.Count != 0)
            next = spawnQueue.Dequeue();
        else
            next = null;
        visualNext();
        string name = block.Substring(0, 1);
        string colors = block.Substring(2);
        GameObject prefab = Resources.Load("Prefabs/StoneSet" + name) as GameObject;
        stoneSet = Instantiate(prefab, new Vector3(spawnPointX, spawnPointY, 2), Quaternion.identity);
        for (int i = 0; i < colors.Length; i++)
        {
            stoneSet.transform.GetChild(i).GetComponent<Stone>().changeAppear(colors[i]);
        }
    }

    public bool isAllStop() {
        for (int y = 0; y < mp.successH; y++)
        {
            for (int x = 0; x < mp.Width; x++)
            {
                if (mp.map[x, y] == null) continue;
                else if (mp.map[x, y].GetComponent<Stone>().isMoving) return false;
            }
        }
        return true;
    }

    public void visualNext()
    {
        Destroy(nextStone1);
        Destroy(nextStone2);
        if (next == null)
        {
            GameObject.Find("Remain").GetComponent<Text>().text = "남은 돌 : " + (spawnQueue.Count);
            return;
        }
        else GameObject.Find("Remain") .GetComponent<Text>().text = "남은 돌 : " + (spawnQueue.Count + 1);
        string name = next.Substring(0, 1);
        string colors = next.Substring(2);
        GameObject prefab = Resources.Load("Prefabs/StoneSet" + name) as GameObject;
        nextStone1 = Instantiate(prefab, new Vector3(visualPointX, visualPointY, -1), Quaternion.identity);
        nextStone1.transform.localScale = Vector3.one * 0.58f;
        for (int i = 0; i < colors.Length; i++)
        {
            nextStone1.transform.GetChild(i).GetComponent<Stone>().changeAppear(colors[i]);
            Destroy(nextStone1.transform.GetChild(i).GetComponent<Stone>());
        }
        Destroy(nextStone1.GetComponent<StoneSet>());
        if (spawnQueue.Count != 0)
        {
            name = spawnQueue.Peek().Substring(0, 1);
            colors = spawnQueue.Peek().Substring(2);
            prefab = Resources.Load("Prefabs/StoneSet" + name) as GameObject;
            nextStone2 = Instantiate(prefab, new Vector3(visualPointX + 2.9f, visualPointY, -1), Quaternion.identity);
            nextStone2.transform.localScale = Vector3.one * 0.58f;
            for (int i = 0; i < colors.Length; i++)
            {
                nextStone2.transform.GetChild(i).GetComponent<Stone>().changeAppear(colors[i]);
                Destroy(nextStone2.transform.GetChild(i).GetComponent<Stone>());
            }
            Destroy(nextStone2.GetComponent<StoneSet>());
        }
    }

    public void Loose()
    {
        loose.SetActive(true);
        SoundManager.get("main").Stop();
        SoundManager.get("Stage fail").Play();
        //StopCoroutine("checkRoutine");
    }

    public void doNext()
    {
        StartCoroutine("doNextRoutine");
    }

    public void clicked(string str) 
    {
        SoundManager.get("touch").Play();
        if (stoneSet == null) return;
        Debug.Log("clicked");
        stoneSet.GetComponent<StoneSet>().clicked(str);
    }

    public void success(Vector3 successPos)
    {
        isSuccess = true;
        SoundManager.get("main").Stop();
        SoundManager.get("Stage win").Play();
        Destroy(nextStone1);
        Destroy(nextStone2);
        stopMoves();
        //StopCoroutine("checkRoutine");
        camMove = true;
        alpaca.GetComponent<MainCharacter>().successPos = successPos;
        UI.SetActive(false);
        nextPanel.SetActive(false);
    }

    public void checkMatchs() {
        matchQueue.Clear();
        for (int y = 0; y < mp.successH; y++)
        {
            for (int x = 0; x < mp.Width; x++)
            {
                if (mp.map[x, y] == null) continue;
                else if (mp.map[x, y].GetComponent<Stone>().isMoving) continue;
                mp.map[x, y].GetComponent<Stone>().checkMatch();
                if (matchQueue.Count >= maxMatchCount)
                {
                    Debug.Log(matchQueue.Count);
                    StartCoroutine("destroyRoutine", matchQueue);
                    matchQueue.Clear();
                    return;
                }
                matchQueue.Clear();
            }
        }
        Invoke("spawnNext", 0.3f);
    }

    public void stageReset()
    {
        for (int y = 0; y < mp.Height; ++y)
            for (int x = 0; x < mp.Width; ++x)
                if (mp.map[x, y] != null)
                {
                    Destroy(mp.map[x, y]);
                    mp.map[x, y] = null;
                }
        SoundManager.get("main").Stop();
        Destroy(stoneSet);
        spawnQueue.Clear();
        Destroy(nextStone1);
        Destroy(nextStone2);
        isSuccess = false;
        matchQueue.Clear();
        stopMoves = () => { };
        transform.position = new Vector3(transform.position.x, 5.5f, transform.position.z);
        alpaca.GetComponent<MainCharacter>().reset();
        UI.SetActive(true);
        nextPanel.SetActive(true);
        UI.transform.Find("Remain").GetComponent<Text>().text = "남은 돌 : ";
        this.enabled = false;
    }

    IEnumerator doNextRoutine()
    {
        while (!isAllStop())
        {
            yield return new WaitForSeconds(0.1f);
        }
        checkMatchs();
    }


    IEnumerator destroyRoutine(List<Stone> matchQueue)
    {
        List<Stone> destroyQueue = new List<Stone>(matchQueue);
        foreach (Stone st in destroyQueue)
        {
            st.changeAppear('g');
        }
        yield return new WaitForSeconds(0.5f);
        foreach (Stone stone in destroyQueue)
        {
            stone.destroy();
        }
        doNext();
    }
}
