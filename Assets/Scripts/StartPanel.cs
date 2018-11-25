using UnityEngine;
using UnityEngine.UI;

public class StartPanel : MonoBehaviour
{

    public int stageNum;
    private Text numText;

    void Awake()
    {
        numText = transform.Find("Num").GetComponent<Text>();
    }

    void OnEnable()
    {
        numText.text = stageNum.ToString();
        Invoke("startStage", 1.0f);
    }

    void startStage()
    {
        StageManager.Instance.stageNum = stageNum;
        StageManager.Instance.enabled = true;
        gameObject.SetActive(false);
    }
}
