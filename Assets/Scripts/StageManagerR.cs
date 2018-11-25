using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class StageManagerR : StageManager
{
    enum stoneType { I,L,J,Z,T,S,O};
    enum color { r,y,b };
    
    protected override void OnEnable()
    {
        StringBuilder stone;
        string stoneStr;
        SoundManager.get("main").Play();
        spawnQueue.Clear();
        for (int i = 0; i < stageNum; i++)
        {
            while (true)
            {
                stone = new StringBuilder();
                stoneType type = (stoneType)Random.Range(0, 7);
                stone.Append(type.ToString());
                stone.Append("_");
                for (int j = 0; j < 4; j++)
                {
                    color col = (color)Random.Range(0, 3);
                    stone.Append(col.ToString());
                }
                stoneStr = stone.ToString();
                if (i == 0 && !checkMatch3(stoneStr)) break;
                if (i != 0 && !checkMatch4(stoneStr)) break;
            }
            spawnQueue.Enqueue(stoneStr);
        }
        if (spawnQueue.Count != 0)
            next = spawnQueue.Dequeue();
        spawnNext();
        isCamMove = false;
    }

    private bool checkMatch4(string str)
    {
        for (int i = 0; i < 3; i++)
        {
            if (str[3+i] != str[2+i])
                return false;
        }
        return true;
    }

    private bool checkMatch3(string str)
    {
        for (int i = 0; i < 2; i++)
        {
                if (str[2 + i] == str[3 + i] && str[2 + i] == str[4 + i]) 
                    return true;
        }
        return false;
    }
}
