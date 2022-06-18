using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyCustomAsset/SoundPool")]
public class SoundPoolSO : ScriptableObject
{
    [SerializeField] List<AudioClip> AllBgm = new List<AudioClip>();
    [SerializeField] List<AudioClip> AllSound = new List<AudioClip>();
    public void GetBgm(AudioSource who, int which)
    {
        if (AllBgm[which] != null)
        {
            who.clip = AllBgm[which];
            who.Play();
        }
    }
    public void GetSound(AudioSource who, int which)
    {
        if (AllSound[which] != null)
        {
            who.clip = AllSound[which];
            who.Play();
        }
    }
}
