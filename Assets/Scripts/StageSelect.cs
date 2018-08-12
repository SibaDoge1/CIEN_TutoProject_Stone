using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelect : MonoBehaviour {

    private int stageNum = 1;
    private Text numText;
    private GameManager GM;
    // Use this for initialization
    void Awake()
    {
        Time.timeScale = 0;
    }
    void Start () {
        numText = transform.Find("Num").GetComponent<Text>();
        GM = GameObject.Find("Main Camera").GetComponent<GameManager>();
    }
	
	// Update is called once per frame
	void Update () {
        numText.text = stageNum.ToString();
    }

    public void up()
    {
        if(stageNum < 10) stageNum++;
    }
    public void down()
    {
        if (stageNum > 1) stageNum--;
    }
    public void start()
    {
        GM.stageNum = stageNum;
        GM.startGame();
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
}
