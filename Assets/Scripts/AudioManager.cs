using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
