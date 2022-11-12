using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cyclops : MonoBehaviour
{
    enum State { Idle, Move, Charge, PlayerDetected }

    [Header("Settings")]
    [SerializeField] float movespeed = 1f;
    [SerializeField] float chargeSpeed = 3f;

    [SerializeField] private float chargeTime = 0.4f;

    [Header("Data")] 
    

    [SerializeField] State currentState = State.Idle;
    [SerializeField]Health target;

    float stateTimer = 0;

    Rigidbody2D rigidbody2D;
    TargetReceiver targetReceiver;
    GameObject aliveGo;
    Animator anim;

    private void Awake()
    {

        aliveGo = transform.Find("Alive").gameObject;
        rigidbody2D = GetComponent<Rigidbody2D>();
        targetReceiver = GetComponent<TargetReceiver>();
        anim = aliveGo.GetComponent<Animator>();

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        target = GetComponent<TargetReceiver>().Target;
        switch (currentState)
        {
            //IDLE
            case State.Idle:
                setVelocity(0f);
                anim.SetBool("idle", true);
                stateTimer += Time.deltaTime;
                
                //exit
                if (stateTimer > 3)
                {
                    anim.SetBool("idle", false);
                    ToMove();
                }

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
                stateTimer += Time.deltaTime;

                if (target == null)
                {
                    ToIdle();
                    anim.SetBool("move", false);
                }
                else if (stateTimer > 3 && target != null)
                {
                    ToPlayerDetected();
                    anim.SetBool("move", false);
                }

                break;
            //CHARGE
            case State.Charge:
                setVelocity(chargeSpeed);
                anim.SetBool("charge", true);
                stateTimer += Time.deltaTime;

                //exit
                if (stateTimer>chargeTime)
                {
                    ToPlayerDetected();
                    anim.SetBool("charge", false);
                }

                break;
            //DETECTED
            case State.PlayerDetected:

                setVelocity(0f);
                anim.SetBool("playerDetected", true);
                stateTimer += Time.deltaTime;

                //exit
                if (stateTimer > 2&&target!=null)
                {
                    ToCharge();
                    anim.SetBool("playerDetected",false);
                }
                else if (target == null)
                {
                    ToIdle();
                    anim.SetBool("playerDetected", false);
                }
                break;
        }
    }
    public void Lookat()
    {
        if (target.transform.position.x <= transform.position.x) //判断目标位置
        {
            transform.Rotate(0f, 180f, 0f);

        }
        else
        {
            transform.Rotate(0f, 0f, 0f);
        }
    }

    void setVelocity(float veclocity)
    {
        if (target != null)
        {
            Vector2 dir = (target.transform.position - transform.position).normalized;
            rigidbody2D.velocity = veclocity * dir;
        }
        else rigidbody2D.velocity=Vector2.zero;

    }
    void ToIdle()
    {
        if (target.transform.position.x <= transform.position.x) Lookat();//判断目标位置
        currentState = State.Idle;
        stateTimer = 0;

    }

    void ToMove()
    {
        if (target.transform.position.x <= transform.position.x) Lookat();//判断目标位置
        currentState = State.Move;
        stateTimer = 0f;
    }

    void ToPlayerDetected()
    {
        //if (target.transform.position.x <= transform.position.x) Flip();
        currentState = State.PlayerDetected;
        stateTimer = 0;
    }

    void ToCharge()
    {
        //if (target.transform.position.x <= transform.position.x) Flip();
        currentState = State.Charge;
        stateTimer = 0f;
    }

}
