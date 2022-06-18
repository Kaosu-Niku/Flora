using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBGM : MonoBehaviour
{
    [SerializeField] SoundPoolSO GetSoundPool;
    [SerializeField] int WhatBgm;
    AudioSource MyAudio;
    private void Start()
    {
        MyAudio = GetComponent<AudioSource>();
        if (MyAudio)
            GetSoundPool.GetBgm(MyAudio, WhatBgm);
    }
}
