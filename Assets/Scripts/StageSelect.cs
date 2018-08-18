using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelect : MonoBehaviour {

    public int stageNum = 1;
    private Text numText;
    private GameManager GM;
    // Use this for initialization
    void Awake()
    {
        numText = transform.Find("Num").GetComponent<Text>();
        GM = GameObject.Find("Main Camera").GetComponent<GameManager>();
    }
    void OnEnable()
    {
        SoundManager.get("main start").Play();
    }
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        numText.text = stageNum.ToString();
    }

    public void up()
    {
        SoundManager.get("touch").Play();
        if (stageNum < 10) stageNum++;
    }
    public void down()
    {
        SoundManager.get("touch").Play();
        if (stageNum > 1) stageNum--;
    }
    public void start()
    {
        SoundManager.get("touch").Play();
        SoundManager.get("main start").Stop();
        GM.stageNum = stageNum;
        GM.enabled = true;
        GM.startGame();

        gameObject.SetActive(false);

    }
}
