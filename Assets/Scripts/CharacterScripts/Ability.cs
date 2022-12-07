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


    private float Poisontimer;

    public bool CanPoison;
    public bool CanDeflect;
    public bool CanHeal;

    public bool isMagic;
    public bool isInvincible;


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
        ChangeInvincible();
        if(CanPoison) ReleasePoison();

        if (isMagic && MagicTime < 3)
        {
            ReleaseSpark();
            MagicTime += Time.deltaTime;
        }
        else if (MagicTime >= 3)
        {
            spark.SetActive(false);
            isMagic = false;
            MagicTime = 0f;
        }

    }

    void ChangeInvincible()
    {
        isStill = mover.GetStillState();

        if (isStill && !isInvincible)
        {
            stillTimer += Time.deltaTime;
            if (stillTimer > 2)
            {
                isInvincible = true;
                stillTimer = 0f;
            }
        }
        else if (!isStill)
        {
            isInvincible = false;
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
                 health.GetComponent<CharacterMover>().AddExtraVelocity(difference);
            }
        }
     }

     void  ReleaseSpark()
     {
         spark.SetActive(true);
     }



}
