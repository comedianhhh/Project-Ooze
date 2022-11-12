using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public FiniteStateMachine stateMachine;

    public D_Entity entityData;
    public int facingDirection{get; private set;}
    public Rigidbody2D rb { get;private set; }

    public Animator anim { get; private set; }

    public GameObject aliveGo { get; private set; }

    private Vector2 veclocityWorkspace;

    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform playerCheck;

    public virtual void Start()
    {
        facingDirection = 1;

        aliveGo = transform.Find("Alive").gameObject;
        rb = aliveGo.GetComponent<Rigidbody2D>();
        anim = aliveGo.GetComponent<Animator>();


        stateMachine = new FiniteStateMachine();
    }

    public virtual void Update()
    {
        stateMachine.currentState.LogicUpdate();
        

    }

    public virtual void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }

    public virtual void SetVelocity(float veclocity)
    {
        veclocityWorkspace.Set(facingDirection*veclocity,rb.velocity.y);
        rb.velocity = veclocityWorkspace;
    }

    public virtual bool CheckWall()
    {
        return Physics2D.Raycast(wallCheck.position, aliveGo.transform.right, entityData.wallCheckDistance,entityData.WhatIsWall);
    }

    //public virtual bool CheckPlayerInMinAgroRange()
    //{
    //    return Physics2D.CircleCast(playerCheck.position, entityData.minAgroDistance, aliveGo.transform.right, 0f,
    //        entityData.WhatIsPlayer);
    //}
    //public virtual bool CheckPlayerInMaxAgroRange()
    //{
    //    return Physics2D.CircleCast(playerCheck.position, entityData.maxAgroDistance, aliveGo.transform.right, 0f,
    //        entityData.WhatIsPlayer);
    //}

    public virtual void Flip()
    {
        facingDirection *= -1;
        aliveGo.transform.Rotate(0f,180f,0f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(wallCheck.position,wallCheck.position+(Vector3)(Vector2.right*facingDirection*entityData.wallCheckDistance));
    }
}
