using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BODMoveState : BODGroundState
{
    public BODMoveState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string animBoolName, BOD enemy) : base(_enemyBase, _enemyStateMachine, animBoolName, enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();  
        enemy.SetVelocity(enemy.moveSpeed * enemy.xScale, rb.velocity.y);

        if (enemy.IsWallDetected() || !enemy.IsGroundDetected())
        {
            enemy.Immobility();
            enemy.Flipper();
            stateMachine.ChangeState(enemy.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

}
