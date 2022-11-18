using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] private float speedDecay=0.2f;

    private Vector2 target;

    public void Initialize(Vector2 tar)
    {
        target = tar;
    }



    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        speed -= speedDecay * speed * Time.fixedDeltaTime;
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if ((Vector2)transform.position == target)
        {
            DestroyProjectile();
        }

    }

    void OntriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DestroyProjectile();
        }
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
