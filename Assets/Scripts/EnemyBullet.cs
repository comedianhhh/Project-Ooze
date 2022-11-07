using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] private float speedDecay=0.2f;

    private Transform player;
    private Vector2 target;

    public void Initialize(Vector2 dir)
    {
        target = dir;
    }



    void Update()
    {
        Move();
    }

    void Move()
    {
        speed -= speedDecay * speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, target , speed * Time.deltaTime);
        if (transform.position.x == target.x && transform.position.y == target.y)
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
