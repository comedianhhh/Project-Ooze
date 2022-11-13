using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int damageDirection;

    public float damageAmount=1f;



    private void OnTriggerEnter2D(Collider2D other)
    {

        Health health = other.GetComponent<Health>();

        if (health != null)
        {
            health.TakeDamge(damageAmount);

        }
    }



}
