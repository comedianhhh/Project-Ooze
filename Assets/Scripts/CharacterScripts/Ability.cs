using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class Ability : MonoBehaviour
{

    [Header("Settings")]
    [SerializeField] GameObject poison;
    [SerializeField] private GameObject spark;

    [SerializeField] private float MagicDamge;
    [SerializeField] private float MagicTime;
    [SerializeField] private float knockbackDistance;

    [SerializeField] float SpeedUp;

    [SerializeField] float StateTime=8f;

    [SerializeField] GameObject cyc;
    [SerializeField] GameObject magic;
    [SerializeField] GameObject mr;
    [SerializeField] GameObject grass;
    [SerializeField] GameObject gob;

 

    private float Poisontimer;

    public bool CanBeGrass;
    public bool CanPoison;
    public bool CanDeflect;
    public bool CanHeal;
    public bool CanSpeedUp;
    public bool isMagic;
    public bool isInvincible;


    [SerializeField] float stateTimer=20f;

    [SerializeField] private float stillTimer;
    private bool isStill;
    private CharacterMover mover;


    void Awake()
    {
        mover = GetComponent<CharacterMover>();
        spark.SetActive(false);
        MagicTime = 0f;
    }
    void Update()
    {
        if (CanSpeedUp&&StateTime>0)
        {
            BeCyc();
            Player.instance.speed = SpeedUp;
            StateTime -= Time.deltaTime;
        }else if (CanSpeedUp && StateTime <= 0)
        {
            CanSpeedUp = false;
            StateTime = stateTimer;
            BeNormal();
        }


        if (CanBeGrass&& StateTime>0)
        {
            ChangeInvincible();
            StateTime -= Time.deltaTime;

        }
        else if(CanBeGrass&& StateTime <= 0)
        {
            CanBeGrass = false;
            StateTime = stateTimer;
            BeNormal();
        }

        if (isMagic && MagicTime < 5)
        {
            BeMagic();
            ReleaseSpark();
            MagicTime += Time.deltaTime;
        }
        else if (MagicTime >= 5)
        {
            spark.SetActive(false);
            isMagic = false;
            BeNormal();
            MagicTime = 0f;
        }
 

        if (CanPoison && StateTime >0)
        {
            BeMr();
            StateTime -= Time.deltaTime;
            ReleasePoison();
        }
        else if(CanPoison && StateTime <= 0)
        {
            CanPoison = false;
            BeNormal();
            StateTime = stateTimer;
        }

        if (CanDeflect && StateTime > 0)
        {
            BeGob();
            StateTime -= Time.deltaTime;
        }else if (CanDeflect && StateTime <= 0)
        {
            BeNormal();
            StateTime = stateTimer;
            CanDeflect = false;
        }

    }

    void ChangeInvincible()
    {
        isStill = mover.GetStillState();

        if (isStill && !isInvincible)
        {
            stillTimer += Time.deltaTime;
            if (stillTimer > 1)
            {
                BeGrass();
                isInvincible = true;
                stillTimer = 0f;
            }
        }
        else if (!isStill)
        {
            isInvincible = false;
            BeNormal();
        }
    }
     void ReleasePoison()
    {
        Poisontimer += Time.deltaTime;
        if (Poisontimer > 4)
        {
            Instantiate(poison, transform.position, Quaternion.identity);
            Poisontimer = 0;
        }
    }

     void OnTriggerEnter2D(Collider2D other)
     {
         if (isMagic&&other.CompareTag("Enemy"))
         {
             Health health = other.GetComponent<Health>();
             if (health != null)
             {
                 health.TakeDamge(MagicDamge);
                 Vector2 difference = (other.transform.position - transform.position).normalized * knockbackDistance;
                if (health.GetComponent<CharacterMover>())
                {
                    health.GetComponent<CharacterMover>().AddExtraVelocity(difference);
                }
                 
            }
        }
     }

     void  ReleaseSpark()
     {
         spark.SetActive(true);
     }
    public void BeMagic()
    {
        magic.SetActive(true);
        cyc.SetActive(false);
        grass.SetActive(false);
        mr.SetActive(false);
        gob.SetActive(false);
    }
    public void BeGob()
    {
        magic.SetActive(false);
        cyc.SetActive(false);
        grass.SetActive(false);
        mr.SetActive(false);
        gob.SetActive(true);
    }
    public void BeCyc()
    {
        magic.SetActive(false);
        cyc.SetActive(true);
        grass.SetActive(false);
        mr.SetActive(false);
        gob.SetActive(false);
    }
    public void BeMr()
    {
        magic.SetActive(false);
        cyc.SetActive(false);
        grass.SetActive(false);
        mr.SetActive(true);
        gob.SetActive(false);
    }
    public void BeGrass()
    {
        magic.SetActive(false);
        cyc.SetActive(false);
        grass.SetActive(true);
        mr.SetActive(false);
        gob.SetActive(false);
    }
    public void BeNormal()
    {
        magic.SetActive(false);
        cyc.SetActive(false);
        grass.SetActive(false);
        mr.SetActive(false);
        gob.SetActive(false);
    }
}
