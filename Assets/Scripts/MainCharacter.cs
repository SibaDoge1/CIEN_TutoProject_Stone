using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour {
    private GameManager GM;
    private Map mp;
    public Vector3 successPos;
    public Vector3 targetPos;
    private bool isMoving;
    private Vector3 previousPos;
    private float moveTime;
    public Sprite alpacaS;
    public Sprite alpacaJ;
    private Vector3 scale;
    private GameObject win;
    // Use this for initialization
    void Awake()
    {
        GM = GameObject.Find("Main Camera").GetComponent<GameManager>();
        mp = GameObject.Find("Main Camera").GetComponent<Map>();
        win = GameObject.Find("Canvas").transform.Find("Win").gameObject;
        isMoving = true;
        moveTime = 0;
        scale = transform.localScale;
    }

    void OnEnable()
    {
        previousPos = transform.position;
        targetPos = new Vector3(successPos.x, successPos.y + 0.35f, -2f);
    }

    void Start () {
	}

    // Update is called once per frame
    void Update() {
        if (isMoving)
        {
            if (transform.position.y >= (mp.successH + 1.5f))
            {
                isMoving = false;
                transform.localScale = new Vector3(0.67f, 0.67f, 1);
                transform.GetComponent<SpriteRenderer>().sprite = alpacaJ;
                Invoke("winPanel", 0.5f);
            }
            moveTime += Time.deltaTime;
            if (transform.position == targetPos)
            {
                moveTime = 0;
                transform.position += Vector3.right * 0.01f;
                previousPos = transform.position;
            }
            if (transform.position.x < targetPos.x)
                transform.position = Vector3.Lerp(previousPos, targetPos, moveTime / 0.75f);
            else
                transform.position = Vector3.Lerp(previousPos, targetPos + new Vector3(0, mp.successH + 1.5f, 0), moveTime / 1.25f);
        }
	}

    public void reset()
    {
        transform.position = new Vector3(-1.5f, 0.4f, 0);
        moveTime = 0;
        transform.GetComponent<SpriteRenderer>().sprite = alpacaS;
        isMoving = true;
        gameObject.SetActive(false);
        transform.localScale = scale;
    }

    public void winPanel()
    {
        win.SetActive(true);
    }
}
