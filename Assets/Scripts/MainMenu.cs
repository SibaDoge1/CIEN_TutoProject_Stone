using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    GameObject Guide;
    GameObject UI;
    void Awake()
    {
        Time.timeScale = 1;
        Guide = GameObject.Find("Canvas").transform.Find("Guide").gameObject;
        UI = GameObject.Find("Canvas").transform.Find("UI").gameObject;
    }

    public void stage()
    {
        SceneManager.LoadScene("StageScene");
    }

    public void random()
    {
        SceneManager.LoadScene("RandomScene");
    }

    public void guide()
    {
        Guide.SetActive(true);
        UI.SetActive(false);
    }

    public void Close()
    {
        UI.SetActive(true);
        Guide.SetActive(false);
    }


    public void clicked(string str)
    {
        SoundManager.get("touch").Play();
        switch (str)
        {
            case "Stage": stage(); break;
            case "Random": random(); break;
            case "Guide": guide(); break;
            case "Close": Close(); break;
        }
    }

}
