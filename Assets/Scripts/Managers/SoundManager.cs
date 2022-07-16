using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager
{
    public static void PlaySound(AudioClip clip)
    {
        GameObject soundGO = new GameObject("Sound");
        soundGO.AddComponent<DelayedDestroy>();
        AudioSource source = soundGO.AddComponent<AudioSource>();
        source.PlayOneShot(clip);
    }
}
