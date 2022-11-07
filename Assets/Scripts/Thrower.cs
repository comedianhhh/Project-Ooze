using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrower : Goblin
{
    private float shotRate = 2.0f;
    private float shotTimer;
    public GameObject projectiles;
    protected override void Attack()
    {
        base.Attack();
        if (shotTimer > shotRate)
        {
            Instantiate(projectiles, transform.position, Quaternion.identity);
        }
    }
}
