using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicMushroom : Enemy
{
    private float moveRate = 2.0f;
    private float moveTimer;

    [SerializeField] private float minX, MaxX, minY, MaxY;

    protected override void Move()
    { 
        //base.Move();
        RandomMove();
    }

    private void RandomMove()
    {
        moveTimer += Time.deltaTime;

        if (moveTimer > moveRate)
        {
            transform.position = new Vector3(Random.Range(minX, MaxX), Random.Range(minY, MaxY),0);
            moveTimer = 0;
        }
    }
}
