using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Goblin : MonoBehaviour
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

    [SerializeField] float knockbackDistance=0.5f;

    [SerializeField] LayerMask layerMask;

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

    private void Awake()
    {
        aliveGo = transform.Find("Alive").gameObject;
        //rigidbody2D = GetComponent<Rigidbody2D>();
        anim = aliveGo.GetComponent<Animator>();
        enemyMover = GetComponent<CharacterMover>();
    }

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
                if (target != null)
                {
                    ToPlayerDetected();
                    anim.SetBool("idle", false);
                }
                break;
            //DETECTED
            case State.PlayerDetected:
                Lookat();
                setVelocity(0f);
                stateTimer += Time.deltaTime;
                anim.SetBool("detect", true);

                //exit
                if (stateTimer > 1 && target != null)
                {
                    ToMove();
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
                else if (target!=null&&isPlayerInRange&&stateTimer>1)
                {
                    ToAttack();
                    anim.SetBool("move", false);
                }
                break;
            //ATK
            case State.Attack:
                anim.SetBool("attack",true);
                break;
            //Die
            case State.Die:
                setVelocity(0f);
                break;
        }
        
        
    }
    public void Lookat()
    {
        if (target == null) return;

        if (transform.position.x- target.transform.position.x>=0.1f) //�ж�Ŀ��λ��
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
            Vector2 dir = (target.transform.position - transform.position).normalized;
            enemyMover.Move(veclocity* dir);
        }
        else enemyMover.Move(Vector2.zero);
    }

    void ToIdle()
    {
        setVelocity(0f);
        currentState = State.Idle;
    }

    public void ToMove()
    {
        anim.SetBool("attack", false);
        //Debug.Log("ToMove");
        currentState = State.Move;
        stateTimer = 0f;
    }

    void ToPlayerDetected()
    {
        currentState = State.PlayerDetected;
        stateTimer = 0;
    }

    void ToAttack()
    {
        setVelocity(0f);
        currentState = State.Attack;
    }

    public void ToDie()
    {
        currentState = State.Die;
    }
    public void AnimatorAttack()
    {
        var tar =Physics2D.OverlapCircle(atk.position, atkRange,layerMask);

        if (tar!= null&&tar.gameObject.tag=="Player")
        {
            tar.gameObject.GetComponent<Health>().TakeDamge(atkAmount);
            Vector2 difference = (tar.transform.position - transform.position).normalized * knockbackDistance;
            tar.GetComponent<CharacterMover>().AddExtraVelocity(difference);
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(atk.position, atkRange);

    }
    void DetectTargetinRange()
    {
        var tarColliders = Physics2D.OverlapCircle(atk.position, atkRange, layerMask);
        if (tarColliders!=null)
        {
            isPlayerInRange = true;
        }
        else
        {
            isPlayerInRange = false;
        }
    }

}
