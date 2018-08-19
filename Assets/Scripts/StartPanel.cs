using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel : MonoBehaviour {

    public int stageNum;
    private GameManager GM;
    private Text numText;
    // Use this for initialization
    void Awake()
    {
        numText = transform.Find("Num").GetComponent<Text>();
        GM = GameObject.Find("Main Camera").GetComponent<GameManager>();
    }

    void OnEnable()
    {
        Invoke("startPanel", 1.0f);
    }
        // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        numText.text = GM.stageNum.ToString();
    }

    void startPanel()
    {
        GM.enabled = true;
        GM.startGame();
        gameObject.SetActive(false);
    }
}
