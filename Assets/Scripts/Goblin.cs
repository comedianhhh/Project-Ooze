using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Enemy
{
    // Start is called before the first frame update
    public Transform wayPoint01, wayPoint02;
    [SerializeField] private Transform wayPointTarget;

    private void Awake()
    {
        wayPointTarget = wayPoint01;
    }

    // Update is called once per frame
    protected override void Move()
    {
        base.Move();
        //patrol
        if (Vector2.Distance(transform.position, target.position) > distance)
        {
            if (Vector2.Distance(transform.position, wayPoint01.position) < 0.01f)
            {
                wayPointTarget = wayPoint02;
            }

            if (Vector2.Distance(transform.position,wayPoint02.position)<0.01f)
            {
                wayPointTarget = wayPoint01;
            }

            transform.position = Vector2.MoveTowards(transform.position, wayPointTarget.position, moveSpeed * Time.deltaTime);
        }
    }
}
