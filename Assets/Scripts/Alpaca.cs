using UnityEngine;

public class Alpaca : MonoBehaviour
{
    public Sprite alpacaS;
    public Sprite alpacaJ;
    public float moveTimeX;
    public float moveTimeY;
    public Vector3 successPos;
    private Vector3 initailPos;
    private Vector3 targetPos;
    private Vector3 previousPos;
    private Vector3 scale;
    private bool isMoving;
    private float elapsedTime;
    private GameObject win;

    void Awake()
    {
        win = GameObject.Find("Canvas").transform.Find("Win").gameObject;
        isMoving = true;
        initailPos = transform.position;
        elapsedTime = 0;
        scale = transform.localScale;
        gameObject.SetActive(false);
    }

    void OnEnable()
    {
        previousPos = transform.position;
        targetPos = new Vector3(successPos.x, successPos.y + 0.35f, -2f);
    }

    void Update()
    {
        if (isMoving)
        {
            if (transform.position.y >= (StageManager.Instance.successH + 1.5f))
            {
                isMoving = false;
                transform.localScale = new Vector3(0.67f, 0.67f, 1);
                transform.GetComponent<SpriteRenderer>().sprite = alpacaJ;
                Invoke("activeWin", 0.5f);
            }
            elapsedTime += Time.deltaTime;
            if (transform.position == targetPos)
            {
                elapsedTime = 0;
                transform.position += Vector3.right * 0.01f;
                previousPos = transform.position;
            }
            if (transform.position.x < targetPos.x)
                transform.position = Vector3.Lerp(previousPos, targetPos, elapsedTime / moveTimeX);
            else
                transform.position = Vector3.Lerp(previousPos, targetPos + new Vector3(0, StageManager.Instance.successH + 1.5f, 0), elapsedTime / moveTimeY);
        }
	}

    public void reset()
    {
        transform.position = initailPos;
        elapsedTime = 0;
        transform.GetComponent<SpriteRenderer>().sprite = alpacaS;
        isMoving = true;
        transform.localScale = scale;
        gameObject.SetActive(false);
    }

    private void activeWin()
    {

        SoundManager.get("main").Stop();
        SoundManager.get("Stage win").Play();
        win.SetActive(true);
    }
}
