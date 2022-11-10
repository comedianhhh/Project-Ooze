using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Entity
{

    public E1_IdleState idleState { get; private set; }
    public E1_MoveState moveState { get; private set; }
    public E1_PlayerDetectedState playerDetectedState { get; private set; }

    public E1_ChargeState chargeState { get; private set; }


    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_MoveState moveStateData;

    [SerializeField] private D_PlayerDetected playerDetectedData;

    [SerializeField] private D_ChargeState chargeStateData;
    //private bool isDashing;

    //private float dashTimeLeft;
    //private float lastImageXpos;
    //private float lastDash = -100f;

    //public float dashTime;
    //public float dashSpeed;
    //public float distanceBetweenImages;
    //public float dashCoolDown;


    public override void Start()
    {
        base.Start();

        moveState = new E1_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new E1_IdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new E1_PlayerDetectedState(this, stateMachine, "playerDetected",playerDetectedData, this);
        chargeState = new E1_ChargeState(this, stateMachine, "charge", chargeStateData, this);

        stateMachine.Initialize(moveState);
    }

    //private void AttemptToDash()
    //{
    //    isDashing = true;
    //    dashTimeLeft = dashTime;
    //    lastDash = Time.time;

    //    AfterImagePool.Instance.GetFromPool();
    //    lastImageXpos = transform.position.x;
    //}

    //private void checkDash()
    //{
    //    if (isDashing)
    //    {
    //        if (dashTimeLeft > 0)
    //        {
    //            rb.velocity = new Vector2(dashSpeed, rb.velocity.y);
    //            dashTimeLeft -= Time.deltaTime;

    //            if (Mathf.Abs(transform.position.x - lastImageXpos) > distanceBetweenImages)
    //            {
    //                AfterImagePool.Instance.GetFromPool();
    //                lastImageXpos = transform.position.x;

    //            }
    //        }

    //        if (dashTimeLeft <= 0)
    //        {
    //            isDashing = false;
    //        }



    //    }
    //}
}
