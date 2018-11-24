using System.Collections.Generic;
using UnityEngine;

public class StageData : MonoBehaviour
{

    private static StageData instance = null;
    private Dictionary<string, Dictionary<string, string>> stageData; // 행 인덱스 -> 열 인덱스 순으로

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        stageData = CsvParser.ReadCsv("stage");
    }

    // Use this for initialization
    void Start()
    {

    }

    public Dictionary<string, string> getStageData(string idx)
    {
        return stageData[idx];
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Camera>().aspect = 9f / 16f;

    }

    public static StageData Instance
    {
        get
        {
            return instance;
        }
    }


}
