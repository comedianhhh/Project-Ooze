using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Settings")]

    [Header("Data")] 
    [SerializeField] float speed;




    Vector2 input;

    Animator animator;
    Shooter shooter;
    CharacterMover characterMover;


    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        shooter = GetComponent<Shooter>();
        characterMover = GetComponent<CharacterMover>();

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
        //rig.velocity = input.normalized * speed;
        characterMover.Move(input.normalized * speed);


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
        if (Input.GetMouseButtonDown(0))
        {
            shooter.Shoot(direction);
        }
    }



}
