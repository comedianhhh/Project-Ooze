using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private string enemyName;
    [SerializeField] private protected float moveSpeed;

    [SerializeField] private protected Transform target;
    [SerializeField] private protected float distance;
    protected virtual void introduction()
    {
        Debug.Log("my name is " + enemyName + "moveSpped:" + moveSpeed);
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    protected virtual void Start()
    {
        introduction();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Move();
        TurnDirection();
        Attack();
    }

    protected  virtual void Move()
    {
        if (Vector2.Distance(transform.position, target.position) < distance)
        {

         transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }
    }

    void TurnDirection()
    {
        if (transform.position.x > target.position.x)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
        else if (transform.position.x < target.position.x)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
    }

    protected virtual void Attack()
    {
        Debug .Log("is attacking");
    }


}
