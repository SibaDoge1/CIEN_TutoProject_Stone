using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneSetZ : StoneSet
{

    // Use this for initialization
    protected override void Awake()
    {
        base.Awake();
        rotationData = new int[4, 4, 2]
        {
            {{2,0},{1,-1},{0,0},{-1,-1}},
            {{0,-2},{-1,-1},{0,0},{-1,1}},
            {{-2,0},{-1,1},{0,0},{1,1}},
            {{0,2},{1,1},{0,0},{1,-1}}
        };
    }
}
