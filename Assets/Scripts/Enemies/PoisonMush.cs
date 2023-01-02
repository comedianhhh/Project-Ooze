using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonMush : MonoBehaviour
{
    enum State
    {
        Idle,
        Move,
        PlayerDetected,
        Attack,
        Die
    }

    [Header("Settings")]

    [SerializeField] float movespeed = 1f;

    [SerializeField] private float atkRange = 1f;
    [SerializeField] private float atkAmount = 5f;

    [SerializeField] float knockbackDistance = 0.5f;

    [SerializeField] LayerMask layerMask;
    [SerializeField] GameObject PoisonParticle;


    [Header("Data")]

    [SerializeField] State currentState = State.Idle;
    [SerializeField] Health target;

    [SerializeField] bool isPlayerInRange;

    float stateTimer = 0;
    [SerializeField]
    //float moveTime=4f;

    //Rigidbody2D rigidbody2D;
    GameObject aliveGo;
    Animator anim;
    CharacterMover enemyMover;
    public Transform atk;

    private AIPath AI;
    private AIDestinationSetter destinationSetter;

    private void Awake()
    {
        aliveGo = transform.Find("Alive").gameObject;
        //rigidbody2D = GetComponent<Rigidbody2D>();
        anim = aliveGo.GetComponent<Animator>();
        enemyMover = GetComponent<CharacterMover>();
        AI = GetComponent<AIPath>();
        destinationSetter = GetComponent<AIDestinationSetter>();
    }

    void FixedUpdate()
    {
        target = GetComponent<TargetReceiver>().Target;
        switch (currentState)
        {
            //IDLE
            case State.Idle:
                //setVelocity(0f);
                AI.canMove = false;
                anim.SetBool("idle", true);
                stateTimer += Time.deltaTime;
                //exit
                if (target != null)
                {
                    ToPlayerDetected();
                    anim.SetBool("idle", false);
                }
                break;
            //DETECTED
            case State.PlayerDetected:
                Lookat();
                //setVelocity(0f);
                AI.canMove = false;
                stateTimer += Time.deltaTime;
                anim.SetBool("detect", true);

                //exit
                if (stateTimer > 1 && target != null)
                {
                    ToMove();
                    destinationSetter.target = target.transform;
                    anim.SetBool("detect", false);

                }
                else if (target == null)
                {
                    ToIdle();
                    anim.SetBool("detect", false);

                }
                break;
            //MOVE
            case State.Move:
                Lookat();
                DetectTargetinRange();
                stateTimer += Time.fixedDeltaTime;
                setVelocity(movespeed);
                anim.SetBool("move", true);

                if (target == null)
                {
                    ToIdle();
                    anim.SetBool("move", false);
                }
                else if (target != null && isPlayerInRange && stateTimer > 1)
                {
                    ToAttack();
                    anim.SetBool("move", false);
                }
                break;
            //ATK
            case State.Attack:
                anim.SetBool("attack", true);
                break;
            //Die
            case State.Die:
                AI.canMove = false;
                break;
        }


    }
    public void Lookat()
    {
        if (target == null) return;

        if (transform.position.x - target.transform.position.x >= 0.1f) //�ж�Ŀ��λ��
        {
            transform.localEulerAngles = Vector3.up * 180;

        }
        else
        {
            transform.localEulerAngles = Vector3.zero;
        }
    }
    void setVelocity(float veclocity)
    {
        if (target != null)
        {

            //Vector2 dir = (target.transform.position - transform.position).normalized;
            //enemyMover.Move(veclocity* dir);
            AI.maxSpeed = veclocity;

        }
        else AI.maxSpeed = 0; //enemyMover.Move(Vector2.zero);
    }

    void ToIdle()
    {
        setVelocity(0f);
        
        currentState = State.Idle;
    }

    public void ToMove()
    {
        anim.SetBool("attack", false);
        AI.canMove = true;

        //Debug.Log("ToMove");
        currentState = State.Move;
        stateTimer = 0f;
        AudioManager.Play("Meat_jumps2");

    }

    void ToPlayerDetected()
    {
        currentState = State.PlayerDetected;
        stateTimer = 0;
    }

    void ToAttack()
    {
        //setVelocity(0f);
        AI.canMove = false;
        currentState = State.Attack;
    }

    public void ToDie()
    {
        currentState = State.Die;
    }
    public void AnimatorAttack()
    {
        if (PoisonParticle != null) Instantiate(PoisonParticle, transform.position, Quaternion.identity);
        AudioManager.Play("meat_death4");

    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(atk.position, atkRange);

    }
    void DetectTargetinRange()
    {
        var tarColliders = Physics2D.OverlapCircle(atk.position, atkRange, layerMask);
        if (tarColliders != null)
        {
            isPlayerInRange = true;
        }
        else
        {
            isPlayerInRange = false;
        }
    }
}
