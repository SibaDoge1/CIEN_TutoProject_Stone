using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageData : MonoBehaviour
{

    private static StageData _instance = null;
    public Dictionary<string, Dictionary<string, string>> stageData; // 행 인덱스 -> 열 인덱스 순으로

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        Instance.stageData = CsvParser.ReadCsv("stage");
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public static StageData Instance
    {
        get
        {
            return _instance;
        }
    }


}
