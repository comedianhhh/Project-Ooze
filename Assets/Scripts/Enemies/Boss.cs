using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Boss : MonoBehaviour
{
    public int damage;
    Health health;
    private float timeBtwDamage = 1.5f;

    public Slider healthBar;
    public Animator anim;

    public GameObject Head1;
    public GameObject Head2;
    Transform playerPos;

    [SerializeField] private int totalProjectiles = 1;
    public GameObject projectiles;
    [SerializeField] private bool RandomSpread;
    [SerializeField] Vector3 Spread = Vector3.zero;
    Vector3 _randomSpreadDirection;

    private void Awake()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        health = GetComponent<Health>();
        anim = GetComponentInChildren<Animator>();
        healthBar.maxValue = health.MaxHealth;

    }
    private void Update()
    {
        if (health.CurrentHealth < health.MaxHealth *0.7&& health.CurrentHealth > health.MaxHealth * 0.5)
        {
            anim.SetTrigger("stageTwo");
        }

        if (health.CurrentHealth <= health.MaxHealth * 0.5)
        {
            anim.SetTrigger("stageThree");
        }

        if (health.CurrentHealth <= 0)
        {
            anim.SetTrigger("death");
        }


        if (timeBtwDamage > 0)
        {
            timeBtwDamage -= Time.deltaTime;
        }

        healthBar.value = health.CurrentHealth;

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Health health = other.GetComponent<Health>();
            if (health != null&&timeBtwDamage<=0)
            {
                health.TakeDamge(damage);
                timeBtwDamage = 1.5f;
            }

        }
    }


    public void PlayAudio(string audio)
    {
        AudioManager.Play(audio);
    }

    public void MoveHead()
    {
        Instantiate(Head1, transform.position+Vector3.right, Quaternion.identity);
        Instantiate(Head2, transform.position - Vector3.right, Quaternion.identity);
        AudioManager.Play("lavaball_large_shoot0",transform.position, 1f);
        AudioManager.Play("lavaball_large_shoot1", transform.position, 1f);

    }

    public void AnimatorAttack()
    {
        Shoot();
    }
    private void Shoot()
    {
        AudioManager.Play("stone_hit");

        for (int i = 0; i < totalProjectiles; i++)
        {
            if (projectiles != null)
            {
                if (RandomSpread)
                {
                    _randomSpreadDirection.x = Random.Range(-Spread.x, Spread.x);
                    _randomSpreadDirection.y = Random.Range(-Spread.y, Spread.y);
                    _randomSpreadDirection.z = Random.Range(-Spread.z, Spread.z);
                }
                else
                {
                    if (totalProjectiles > 1)
                    {
                        _randomSpreadDirection.x = Remap(i, 0, totalProjectiles - 1, -Spread.x, Spread.x);
                        _randomSpreadDirection.y = Remap(i, 0, totalProjectiles - 1, -Spread.y, Spread.y);
                        _randomSpreadDirection.z = Remap(i, 0, totalProjectiles - 1, -Spread.z, Spread.z);
                    }
                    else
                    {
                        _randomSpreadDirection = Vector3.zero;
                    }
                }
               
                Quaternion spread = Quaternion.Euler(_randomSpreadDirection);

                Bullet bullet = Instantiate(projectiles).GetComponent<Bullet>();
                bullet.transform.position = transform.position;
                bullet.Initialize(transform, spread * (playerPos.position - transform.position));
            }
        }

    }
    public static float Remap(float x, float A, float B, float C, float D)
    {
        float remappedValue = C + (x - A) / (B - A) * (D - C);
        return remappedValue;
    }
}
