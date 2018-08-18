using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager {

    public static AudioSource get (string src)
    {
        return GameObject.Find(src).GetComponent<AudioSource>();
    }
}
