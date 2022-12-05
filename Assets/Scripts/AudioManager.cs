using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    

    public static void Play(string name)
    {
        var clip = Resources.Load<AudioClip>("Audio/" + name);
        AudioSource.PlayClipAtPoint(clip, Vector3.zero, 1);
    }

    public static void Play(string name, Vector3 pos, float volume)
    {
        var clip = Resources.Load<AudioClip>("Audio/" + name);
        AudioSource.PlayClipAtPoint(clip, pos, volume);
    }


}
