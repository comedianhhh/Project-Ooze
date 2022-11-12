using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeState : State
{
    protected D_ChargeState stateData;

    protected bool isPlayerInMinAgroRange;
    protected bool isChargeTimeOver;

    protected bool isDetectingWall;
    public ChargeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_ChargeState stateData) : base(
        entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base .Enter();
        entity .SetVelocity(stateData.chargeSpeed);
        isChargeTimeOver = false;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isDetectingWall)
        {
            //TODO:connet to look for player
        }
        else if (Time.time >= startTime + stateData.chargeTime)
        {
            isChargeTimeOver = true;
        }
    }

    public override void Dochecks()
    {
        base.Dochecks();
        //isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
        isDetectingWall = entity.CheckWall();

    }
}
