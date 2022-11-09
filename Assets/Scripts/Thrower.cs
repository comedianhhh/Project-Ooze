using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrower : Goblin
{

    public GameObject projectiles;

    [SerializeField]
    private float shotRate = 2.0f;
    private float shotTimer=2f;
    public float stoppingDistance;
    public float retreatDistance;

    [SerializeField] private bool isAttack=false;

    //protected override void Attack()
    //{ 
    //    if (shotRate <= 0)
    //    {
    //        isAttack = true;
    //        EnemyBullet bullet = Instantiate(projectiles).GetComponent<EnemyBullet>();
    //        bullet.transform.position = transform.position; 
    //        bullet.Initialize(target.position - transform.position);
    //        shotRate = shotTimer;
    //    }
    //    else
    //    {
    //        shotRate -= Time.deltaTime;
    //    }
    //}

    //protected override void Update()
    //{

    //    if (Vector2.Distance(transform.position, target.position) > distance)
    //    {
    //        Patrol();
    //    }
    //    else if(Vector2.Distance(transform.position, target.position) < stoppingDistance)
    //    {
    //        Attack();
    //    }
    //    else if (Vector2.Distance(transform.position, target.position) < distance &&
    //             Vector2.Distance(transform.position, target.position) > stoppingDistance&& isAttack!=true)
    //    {
    //        Move();
    //    }
        
    //}

    //protected override void Move()
    //{

    //    transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

    //    if (Vector2.Distance(transform.position, target.position) < stoppingDistance && Vector2.Distance(transform.position, target.position) > retreatDistance)
    //    {
    //        transform.position = this.transform.position;

    //    }
    //    else if (Vector2.Distance(transform.position, target.position) < retreatDistance)
    //    {

    //        transform.position = Vector2.MoveTowards(transform.position, target.position, -moveSpeed * Time.deltaTime);
    //    }

    //}

    //protected override void Patrol()
    //{
    //    base.Patrol();
    //}

}
