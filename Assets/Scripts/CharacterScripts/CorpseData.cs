using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class CorpseData : MonoBehaviour
{
    [SerializeField] private float disappearTimer=0;
    public EnemyData EnemyData;


    void Update()
    {
        if (disappearTimer > 10)
        {
            Destroy(gameObject);
        }
        else
        {
            disappearTimer += Time.deltaTime;
        }
    }
    
}
