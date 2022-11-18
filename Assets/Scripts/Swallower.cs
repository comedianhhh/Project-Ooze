using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swallower : MonoBehaviour
{
    public List<EnemyData> SwallowedEnemies = new List<EnemyData>();


    private void OnTriggerEnter2D(Collider2D other)
    {
        var enemy = other.GetComponent<Enemy>();
        if (enemy != null && enemy.EnemyData.Type != EnemyType.None)
        {
            // todo: swallow animation, enemy dies
            SwallowedEnemies.Add(enemy.EnemyData);
            Destroy(other.gameObject);
        }
    }
}
