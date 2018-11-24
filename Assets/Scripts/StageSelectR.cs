using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectR : StageSelect
{
    protected override void stageUp()
    {
        if (stageNum < 50) stageNum++;
        numText.text = stageNum.ToString();
    }
}
