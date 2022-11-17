using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swallower : MonoBehaviour
{
    public List<EnemyData> SwallowedEnemies = new List<EnemyData>();


    private void OnTriggerEnter2D(Collider2D other)
    {
        CorpseData data = other.GetComponent<CorpseData>();
        if (data.EnemyData.Type != EnemyType.None)
        {
            // todo: swallow animation, enemy dies
            SwallowedEnemies.Add(data.EnemyData);
            Destroy(other.gameObject);
        }
    }
}
