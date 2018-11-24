using UnityEngine;
using UnityEngine.SceneManagement;

public class Setting : MonoBehaviour {
    private GameObject panel;
    private GameObject Option;

    void Awake()
    {
        panel = GameObject.Find("Canvas").transform.Find("Panel").gameObject;
        Option = GameObject.Find("Canvas").transform.Find("Option").gameObject;
    }

    public void open()
    {
        Option.SetActive(true); Time.timeScale = 0;
    }

    public void close()
    {
        Option.SetActive(true); Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    /*public void gotoMenu()
    {
        StageManager.Instance.stageReset();
        panel.SetActive(true);
        panel.GetComponent<StageSelect>().stageNum = StageManager.Instance.stageNum;
        SoundManager.get("Stage fail").Stop();
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }*/

    public void gotoMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void restart()
    {
        if (!StageManager.Instance.enabled) return;
        StageManager.Instance.stageReset();
        GameObject.Find("Canvas").transform.Find("StartPanel").gameObject.SetActive(true);
        SoundManager.get("Stage fail").Stop();
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    public void Next()
    {
        if (StageManager.Instance.stageNum < 10) StageManager.Instance.stageNum++;
        StageManager.Instance.stageReset();
        GameObject.Find("Canvas").transform.Find("StartPanel").gameObject.SetActive(true);
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    public void clicked(string str)
    {
        SoundManager.get("touch").Play();
        switch(str)
        {
            case "Open": open(); break;
            case "Close": close(); break;
            case "GotoMenu": gotoMenu(); break;
            case "Restart": restart(); break;
            case "Next": Next(); break;
        }
    }

}
