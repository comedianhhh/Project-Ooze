using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float damageAmount=1f;
    public float knockbackDistance;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 7)
        {
            Health health = other.GetComponent<Health>();
            if (health != null)
            {
                Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
                Vector2 difference = (other.transform.position - transform.position).normalized * knockbackDistance;
                health.TakeDamge(damageAmount);
                health.GetComponent<CharacterMover>().AddExtraVelocity(difference);

            }
        }

    }
}
