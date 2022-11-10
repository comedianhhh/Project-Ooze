using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_ChargeState : ChargeState
{
    private Enemy1 enemy1;
    public E1_ChargeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_ChargeState stateData,Enemy1 enemy1) : base(
        entity, stateMachine, animBoolName,stateData)
    {
        this.enemy1 = enemy1;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isChargeTimeOver)
        {
            if (isPlayerInMinAgroRange)
            {
                stateMachine.ChangeState(enemy1.playerDetectedState);
            }
        }
    }
}
