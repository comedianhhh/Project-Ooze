using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioActions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position, 1);
    }
}
