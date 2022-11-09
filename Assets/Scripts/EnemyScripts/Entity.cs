using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public FiniteStateMachine stateMachine;

    public D_Entity entityData;
    public int FacingDirection{get; private set;}
    public Rigidbody2D rb { get;private set; }

    public Animator anim { get; private set; }

    public GameObject aliveGo { get; private set; }

    private Vector2 veclocityWorkspace;

    [SerializeField] private Transform wallCheck;

    public virtual void Start()
    {
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
        veclocityWorkspace.Set(FacingDirection*veclocity,rb.velocity.y);
        rb.velocity = veclocityWorkspace;
    }

    public virtual bool CheckWall()
    {
        return Physics2D.Raycast(wallCheck.position, aliveGo.transform.right, entityData.wallCheckDistance,entityData.WhatIsWall);
    }

    public virtual void Flip()
    {
        FacingDirection *= -1;
        aliveGo.transform.Rotate(0f,180f,0f);
    }
}
