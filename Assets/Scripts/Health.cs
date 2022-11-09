using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public EnemyData EnemyData;
    public float CurrentHealth = 100;
    public float MaxHealth = 100;

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
    }

    public void TakeDamge(float damage)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, MaxHealth);
    }

    public void Die(bool isSwallowed = false)
    {
        Destroy(gameObject);
    }
}
