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
        SoundManager.get("main").Play();
        spawnQueue.Clear();
        for (int i = 0; i < stageNum; i++)
        {
            StringBuilder stone = new StringBuilder();
            stoneType type = (stoneType)Random.Range(0, 7);
            stone.Append(type.ToString());
            stone.Append("_");
            for (int j = 0; j<4; j++)
            {
                color col = (color)Random.Range(0, 3);
                stone.Append(col.ToString());
            }
            spawnQueue.Enqueue(stone.ToString());
        }
        if (spawnQueue.Count != 0)
            next = spawnQueue.Dequeue();
        spawnNext();
        isCamMove = false;
    }
}
