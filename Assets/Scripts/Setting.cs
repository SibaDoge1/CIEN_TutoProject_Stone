using UnityEngine;
using UnityEngine.SceneManagement;

public class Setting : MonoBehaviour {
    private GameObject Option;

    void Awake()
    {
        Option = GameObject.Find("Canvas").transform.Find("Option").gameObject;
    }

    private void open()
    {
        Option.SetActive(true); Time.timeScale = 0;
    }

    private void close()
    {
        Option.SetActive(true); Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    private void gotoMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    private void restart()
    {
        Time.timeScale = 1;
        if (!StageManager.Instance.enabled) return;
        StageManager.Instance.stageReset();
        GameObject.Find("Canvas").transform.Find("StartPanel").gameObject.SetActive(true);
        SoundManager.get("Stage fail").Stop();
        gameObject.SetActive(false);
    }

    private void next()
    {

        Time.timeScale = 1;
        StartPanel sp = GameObject.Find("Canvas").transform.Find("StartPanel").GetComponent<StartPanel>();
        sp.stageNum++;
        if (sp.stageNum > 10) sp.stageNum = 10;
        StageManager.Instance.stageReset();
        GameObject.Find("Canvas").transform.Find("StartPanel").gameObject.SetActive(true);
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
            case "Next": next(); break;
        }
    }

}
