using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    [SerializeField] Vector2 extraVelocity = Vector2.zero;
    [SerializeField] float extraVelocityDecay = 1;
    bool canMove=true;

    Rigidbody2D rigidbody2D;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        extraVelocity -= extraVelocity.normalized * Time.deltaTime * extraVelocityDecay;

    }

    public void Move(Vector2 velocity)
    {
        //Vector2 dir = (target - (Vector2)transform.position).normalized;
        if (canMove)
        {
            rigidbody2D.velocity = velocity + extraVelocity;

        }
        else
        {
            rigidbody2D.velocity=Vector2.zero;
        }
    }

    public void AddExtraVelocity(Vector2 velocity)
    {
        extraVelocity += velocity;
    }

    public void Freeze()
    {
        canMove = false;
    }

    public void Unfreeze()
    {
        canMove = true;
    }
}
