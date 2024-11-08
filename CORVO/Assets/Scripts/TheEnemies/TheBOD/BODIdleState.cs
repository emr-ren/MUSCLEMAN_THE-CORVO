using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BODIdleState : BODGroundState
{
    public BODIdleState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string animBoolName, BOD enemy) : base(_enemyBase, _enemyStateMachine, animBoolName, enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = enemy.idleTime;
    }


    public override void Update()
    {
        base.Update();

        if (stateTimer <0 ) 
        {
            stateMachine.ChangeState(enemy.moveState);
        }

        
    }
    public override void Exit()
    {
        base.Exit();
    }
}
