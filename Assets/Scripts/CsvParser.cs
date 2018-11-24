using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
public static class CsvParser
{


    //파일 이름 받아서 해당 파일을 TextAsset으로 변환하고 string 배열로 저장  
    private static string[] GetLines(string fileName)
    {
        TextAsset csvTextAsset = (TextAsset)Resources.Load(fileName) as TextAsset;
        return csvTextAsset.text.Split('\n');
    }

    // ',' 과 'eol' 로 스트링 배열을 다시 분리해서 Dictionary 구조에 저장
    public static Dictionary<string, Dictionary<string, string>> ReadCsv(string fileName)
    {
        Dictionary<string, Dictionary<string, string>> data = new Dictionary<string, Dictionary<string, string>>();
        string[] line = GetLines(fileName);

        string[] attrs = line[0].Split(',');

        for (int loop = 1; loop < line.Length; loop++)
        {
            if (line[loop].Length < 1)
            {
                continue;
            }
            string[] split = line[loop].Split(',');

            Dictionary<string, string> tempDict = new Dictionary<string, string>();
            for (int loop2 = 1; loop2 < split.Length - 1; loop2++)
            {
                if (split[loop2].Equals("eol") || attrs[loop2].Equals("eol")) break;
                tempDict.Add(attrs[loop2], split[loop2]);
            }

            data.Add(split[0], tempDict);
        }

        return data;
    }


}