using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    //public EnemyData EnemyData;
    public float CurrentHealth = 100;
    public float MaxHealth = 100;
    public GameObject bloodParticle;
    //public GameObject corpseOB;


    public float hurtTimer = 0;


    bool isHurting=false;
    [SerializeField] private List<SpriteRenderer> sps;
    private float flashTime=0.5f;

    [Header("Events")]
    public UnityEvent OnDie = new UnityEvent();

    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = MaxHealth;
    }
    // Update is called once per frame
    void Update()
    {
        if (CurrentHealth <= 0)
        {
            Die();
            Debug.Log("currenthealth<=0");

        }
        if (isHurting)
        {
            hurtTimer += Time.deltaTime;
            Flash(0.5f);

            if (hurtTimer > flashTime)
            {
                hurtTimer = 0f;
                isHurting = false;
                Flash(0f);
            }
        }
    }
    public void TakeDamge(float damage)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, MaxHealth);

        if(bloodParticle!=null) Instantiate(bloodParticle, transform.position, Quaternion.identity);//受伤效果

        Debug.Log("hurt");
        isHurting = true;

    }

    public void Die()
    {
        Debug.Log("die");

        //if(corpseOB!=null) Instantiate(corpseOB,transform.position,Quaternion.identity).GetComponent<CorpseData>().EnemyData = EnemyData;
        OnDie.Invoke();
        Destroy(gameObject);

    }

    //受伤闪烁
    public void Flash(float amount)
    {
        foreach (var sp in sps)
        {
            sp.material.SetFloat("_FlashAmount", amount);
        }
    }
}
