using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeStatement : MonoBehaviour
{
    private Health health;
    private Animator anim;

    void Awake()
    {
        health = GetComponent<Health>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        //Debug.Log(health.CurrentHealth / health.MaxHealth);
        if (health.CurrentHealth / health.MaxHealth >= 0.7)
        {
            anim.SetBool("eye1",true);
            anim.SetBool("eye2", false);
            anim.SetBool("eye3", false);
        }
        else if (health.CurrentHealth / health.MaxHealth < 0.7 && health.CurrentHealth / health.MaxHealth >= 0.3)
        {
            anim.SetBool("eye1", false);
            anim.SetBool("eye2", true);
            anim.SetBool("eye3", false);
        }
        else
        {
            anim.SetBool("eye1", false);
            anim.SetBool("eye2", false);
            anim.SetBool("eye3", true);
        }
    }


}
