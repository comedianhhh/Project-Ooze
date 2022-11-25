using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class Thrower : MonoBehaviour
{
    enum State
    {
        Idle,
        PlayerDetected,
        Attack,
        Die
    }
    [Header("Settings")]

    [Header("Data")]

    [SerializeField] State currentState = State.Idle;
    [SerializeField] Health target;

    public GameObject projectiles;

    [SerializeField]
    float stateTimer = 0;



    GameObject aliveGo;
    Animator anim;
    public Transform attackpos;
    CharacterMover enemyMover;


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
                anim.SetBool("idle", true);
                //exit
                if (target != null)
                {
                    ToPlayerDetected();
                    anim.SetBool("idle", false);
                }

                break;

            //DETECTED
            case State.PlayerDetected:

                //setVelocity(0f);
                stateTimer += Time.deltaTime;

                //exit
                if (stateTimer > 1 && target != null)
                {
                    ToAttack();
                }
                else if (target == null)
                {
                    ToIdle();
                }
                break;
            //Attack
            case State.Attack:
                if (stateTimer <1)
                {
                    Bullet bullet = Instantiate(projectiles).GetComponent<Bullet>();
                    bullet.transform.position = attackpos.position;
                    bullet.Initialize(transform, target.transform);
                    //Debug.DrawLine(target.transform.position, attackpos.position, Color.red, 1);
                    stateTimer = 2f;
                }
                stateTimer -= Time.fixedDeltaTime;

                if (target == null)
                {
                    ToIdle();
                }

                break;
        }
        Lookat();
    }

    public void Die()
    {
        currentState = State.Die;
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
        //setVelocity(0f);
        currentState = State.Idle;
    }


    void ToPlayerDetected()
    {
        currentState = State.PlayerDetected;
        stateTimer = 0f;
    }

    void ToAttack()
    {
        currentState = State.Attack;
        stateTimer = 0f;
    }
}
