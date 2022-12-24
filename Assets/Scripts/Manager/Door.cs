using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();

        GameManager.RegisterDoor(this);
    }

    public void Open()
    {
        anim.SetTrigger("Open");
        AudioManager.Play("stoneDoor_snd");
    }
}
