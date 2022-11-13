using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterBullet : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float damage = 10;
    [SerializeField] private float speedDecay = 2f;
    [SerializeField] private float minSpeed = 0.1f;
    [SerializeField] private float startSpeed = 20;
    [SerializeField] float speed;
    [SerializeField] private float knockbackDistance=0.3f;
    [Header("Debug")]
    [SerializeField] Vector2 direction;

    float knockbackTIme = 0.2f;
    float knockbackSpeed = 20f;


    bool disappearing;
    public SpriteRenderer bulletRend;

    void Update()
    {
        Move();
        CheckDisappear();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        Health health = other.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamge(damage);

            //knock back
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            Vector2 difference = (other.transform.position - transform.position).normalized*knockbackDistance;
            rb.MovePosition(new Vector2(other.transform.position.x+difference.x,other.transform.position.y+difference.y));

            Destroy(gameObject);
        }
    }
    void Move()
    {
        speed -= speedDecay * speed * Time.deltaTime; //slow down the bullet over time
        if (speed < minSpeed)
        {
            speed = 0; //clamp down speed so it doesnt take too long to stop
        }
        Vector2 tempPos = transform.position; //capture current position
        tempPos += direction * speed * Time.deltaTime; //find new position
        transform.position = tempPos; //update position
    }

    public void Initialize(Vector2 dir)
    {
        direction = dir;
        speed = startSpeed;
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            speed = 0; //stop if it hits a wall
        }
    }
    void CheckDisappear()
    {
        if (speed == 0 && !disappearing)
        { //disappear and destroy when stopped
            disappearing = true; //so we dont continuelly call the coroutine
            StartCoroutine(Disappear());
        }
    }
    IEnumerator Disappear()
    {
        float curAlpha = 1; //start at full alpha
        float disSpeed = 3f; //take 1/3 seconds to disappear
        Color disColor = bulletRend.color; //capture color to edit its alpha
        do
        {
            curAlpha -= disSpeed * Time.deltaTime; //find new alpha
            disColor.a = curAlpha; //apply alpha to color
            bulletRend.color = disColor; // apply color to bullet
            yield return null;
        } while (curAlpha > 0); //end when the bullet is transparent
        Destroy(gameObject); //get rid of bullet now that it can't be seen
    }

}
