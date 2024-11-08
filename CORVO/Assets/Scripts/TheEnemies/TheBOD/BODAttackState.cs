using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BODAttackState : EnemyState
{
    private BOD enemy;
    public BODAttackState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string animBoolName, BOD _enemy) : base(_enemyBase, _enemyStateMachine, animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        enemy.Immobility();

        if (triggerCalled)
        {
            stateMachine.ChangeState(enemy.battleState);
        }
    }

    public override void Exit()
    {
        base.Exit();

        enemy.lastestAttackTime = Time.time;
    }
}
