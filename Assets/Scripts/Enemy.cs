using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour
{
    public EnemyData EnemyData = new EnemyData();

    Health health;
    public bool CanBeSwallowed => health.CurrentHealth == 0;

    private void Awake()
    {
        health = GetComponent<Health>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
