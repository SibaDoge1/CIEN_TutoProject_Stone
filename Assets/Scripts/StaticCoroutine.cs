using UnityEngine;
using System;
using System.Collections;

public class StaticCoroutine : MonoBehaviour
{

    private static StaticCoroutine mInstance = null;
    public delegate void func();

    private static StaticCoroutine instance
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = GameObject.FindObjectOfType(typeof(StaticCoroutine)) as StaticCoroutine;

                if (mInstance == null)
                {
                    mInstance = new GameObject("StaticCoroutine").AddComponent<StaticCoroutine>();
                }
            }
            return mInstance;
        }
    }

    void Awake()
    {
        if (mInstance == null)
        {
            mInstance = this as StaticCoroutine;
        }
    }

    IEnumerator Perform(IEnumerator coroutine)
    {
        yield return StartCoroutine(coroutine);
        Die();
    }

   static IEnumerator doDelay(func function, float Delay)
    {
        yield return new WaitForSeconds(Delay);
        function();
    }

    public static void DoCoroutine(func function, float Delay)
    {
        //여기서 인스턴스에 있는 코루틴이 실행될 것이다.
        instance.StartCoroutine(instance.Perform(doDelay(function, Delay)));
    }

    void Die()
    {
        mInstance = null;
        Destroy(gameObject);
    }

    void OnApplicationQuit()
    {
        mInstance = null;
    }
}