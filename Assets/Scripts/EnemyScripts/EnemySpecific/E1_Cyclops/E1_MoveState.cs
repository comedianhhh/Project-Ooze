using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_MoveState : MoveState
{
    private Enemy1 enemy;
    public E1_MoveState(Entity entity, FiniteStateMachine stateMachine, string animBoolName,D_MoveState stateData,Enemy1 enemy) : base(entity, stateMachine,
        animBoolName,stateData)
    {
        this.enemy = enemy;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isPlayerInMinAgroRange)
        {
            stateMachine.ChangeState(enemy.playerDetectedState);
        }
        
        else if (isDetectingWall)
        {
            enemy.idleState.SetFlipAfterIdle(true);
           stateMachine.ChangeState(enemy.idleState);
        }
    }
}
