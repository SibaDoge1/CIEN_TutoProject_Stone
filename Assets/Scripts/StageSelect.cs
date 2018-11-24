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

    protected virtual void stageUp()
    {
        if (stageNum < 10) stageNum++;
        numText.text = stageNum.ToString();
    }

    private void stageDown()
    {
        if (stageNum > 1) stageNum--;
        numText.text = stageNum.ToString();
    }

    private void start()
    {
        SoundManager.get("touch").Play();
        SoundManager.get("main start").Stop();
        GameObject startPanel = GameObject.Find("Canvas").transform.Find("StartPanel").gameObject;
        startPanel.GetComponent<StartPanel>().stageNum = stageNum;
        startPanel.SetActive(true);
        gameObject.SetActive(false);
    }

    public void clicked(string str)
    {
        SoundManager.get("touch").Play();
        switch (str)
        {
            case "StageUp": stageUp(); break;
            case "StageDown": stageDown(); break;
            case "Start": start(); break;
        }
    }    
}
