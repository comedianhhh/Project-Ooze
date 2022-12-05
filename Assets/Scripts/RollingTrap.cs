using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Animations;
using UnityEngine;

public class RollingTrap : MonoBehaviour
{
    enum State
    {
        Idle,
        Roll
    }
    [SerializeField] private float speed;

    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask layerMask;

    [SerializeField] Health target;

    [SerializeField] Transform detect;
    [SerializeField] float detectRange;



    [SerializeField] private bool isPlayerInRange;


    [SerializeField]private float stateTimer = 0f;
    [SerializeField] State currentState = State.Idle;
    private Vector2 dir;


    private Animator anim;
    private CharacterMover enemyMover;



    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        enemyMover = GetComponent<CharacterMover>();

    }
    private void Update()
    {
        target = GetComponent<TargetReceiver>().Target;
        switch (currentState)
        {
            case State.Idle:

                anim.SetBool("idle",true);
                DetectTargetinRange();
                if (isPlayerInRange)
                {
                    stateTimer += Time.deltaTime;
                    if (stateTimer > checkDelay)
                    {
                        Lookat();
                        ToRoll();
                        anim.SetBool("idle", false);
                    }

                }
                break;

            case State.Roll:

                if (target != null)
                {
                    enemyMover.Move(speed * dir);
                }
                else enemyMover.Move(Vector2.zero);

                anim.SetBool("roll",true);
                break;
        }


    }

    void setVelocity(float veclocity)
    {
        if (target != null)
        {
            Vector2 dir = (target.transform.position - transform.position).normalized;
            enemyMover.Move(veclocity * dir);
        }
        else enemyMover.Move(Vector2.zero);
    }



    void DetectTargetinRange()
    {
        var tarColliders = Physics2D.OverlapCircleAll(detect.position, detectRange, layerMask).ToList().Find(e => e.CompareTag("Player"));

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
        Gizmos.DrawWireSphere(detect.position, detectRange);

    }
    public void Lookat()
    {
        if (target == null) return;

        if (transform.position.x - target.transform.position.x >= 0.1f) //ÅÐ¶ÏÄ¿±êÎ»ÖÃ
        {
            transform.localEulerAngles = Vector3.up * 180;

        }
        else
        {
            transform.localEulerAngles = Vector3.zero;
        }
    }

    void OnTriggerEnter2D()
    {
        setVelocity(0f);
        anim.SetBool("roll", false);
        ToIdle();
        
    }

    void ToRoll()
    {
        currentState = State.Roll;
        stateTimer = 0f;
        dir = (target.transform.position - transform.position).normalized;
    }

    void ToIdle()
    {
        currentState = State.Idle;
        stateTimer = 0f;
    }

}
