using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : MonoBehaviour {
    private GameManager GM;
    private GameObject panel;
    private GameObject Option;

    void Awake()
    {
        GM = GameObject.Find("Main Camera").GetComponent<GameManager>();
        panel = GameObject.Find("Canvas").transform.Find("Panel").gameObject;
        Option = GameObject.Find("Canvas").transform.Find("Option").gameObject;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void clicked(string str)
    {
        SoundManager.get("touch").Play();
        switch(str)
        {
            case "SetOpen": Option.SetActive(true); Time.timeScale = 0; break;
            case "SetClose": Option.SetActive(false); Time.timeScale = 1; break;
            case "Menu":
                GM.stageReset();
                panel.SetActive(true);
                panel.GetComponent<StageSelect>().stageNum = GM.stageNum;
                SoundManager.get("Stage fail").Stop();
                Time.timeScale = 1;
                gameObject.SetActive(false);
                break;
            case "Restart":
                GM.stageReset();
                GameObject.Find("Canvas").transform.Find("StartPanel").gameObject.SetActive(true);
                SoundManager.get("Stage fail").Stop();
                Time.timeScale = 1;
                gameObject.SetActive(false);
                break;
            case "Next":
                if (GM.stageNum < 10) GM.stageNum++;
                GM.stageReset();
                GameObject.Find("Canvas").transform.Find("StartPanel").gameObject.SetActive(true);
                gameObject.SetActive(false);
                break;
        }
    }

}
