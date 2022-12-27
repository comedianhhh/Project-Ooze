using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Cyclops : MonoBehaviour
{
    enum State { Idle, Move, Charge, PlayerDetected,Die}

    [Header("Settings")]
    [SerializeField] float movespeed = 1f;
    [SerializeField] float chargeSpeed = 3f;

    [SerializeField] private float chargeTime = 0.4f;
    [SerializeField] private float atkRange=1.5f;
    [SerializeField] private float atkAmount=5f;
    [SerializeField] float detectRange=2f;



    [Header("Data")] 
    
    [SerializeField] State currentState = State.Idle;
    [SerializeField] bool isPlayerInRange;

    [SerializeField] Health target;
    [SerializeField] LayerMask layerMask;


    float stateTimer = 0;

    //Rigidbody2D rigidbody2D;
    TargetReceiver targetReceiver;
    GameObject aliveGo;
    Animator anim;
    CharacterMover enemyMover;

    private AIPath AI;
    private AIDestinationSetter destinationSetter;

    private void Awake()
    {
        aliveGo = transform.Find("Alive").gameObject;
        //rigidbody2D = GetComponent<Rigidbody2D>();
        targetReceiver = GetComponent<TargetReceiver>();
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

                //exit
                if (target != null)
                {
                    ToMove();
                    anim.SetBool("idle", false);
                }

                break;
            //MOVE
            case State.Move:

                setVelocity(movespeed);
                anim.SetBool("move", true);
                stateTimer += Time.fixedDeltaTime;
                DetectTargetinRange();
                if (target == null)
                {
                    ToIdle();
                    anim.SetBool("move", false);
                }
                else if (stateTimer > 2 && target != null&& isPlayerInRange)
                {
                    ToPlayerDetected();
                    destinationSetter.target = target.transform;
                    anim.SetBool("move", false);
                }
                break;

            //DETECTED
            case State.PlayerDetected:

                //setVelocity(0f);
                anim.SetBool("playerDetected", true);
                stateTimer += Time.fixedDeltaTime;

                //exit
                if (stateTimer > 1 && target != null&& isPlayerInRange)
                {
                    ToCharge();
                    anim.SetBool("playerDetected", false);
                }
                else if (target == null)
                {
                    ToIdle();
                    anim.SetBool("playerDetected", false);
                }
                break;
            //CHARGE
            case State.Charge:
                setVelocity(chargeSpeed);
                anim.SetBool("charge", true);
                break;
            //Die
            case State.Die:
                setVelocity(0f);
                break;

        }

        Lookat();
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

    void setVelocity(float veclocity)
    {
        if (target != null)
        {
            AI.maxSpeed = veclocity;
        }
        else AI.maxSpeed = 0;
    }


    //State Switch
    void ToIdle()
    {
        //setVelocity(0f);
        AI.canMove = false;
        currentState = State.Idle;
    }

    public void ToMove()
    {
        currentState = State.Move;
        anim.SetBool("charge",false);
        stateTimer = 0f;
        AI.canMove = true;

    }

    void ToPlayerDetected()
    {
        currentState = State.PlayerDetected;
        AI.canMove = false;

        stateTimer = 0;
    }

    public void ToCharge()
    {
        currentState = State.Charge;
        AI.canMove = true;

    }

    public void Todie()
    {
        currentState = State.Die;
    }
    public void AnimatorAttack()
    {
        var tar = Physics2D.OverlapCircle(transform.position, atkRange, layerMask);

        if (tar != null && tar.gameObject.tag == "Player")
        {
            tar.gameObject.GetComponent<Health>().TakeDamge(atkAmount);
        }
    }
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }
    void DetectTargetinRange()
    {
        var tarColliders = Physics2D.OverlapCircle(transform.position, detectRange, layerMask);
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
