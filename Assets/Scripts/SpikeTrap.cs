using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    // Start is called before the first frame update
    enum State
    {
        Idle,
        Attack,
    }
    [Header("Settings")]

    [SerializeField] private float atkRange = 1f;
    [SerializeField] private float atkAmount = 5f;
    [SerializeField] private float knockbackDistance = 5f;

    [Header("Data")]

    [SerializeField] LayerMask layerMask;
    [SerializeField] State currentState = State.Idle;

    float stateTimer = 0;
    [SerializeField] bool isPlayerInRange;


    Animator anim;
    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        switch (currentState)
        {
            case State.Idle:
                anim.SetBool("idle",true);
                DetectTargetinRange();
                stateTimer += Time.deltaTime;

                if (isPlayerInRange&&stateTimer>3)
                {
                    ToAttack();
                    anim.SetBool("idle", false);

                }
                break;

            case State.Attack:
                anim.SetBool("attack", true);
                break;
        }

    }

    public void ToIdle()
    {
        currentState = State.Idle;
        anim.SetBool("attack", false);
        stateTimer = 0f;
    }

    void ToAttack()
    {
        currentState = State.Attack;

    }
    public void AnimatorAttack()
    {
        var tar = Physics2D.OverlapCircle(transform.position, atkRange, layerMask);

        if (tar != null && tar.gameObject.tag == "Player")
        {
            tar.gameObject.GetComponent<Health>().TakeDamge(atkAmount);
            Vector2 difference = (tar.transform.position - transform.position).normalized * knockbackDistance;
            tar.GetComponent<CharacterMover>().AddExtraVelocity(difference);
        }
    }
    void DetectTargetinRange()
    {
        var tarColliders = Physics2D.OverlapCircle(transform.position, atkRange, layerMask);
        if (tarColliders != null)
        {
            isPlayerInRange = true;
        }
        else
        {
            isPlayerInRange = false;
        }
    }
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, atkRange);

    }
}
