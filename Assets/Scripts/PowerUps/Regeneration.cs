using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regeneration : MonoBehaviour
{
    [Header("Settings")] 

    [SerializeField] float regenRate;
    [SerializeField] float regenAmount;
    [SerializeField] private float MaxAmount;


    private float timer;
    private Health health;

    void Awake()
    {
        health = GetComponent<Health>();
    }

    void Update()
    {
        if (health.CurrentHealth < MaxAmount)
        {
            timer += Time.deltaTime;
            if (timer > regenRate)
            {
                health.CurrentHealth += regenAmount;
                timer = 0;
            }
        }
    }

}
