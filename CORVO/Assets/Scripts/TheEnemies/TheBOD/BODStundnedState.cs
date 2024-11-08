using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BODStundnedState : EnemyState
{
    private BOD enemy;
    public BODStundnedState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animBoolName, BOD _enemy) : base(_enemyBase, _enemyStateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        enemy.fx.InvokeRepeating("ColorBlink", 0, .1f);
            
        stateTimer = enemy.stunDuration; 

        rb.velocity = new  Vector2(-enemy.xScale * enemy.stunDirection.x, enemy.stunDirection.y);
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
        {
            stateMachine.ChangeState(enemy.idleState);
        }
    }
    public override void Exit()
    {
        base.Exit();

        enemy.fx.Invoke("CancelBlink", 0);
    }

}
