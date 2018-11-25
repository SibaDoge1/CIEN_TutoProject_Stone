using UnityEngine;
using UnityEngine.UI;

public class StageSelect : MonoBehaviour
{

    protected int stageNum = 1;
    protected Text numText;

    void Awake()
    {
        numText = transform.Find("Num").GetComponent<Text>();
    }

    void OnEnable()
    {
        SoundManager.get("main start").Play();
    }

    protected virtual void stageChange(int i)
    {
        stageNum += i;
        if (stageNum > 10) stageNum = 10;
        if (stageNum < 1) stageNum = 1;
        numText.text = stageNum.ToString();
    }
    protected void start()
    {
        SoundManager.get("touch").Play();
        SoundManager.get("main start").Stop();
        GameObject startPanel = GameObject.Find("Canvas").transform.Find("StartPanel").gameObject;
        startPanel.GetComponent<StartPanel>().stageNum = stageNum;
        startPanel.SetActive(true);
        gameObject.SetActive(false);
    }

    public virtual void clicked(string str)
    {
        SoundManager.get("touch").Play();
        switch (str)
        {
            case "StageUp": stageChange(1); break;
            case "StageDown": stageChange(-1); break;
            case "Start": start(); break;
        }
    }    
}
