using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swallower : MonoBehaviour
{
    public List<EnemyData> SwallowedEnemies = new List<EnemyData>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Health health = other.GetComponent<Health>();
        if (health.EnemyData.Type != EnemyType.None && health.CurrentHealth / health.MaxHealth <= 0.1f)
        {
            // todo: swallow animation, enemy dies
            health.Die();
            SwallowedEnemies.Add(health.EnemyData);
        }
    }
}
