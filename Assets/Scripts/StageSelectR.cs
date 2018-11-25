using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectR : StageSelect
{
    protected override void stageChange(int i)
    {
        stageNum+= i;
        if (stageNum > 50) stageNum = 50;
        if (stageNum < 1) stageNum = 1;
        numText.text = stageNum.ToString();
    }

    public override void clicked(string str)
    {
        SoundManager.get("touch").Play();
        switch (str)
        {
            case "StageUp": stageChange(1); break;
            case "StageDown": stageChange(-1); break;
            case "StageUp5": stageChange(5); break;
            case "StageDown5": stageChange(-5); break;
            case "Start": start(); break;
        }
    }
}
