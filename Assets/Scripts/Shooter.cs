using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform muzzlePos;

    protected float range;
    //How long the character's tears will travel before landing.
    protected float damage;
    protected float bubbles;
    //The character's rate of fire. The lower the number, the faster tears are fired.
    protected float shotspeed;
    //How quickly the character's tears travel.

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void Shoot(Vector2 direction)
    {
        Bullet bullet = Instantiate(bulletPrefab).GetComponent<Bullet>();
        bullet.transform.position = transform.position;
        bullet.Initialize(transform,direction);
    }
}
