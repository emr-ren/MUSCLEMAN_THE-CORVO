using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BODDeadState : EnemyState
{
    private BOD enemy;
    public BODDeadState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animBoolName, BOD _enemy) : base(_enemyBase, _enemyStateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        enemy.Immobility();

    }

    public override void Update()
    {
        base.Update();

        
    }


}
