using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneSetO : StoneSet
{

    // Use this for initialization
    protected override void Awake()
    {
        base.Awake();
        rotationData = new int[4, 4, 2]
        {
            {{1,0},{0,-1},{0,1},{-1,0}},
            {{0,-1},{-1,0},{1,0},{0,1}},
            {{-1,0},{0,1},{0,-1},{1,0}},
            {{0,1},{1,0},{-1,0},{0,-1}}
        };
        wallKickData = new int[4, 5, 2]
        {
            {{0,0},{0,0},{0,0},{0,0},{0,0}},
            {{0,0},{0,0},{0,0},{0,0},{0,0}},
            {{0,0},{0,0},{0,0},{0,0},{0,0}},
            {{0,0},{0,0},{0,0},{0,0},{0,0}}
        };
    }
}
