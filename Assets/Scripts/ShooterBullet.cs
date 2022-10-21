using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterBullet : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float damage = 10;
    [SerializeField] float speed = 1;

    [Header("Debug")]
    [SerializeField] Vector2 direction;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)direction * Time.deltaTime * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        Health health = other.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamge(damage);
            Destroy(gameObject);
        }
    }

    public void Initialize(Vector2 dir)
    {
        direction = dir;
    }
}
