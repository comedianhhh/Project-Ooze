using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float health;

    [Header("Data")]
    [SerializeField] float speed;

    private Vector2 input;

    private Rigidbody2D rig;
    private Animator animator;
    Shooter shooter;
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        shooter = GetComponent<Shooter>();
    }


    void Update()
    {
        Move();
        Aim();
    }
    void Move()
    {
        // movement
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");
        rig.velocity = input.normalized * speed;

        // face towards
        if (input.x > 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        else if (input.x < 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }

        // animate
        animator.SetBool("isMoving", input != Vector2.zero);
    }
    void Aim()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;
        Vector3 direction = mouseWorldPos - transform.position;
        //Debug.Log(direction);
        if (Input.GetMouseButtonDown(0))
            shooter.Shoot(direction);
    }
}
