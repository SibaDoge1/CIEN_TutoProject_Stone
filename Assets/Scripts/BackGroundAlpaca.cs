using System.Collections;
using UnityEngine;

public class BackGroundAlpaca : MonoBehaviour
{
    Rigidbody2D rigid;
    Vector2 forceDir;
    // Use this for initialization
    void Awake ()
    {
        Debug.Log("awake!");
        rigid = GetComponent<Rigidbody2D>();
        StartCoroutine("jump");
    }

    // Update is called once per frame
    void Update ()
    {
        transform.Translate(Vector2.right * 0.015f);
        if (transform.position.x > 9f) transform.position = new Vector2(-2f, -0.30f);

    }

    IEnumerator jump()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            rigid.AddForce(Vector2.up * 200f);
        }
    }

}
