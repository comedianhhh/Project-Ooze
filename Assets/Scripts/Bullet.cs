using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] private float speedDecay=0.2f;
    [SerializeField] bool isDeflected = false;
    [SerializeField] string targetTag = "Player";

    Transform sender;
    Transform receiver;
    Vector2 direction;

    public void Initialize(Transform send, Vector2 dir)
    {
        sender = send;
        direction = dir;
    }
    public void Initialize(Transform send, Transform receive)
    {
        sender = send;
        receiver = receive;
    }


    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        Vector2 tar;
        if (isDeflected)
        {
            if (receiver != null)
                tar = sender.transform.position;
            else
                tar = transform.position + (Vector3)direction;
        }
        else
        {
            if (receiver != null)
                tar = receiver.transform.position;
            else
                tar = transform.position + (Vector3)direction;
        }

        speed -= speedDecay * speed * Time.fixedDeltaTime;
        transform.position = Vector2.MoveTowards(transform.position, tar, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(targetTag))
        {
            if (collision.GetComponent<Deflector>())
            {
                Deflect();
            }
            else
               DestroyProjectile();
        }
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    public void Deflect()
    {
        isDeflected = true;
        direction = -direction;
        targetTag = "Enemy";
    }
}
