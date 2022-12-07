using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TrapTrigger : MonoBehaviour
{
    [Header("Trap Settings")] 

    [SerializeField] private float activationDelay;

    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float atkRange = 1f;
    [SerializeField] private float atkAmount = 1f;
    [SerializeField] private float knockbackDistance = 1f;

    private Animator anim;

    [SerializeField]private bool triggered;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collison)
    {
        if (collison.tag == "Player")
        {
            Debug.Log("player enter");
            if (!triggered)
                StartCoroutine(ActiveTrap());


        }
    }
    public void AnimatorAttack()
    {
        var tar = Physics2D.OverlapCircleAll(transform.position, atkRange, layerMask).ToList().Find(e => e.CompareTag("Player"));

        if (tar != null && tar.gameObject.tag == "Player")
        {
            tar.gameObject.GetComponent<Health>().TakeDamge(atkAmount);
            Vector2 difference = (tar.transform.position - transform.position).normalized * knockbackDistance;
            tar.GetComponent<CharacterMover>().AddExtraVelocity(difference);
        }
    }
    private IEnumerator ActiveTrap()
    {
        Debug.Log("start countdown");
        triggered = true;
        yield return new WaitForSeconds(activationDelay);

        anim.SetTrigger("attack");
    }

    public void inActiveTrap()
    {
        triggered = false;
    }

}
