using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour
{
    enum State
    {
        Idle,
        Move,
        PlayerDetected
    }

    [Header("Settings")] 

    [SerializeField] float movespeed = 1f;

    [Header("Data")] 

    [SerializeField] State currentState = State.Idle;
    [SerializeField] Health target;

    float stateTimer = 0;
    [SerializeField] 
    float moveTime=4f;

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

    void FixedUpdate()
    {
        target = GetComponent<TargetReceiver>().Target;
        switch (currentState)
        {
            //IDLE
            case State.Idle:
                anim.SetBool("idle", true);
                stateTimer += Time.deltaTime;

                //exit

                if (target != null)
                {
                    ToMove();
                    anim.SetBool("idle", false);
                }

                break;
            //DETECTED
            case State.PlayerDetected:

                setVelocity(0f);
                anim.SetBool("playerDetected", true);
                stateTimer += Time.deltaTime;

                //exit
                if (stateTimer > 1 && target != null)
                {
                    ToMove();
                    anim.SetBool("playerDetected", false);
                }
                else if (target == null)
                {
                    ToIdle();
                    anim.SetBool("playerDetected", false);
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
                else if (stateTimer > moveTime && target != null)
                {
                    ToPlayerDetected();
                    anim.SetBool("move", false);
                }
                break;
        }
        Lookat();
    }
    public void Lookat()
    {
        if (target == null) return;

        if (target.transform.position.x <= transform.position.x) //ÅÐ¶ÏÄ¿±êÎ»ÖÃ
        {
            transform.localEulerAngles = Vector3.up * 180;

        }
        else
        {
            transform.localEulerAngles = Vector3.zero;
        }
    }

    void ToIdle()
    {
        setVelocity(0f);
        currentState = State.Idle;
    }

    void ToMove()
    {
        currentState = State.Move;
        stateTimer = 0f;
    }

    void ToPlayerDetected()
    {
        currentState = State.PlayerDetected;
        stateTimer = 0;
    }

    void setVelocity(float veclocity)
    {
        if (target != null)
        {
            Vector2 dir = (target.transform.position - transform.position).normalized;
            rigidbody2D.velocity = veclocity * dir;
        }
        else rigidbody2D.velocity = Vector2.zero;
    }
}
