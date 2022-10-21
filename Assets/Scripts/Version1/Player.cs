using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public GameObject bulletPrefab;
    protected Transform muzzlePos;
    protected Vector2 direction;
    [SerializeField]protected float speed;
    protected float health;


    private Vector2 input;
    public Rigidbody2D rig;
    private Animator animator;
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();

    }


    void Update()
    {
        move();
        //Debug .Log();
    }
    void move()
    {
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");

        rig.velocity = input.normalized * speed;
        if (input.x > 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        else if (input.x < 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }

        if (input != Vector2.zero)
        
            animator.SetBool("isMoving",true);
        else 
            animator.SetBool("isMoving",false);

    }
}
