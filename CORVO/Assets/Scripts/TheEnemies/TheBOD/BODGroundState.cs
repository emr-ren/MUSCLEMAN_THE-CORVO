using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BODGroundState : EnemyState
{
    protected BOD enemy;
    protected Transform player;

    public BODGroundState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string animBoolName, BOD _enemy) : base(_enemyBase, _enemyStateMachine, animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        player = PlayerManager.instance.player.transform;
    }
    public override void Update()
    {
        base.Update();

        if (enemy.isPlayerDetected() || Vector2.Distance(enemy.transform.position, player.position) < enemy.SenseOfStealth)
        {
            stateMachine.ChangeState(enemy.battleState);
        }
     }

    public override void Exit()
    {
        base.Exit();
    }

}
