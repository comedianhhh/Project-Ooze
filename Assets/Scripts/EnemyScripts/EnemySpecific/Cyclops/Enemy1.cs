using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Entity
{

    public Cyc_IdleState idleState { get; private set; }
    public Cyc_MoveState moveState { get; private set; }

    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_MoveState moveStateData;


    private bool isDashing;

    private float dashTimeLeft;
    private float lastImageXpos;
    private float lastDash = -100f;

    public float dashTime;
    public float dashSpeed;
    public float distanceBetweenImages;
    public float dashCoolDown;


    public override void Start()
    {
        base.Start();
        moveState = new Cyc_MoveState(this, stateMachine, "move", moveStateData, this);
    }

    private void AttemptToDash()
    {
        isDashing = true;
        dashTimeLeft = dashTime;
        lastDash = Time.time;

        AfterImagePool.Instance.GetFromPool();
        lastImageXpos = transform.position.x;
    }

    private void checkDash()
    {
        if (isDashing)
        {
            if (dashTimeLeft > 0)
            {
                rb.velocity = new Vector2(dashSpeed, rb.velocity.y);
                dashTimeLeft -= Time.deltaTime;

                if (Mathf.Abs(transform.position.x - lastImageXpos) > distanceBetweenImages)
                {
                    AfterImagePool.Instance.GetFromPool();
                    lastImageXpos = transform.position.x;

                }
            }

            if (dashTimeLeft <= 0)
            {
                isDashing = false;
            }



        }
    }
}
