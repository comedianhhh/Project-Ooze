using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float CurrentHealth = 100;
    public float MaxHealth = 100;
    public GameObject bloodParticle;
    public GameObject FireDamageParticlePrefab;
    [SerializeField] bool isPlayer=false;
    [SerializeField] bool isBoss=false;
    private float timer;
    [Header("Heal setting")]
    [SerializeField] private float HealRate = 1;
    [SerializeField] private float HealAmount = 1;

    [SerializeField] private List<SpriteRenderer> sps;

    [Header("Effects")]
    [SerializeField] float effectApplyInterval = 0.25f;
    [SerializeField] List<HealthEffect> effects = new List<HealthEffect>();

    [Header("Events")]
    public UnityEvent OnHit = new UnityEvent();
    public UnityEvent OnDie = new UnityEvent();
    public UnityEvent OnDisappear = new UnityEvent();

    Animator anim;
    Coroutine die;
    private Ability ability;

    void Update()
    {
        if (ability!=null)
        {
            if(ability.CanHeal)
            {
                HealthRegenerate();

            }
        }
    }

    void Start()
    {
        CurrentHealth = MaxHealth;
        StartCoroutine(IApplyEffects());
        if (GetComponent<Ability>()) ability = GetComponent<Ability>();
        sps.Add(GetComponentInChildren<SpriteRenderer>());
        anim = GetComponentInChildren<Animator>();
    }

    public void TakeDamge(float damage)
    {

        OnHit.Invoke();

        if (ability != null)
        {
            if (ability.isInvincible||ability.isMagic)
            {
                return;
            }
        }

        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, MaxHealth);
        if(bloodParticle!=null) Instantiate(bloodParticle, transform.position, Quaternion.identity);// ‹…À–ßπ˚

        if (isPlayer)
        {
            AudioManager.Play("minehurt", transform.position, 2.0f);
            GameManager.PlayerHurt();
        }

        else
            AudioManager.Play("splat 2");






        StartCoroutine(Flash(1f, 10));

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }


    void HealthRegenerate()
    {
        if (CurrentHealth < MaxHealth)
        {
            timer += Time.deltaTime;
            if (timer > HealRate)
            {
                CurrentHealth += HealAmount;
                timer = 0;
            }
        }
    }

    public void Die()
    {
        Debug.Log("die");
        OnDie.Invoke();
        if (isBoss)
        {

        }
        else if (isPlayer)
        {

        }
        else
        {
            die = StartCoroutine(IDestroy());
        }
        //Destroy(gameObject);

    }

    public void StopSelfDestroy()
    {
        StopCoroutine(die);
    }

    IEnumerator Flash(float intensity, float speed)
    {
        bool isFadeOut = false;
        float currentAmount = 0;
        Debug.Log("flash");

        anim.SetTrigger("hurt");

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

                if (effects[i].Duration != -1 && Time.time - effects[i].TimeStart >= effects[i].Duration)
                {
                    effects.RemoveAt(i);
                    i--;
                }
            }
        }
    }

    IEnumerator IDestroy()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            OnDisappear.Invoke();
            Destroy(gameObject);
        }
    }
}
