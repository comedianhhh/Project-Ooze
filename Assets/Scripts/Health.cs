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

    [Header("Effects")]
    [SerializeField] float effectApplyInterval = 0.25f;
    [SerializeField] List<HealthEffect> effects = new List<HealthEffect>();

    [Header("Events")]
    public UnityEvent OnHit = new UnityEvent();
    public UnityEvent OnDie = new UnityEvent();

    Rigidbody2D rigidbody2D;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = MaxHealth;
        StartCoroutine(IApplyEffects());
    }
    // Update is called once per frame
    void Update()
    {

        /*if (isHurting)
        {
            hurtTimer += Time.deltaTime;
            Flash(0.5f);

            if (hurtTimer > flashTime)
            {
                hurtTimer = 0f;
                isHurting = false;
                Flash(0f);
            }
        }*/
    }
    public void TakeDamge(float damage)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, MaxHealth);

        if(bloodParticle!=null) Instantiate(bloodParticle, transform.position, Quaternion.identity);// ‹…À–ßπ˚

        Debug.Log("hurt");
        //isHurting = true;

        StartCoroutine(Flash(1, 10));

        if (CurrentHealth <= 0)
        {
            Die();
            Debug.Log("currenthealth<=0");
        }
    }

    public void Die()
    {
        Debug.Log("die");

        //if(corpseOB!=null) Instantiate(corpseOB,transform.position,Quaternion.identity).GetComponent<CorpseData>().EnemyData = EnemyData;
        OnDie.Invoke();
        Destroy(gameObject);

    }

    IEnumerator Flash(float intensity, float speed)
    {
        bool isFadeOut = false;
        float currentAmount = 0;

        while (true)
        {
            currentAmount += isFadeOut ? -intensity * Time.deltaTime * speed : intensity * Time.deltaTime * speed;

            if (!isFadeOut && currentAmount >= 1)
                isFadeOut = true;

            if (isFadeOut && currentAmount <= 0)
                break;

            foreach (var sp in sps)
                sp.material.SetFloat("_FlashAmount", currentAmount);

            yield return new WaitForEndOfFrame();
        }
    }

    public void AddEffect(HealthEffect effect)
    {
        if (effect.Type != HealthEffect.HeathEffectType.None)
        {
            int index = effects.FindIndex(e => e.Type == effect.Type);
            if (index > 0) // has found same type effect
            {
                //effects[index] = effect;
                effects[index] = effect.Duration > effects[index].Duration ? effect : effects[index];
            }
        }
        effects.Add(effect);
    }

    IEnumerator IApplyEffects()
    {
        while (true)
        {
            yield return new WaitForSeconds(effectApplyInterval);
            for (int i = 0; i < effects.Count; i++)
            {
                TakeDamge(effects[i].DamagePerSecond * effectApplyInterval);
                Debug.Log("Take Damage: " + effects[i].DamagePerSecond);
                // other health count

                if (Time.time - effects[i].TimeStart >= effects[i].Duration)
                {
                    effects.RemoveAt(i);
                    i--;
                }
            }

            
        }
    }
}
