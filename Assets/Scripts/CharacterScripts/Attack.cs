using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float knockbackDistance;
    //[SerializeField] LayerMask targetLayerMask;
    [SerializeField]
    HealthEffect.HeathEffectType type;
    ///[SerializeField] private float atkRange;
    [SerializeField] private float dps=5f;
    [SerializeField] private float time = 3f;
    [SerializeField] private string targetTag="Player";
    [SerializeField] private bool CanHurtEverthing=false;

    //public void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireSphere(transform.position, atkRange);
    //}

    //public void rangeAttack()
    //{
    //    var tar = Physics2D.OverlapCircle(transform.position, atkRange, targetLayerMask);
    //    if (tar != null)
    //    {
    //        tar.gameObject.GetComponent<Health>().AddEffect(new HealthEffect(dps, time, type));
    //        Vector2 difference = (tar.transform.position - transform.position).normalized * knockbackDistance;
    //        tar.GetComponent<CharacterMover>().AddExtraVelocity(difference);
    //    }
    //}
    private void OnTriggerEnter2D(Collider2D other)
    {
        Health health = other.GetComponent<Health>();
        if (!CanHurtEverthing)
        {
            if (health != null && other.tag == targetTag)
            {
                if (knockbackDistance != 0)
                {
                    Vector2 difference = (other.transform.position - transform.position).normalized * knockbackDistance;
                    health.GetComponent<CharacterMover>().AddExtraVelocity(difference);
                }
                health.AddEffect(new HealthEffect(dps, time, type));
            }
        }
        else
        {
            if (health != null)
            {
                if (knockbackDistance != 0)
                {
                    Vector2 difference = (other.transform.position - transform.position).normalized * knockbackDistance;
                    health.GetComponent<CharacterMover>().AddExtraVelocity(difference);
                }
                health.AddEffect(new HealthEffect(dps, time, type));
            }
        }

    }
}
