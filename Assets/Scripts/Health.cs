using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public EnemyData EnemyData;
    public float CurrentHealth = 100;
    public float MaxHealth = 100;

    public float hurtTimer = 0;


    bool isHurting=false;
    [SerializeField] private List<SpriteRenderer> sps;
    private float flashTime=0.5f;

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
        Debug.Log("hurt");
        isHurting = true;

    }

    public void Die(bool isSwallowed = false)
    {
        Destroy(gameObject);
    }

    // ‹…À…¡À∏
    public void Flash(float amount)
    {
        foreach (var sp in sps)
        {
            sp.material.SetFloat("_FlashAmount", amount);
        }
    }
}
