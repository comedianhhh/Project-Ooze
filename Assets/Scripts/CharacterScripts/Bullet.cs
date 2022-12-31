using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] private float damage = 10;
    [SerializeField] private float speedDecay=0.2f;
    [SerializeField] private float minSpeed = 0.1f;
    [SerializeField] private float startSpeed = 20;
    [SerializeField] private float knockbackDistance = 0.3f;

    [SerializeField] bool isDeflected = false;
    [SerializeField] string targetTag = "Player";

    [SerializeField] private float isEnemyBullet=1;
    bool disappearing;
    public bool canDeflect=false;
    public SpriteRenderer bulletRend;

    Transform sender;
    Transform receiver;
    Vector2 direction;

    public void Initialize(Transform send, Vector2 dir)
    {
        sender = send;
        direction = dir;
        speed = startSpeed;
    }
    public void Initialize(Transform send, Transform receive)
    {
        sender = send;
        receiver = receive;
        speed = startSpeed;
    }


    void FixedUpdate()
    {
        Move();
        CheckDisappear();
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
        transform.position = Vector2.MoveTowards(transform.position, tar, speed * Time.fixedDeltaTime);
        if (speed < minSpeed)
        {
            speed = 0;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            speed = 0; //stop if it hits a wall
        }
        if (isEnemyBullet==1)
        {
            if (collision.CompareTag(targetTag))
            {
                if (collision.GetComponent<Ability>())
                {
                    canDeflect = collision.GetComponent<Ability>().CanDeflect;
                }
                if (canDeflect)
                {
                    canDeflect = false;
                    Deflect();
                }
                else
                    DestroyProjectile();
            }
        }
        //else if(isEnemyBullet==-1)
        //{
        //    if (collision.CompareTag(targetTag))
        //    {
        //        Health health = collision.GetComponent<Health>();
        //        if (health != null)
        //        {
        //            health.TakeDamge(damage);

        //            //knock back
        //            Vector2 difference = (collision.transform.position - transform.position).normalized * knockbackDistance;
        //            health.GetComponent<CharacterMover>().AddExtraVelocity(difference);
        //            //health.AddEffect(new HealthEffect(1, 5));
        //            Destroy(gameObject);
        //        }
        //    }

        //}
        if (collision.CompareTag(targetTag))
        {
            Health health = collision.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamge(damage);
                //knock back
                Vector2 difference = ((Vector2)collision.transform.position - (Vector2)transform.position).normalized * knockbackDistance;
                if (health.GetComponent<CharacterMover>())
                {
                    health.GetComponent<CharacterMover>().AddExtraVelocity(difference);

                }
                //health.AddEffect(new HealthEffect(1, 5));
                Destroy(gameObject);
                AudioManager.Play("TearImpacts1");

            }
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
        isEnemyBullet *=-1;
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
            curAlpha -= disSpeed * Time.fixedDeltaTime; //find new alpha
            disColor.a = curAlpha; //apply alpha to color
            bulletRend.color = disColor; // apply color to bullet
            yield return null;
        } while (curAlpha > 0); //end when the bullet is transparent
        Destroy(gameObject); //get rid of bullet now that it can't be seen
    }

}
