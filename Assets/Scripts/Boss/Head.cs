using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Head : MonoBehaviour
{
    Transform playerPos;
    GameObject Boss;


    [SerializeField] private int totalProjectiles = 1;
    public GameObject projectiles;
    [SerializeField] private bool RandomSpread;
    [SerializeField] Vector3 Spread = Vector3.zero;
    Vector3 _randomSpreadDirection;
    float interval = 4f;

    bool isAlive=true;

    private void Awake()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        Boss = GameObject.FindGameObjectWithTag("Enemy");

    }


    private void Update()
    {
        if (Boss.GetComponent<Health>().CurrentHealth <= 0)
        {
            isAlive = false;
        }
        
        if (interval > 0&&isAlive)
        {
            interval -= Time.deltaTime;
        }
        else if(isAlive&&interval<=0)
        {
            Shoot();
            interval = 4f;
        }
    }


    private void Shoot()
    {
        AudioManager.Play("stone_hit");

        for (int i = 0; i < totalProjectiles; i++)
        {
            if (projectiles != null)
            {
                if (RandomSpread)
                {
                    _randomSpreadDirection.x = Random.Range(-Spread.x, Spread.x);
                    _randomSpreadDirection.y = Random.Range(-Spread.y, Spread.y);
                    _randomSpreadDirection.z = Random.Range(-Spread.z, Spread.z);
                }
                else
                {
                    if (totalProjectiles > 1)
                    {
                        _randomSpreadDirection.x = Remap(i, 0, totalProjectiles - 1, -Spread.x, Spread.x);
                        _randomSpreadDirection.y = Remap(i, 0, totalProjectiles - 1, -Spread.y, Spread.y);
                        _randomSpreadDirection.z = Remap(i, 0, totalProjectiles - 1, -Spread.z, Spread.z);
                    }
                    else
                    {
                        _randomSpreadDirection = Vector3.zero;
                    }
                }

                Quaternion spread = Quaternion.Euler(_randomSpreadDirection);

                Bullet bullet = Instantiate(projectiles).GetComponent<Bullet>();
                bullet.transform.position = transform.position;
                bullet.Initialize(transform, spread * (playerPos.position - transform.position));
            }
        }

    }
    public static float Remap(float x, float A, float B, float C, float D)
    {
        float remappedValue = C + (x - A) / (B - A) * (D - C);
        return remappedValue;
    }
}
