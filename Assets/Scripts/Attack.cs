using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float knockbackDistance;
    [SerializeField] LayerMask targetLayerMask;
    [SerializeField]
    HealthEffect.HeathEffectType type;
    [SerializeField] private float atkRange;
    [SerializeField] private float dps=5f;
    [SerializeField] private float time = 3f;


    void Awake()
    {

    }

    void FixedUpdate()
    {

    }
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, atkRange);
    }

    public void rangeAttack()
    {
        var tar = Physics2D.OverlapCircle(transform.position, atkRange, targetLayerMask);
        Debug.Log(tar);
        if (tar != null)
        {
            tar.gameObject.GetComponent<Health>().AddEffect(new HealthEffect(dps, time, type));
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        Health health = other.GetComponent<Health>();
        if (health != null&&other.tag=="Player")
        {
            if (knockbackDistance != 0)
            {
                Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
                Vector2 difference = (other.transform.position - transform.position).normalized * knockbackDistance;
                health.GetComponent<CharacterMover>().AddExtraVelocity(difference);
            }
            health.AddEffect(new HealthEffect(dps, time, type));
        }
    }
}
