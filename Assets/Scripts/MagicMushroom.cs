using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicMushroom : MonoBehaviour
{
    enum State
    {
        Move,
        PlayerDetected,
        Idle,
        Attack,
        Die
    }

    [Header("Settings")]

    [SerializeField] float movespeed = 1f;

    [SerializeField] private float atkRange = 1f;
    [SerializeField] private float atkAmount = 5f;
    [SerializeField] private int totalProjectiles = 1;

    [SerializeField] LayerMask layerMask;
    public GameObject projectiles;
    [SerializeField] private bool RandomSpread;
    [SerializeField] Vector3 Spread = Vector3.zero;



    [Header("Data")]

    [SerializeField] State currentState = State.Idle;
    [SerializeField] Health target;

    [SerializeField] bool isPlayerInRange;
    [SerializeField] bool isUnderGround;

    Vector3 _randomSpreadDirection;
    Vector3 _spawnPositionCenter;


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
            //MOVE
            case State.Move:
                Lookat();
                DetectTargetinRange();
                stateTimer += Time.fixedDeltaTime;
                
                anim.SetBool("move", true);

                if(target==null)
                    setVelocity(0f);
                else
                    setVelocity(movespeed);

                if (target != null && isPlayerInRange && stateTimer > 1)
                {
                    ToPlayerDetected();
                    anim.SetBool("move", false);
                }
                break;

            //DETECTED
            case State.PlayerDetected:
                setVelocity(0f);
                stateTimer += Time.deltaTime;
                anim.SetBool("detect", true);

                //exit
                if (stateTimer > 0.5f)
                {
                    ToIdle();
                    anim.SetBool("detect", false);
                }

                break;

            //IDLE
            case State.Idle:
                setVelocity(0f);
                anim.SetBool("idle", true);
                stateTimer += Time.deltaTime;
                //exit
                if (target != null&&stateTimer>2f)
                {
                    ToAttack();
                    anim.SetBool("idle", false);
                }
                break;

            //ATK
            case State.Attack:
                anim.SetBool("attack", true);
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
            Vector2 dir = (target.transform.position - transform.position).normalized;
            enemyMover.Move(veclocity * dir);
        }
        else enemyMover.Move(Vector2.zero);
    }

    public void ToIdle()
    {
        setVelocity(0f);
        currentState = State.Idle;
        stateTimer = 0;
    }

    public void ToMove()
    {
        isUnderGround = false;
        currentState = State.Move;
        stateTimer = 0f;
    }

    void ToPlayerDetected()
    {
        isUnderGround = false;
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
        Shoot();
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

    private void Shoot()
    {
        for (int i = 0; i < totalProjectiles; i++)
        {
            if (projectiles != null)
            {
                if (RandomSpread)
                {
                    _randomSpreadDirection.x = UnityEngine.Random.Range(-Spread.x, Spread.x);
                    _randomSpreadDirection.y = UnityEngine.Random.Range(-Spread.y, Spread.y);
                    _randomSpreadDirection.z = UnityEngine.Random.Range(-Spread.z, Spread.z);
                }
                else
                {
                    if (totalProjectiles > 1)
                    {
                        _randomSpreadDirection.x = Remap(i, 0, totalProjectiles - 1, -Spread.x, Spread.x);
                        _randomSpreadDirection.y = Remap(i, 0, totalProjectiles - 1, -Spread.y, Spread.y);
                        _randomSpreadDirection.z = Remap(i, 0, totalProjectiles - 1, -Spread.z, Spread.z);
                    }
                    else
                    {
                        _randomSpreadDirection = Vector3.zero;
                    }
                }

                Quaternion spread = Quaternion.Euler(_randomSpreadDirection);

                Bullet bullet = Instantiate(projectiles).GetComponent<Bullet>();
                bullet.transform.position = transform.position;
                bullet.Initialize(transform, spread * transform.right);

            }
        }
        
    }
    public static float Remap(float x, float A, float B, float C, float D)
    {
        float remappedValue = C + (x - A) / (B - A) * (D - C);
        return remappedValue;
    }

}
