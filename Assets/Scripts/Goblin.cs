using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Enemy
{
    private float waitTime;
    public float starWaitTime;

    public Transform moveSpot;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    //public Transform wayPoint01, wayPoint02;
    //[SerializeField] public Transform wayPointTarget;

    //private void Awake()
    //{
    //    wayPointTarget = wayPoint01;
    //}
    protected override void Update()
    {
        Move();
        Attack();
        Patrol();
    }
    protected  override void Start()
    {
        base.Start();
        waitTime = starWaitTime;

        moveSpot.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }
    protected override void Move()
    {
        //chase
        if (Vector2.Distance(transform.position, target.position) < distance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }

        //if (Vector2.Distance(transform.position, target.position) > distance)
        //{
        //    if (Vector2.Distance(transform.position, wayPoint01.position) < 0.01f)
        //    {
        //        wayPointTarget = wayPoint02;
        //    }

        //    if (Vector2.Distance(transform.position,wayPoint02.position)<0.01f)
        //    {
        //        wayPointTarget = wayPoint01;
        //    }

        //    transform.position = Vector2.MoveTowards(transform.position, wayPointTarget.position, moveSpeed * Time.deltaTime);
        //}
    }

    protected virtual void Patrol()
    {
        if (Vector2.Distance(transform.position, target.position) > distance)
        {
            transform.position = Vector2.MoveTowards(transform.position, moveSpot.position, moveSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, moveSpot.position) < 0.2f)
            {
                if (waitTime <= 0)
                {
                    moveSpot.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
                    waitTime = starWaitTime;
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
        }
    }
    protected override void Attack()
    {

    }
}
