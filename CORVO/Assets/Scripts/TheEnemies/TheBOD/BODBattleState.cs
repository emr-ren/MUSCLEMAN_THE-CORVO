using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BODBattleState : EnemyState
{
    private Transform player;
    private BOD enemy;
    private int moveDir;

    private bool flippedOnce;
   
    public BODBattleState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string animBoolName,BOD _enemy) : base(_enemyBase, _enemyStateMachine, animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        player = PlayerManager.instance.player.transform;
        //Corvoyu oldurdukten sonra burada durdurulacak dusman
        if (player.GetComponent<CorvoStats>().isDead)
            stateMachine.ChangeState(enemy.moveState);


        stateTimer = enemy.battleTime;
        flippedOnce = false;
    }

    public override void Update()
    {
        base.Update();

        enemy.anim.SetFloat("xVelocity", enemy.rb.velocity.x);

        if(enemy.isPlayerDetected())
        {
            stateTimer = enemy.battleTime;

            if (enemy.isPlayerDetected().distance < enemy.attackRange)
            {
                if(Attackable())
                stateMachine.ChangeState(enemy.attackState);
            }
        }
        else
        {
            if (flippedOnce == false)
            {
                flippedOnce = true;            
                enemy.Flipper();
            }

            //Belirlenen saldiri zamani gectiginde yada Corvo ile aradaki mesafe arttiginda idlea gecicek 
            if (stateTimer < 0 || Vector2.Distance(player.transform.position, enemy.transform.position) > enemy.forgettingDistance)
            {
                stateMachine.ChangeState(enemy.idleState);
            }
        }


        float distanceToPlayerX = Mathf.Abs(player.position.x - enemy.transform.position.x);
        if (distanceToPlayerX < 0.78f)
            return;
        


        // sag sol kavrami 
        if (player.position.x > enemy.transform.position.x)
        {
            moveDir = 1;   
        }
        else if (player.position.x < enemy.transform.position.x)
        {
            moveDir = -1;
        }

        enemy.SetVelocity(enemy.moveSpeed * moveDir, rb.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();

    }

    private bool Attackable()
    {                                                                   //Attack Cooldown bugunu kaldirdim
        if (Time.time >= enemy.lastestAttackTime + enemy.attackCooldown /*|| player.transform*/)
        {
            enemy.lastestAttackTime = Time.time;
            return true;
        }
        else
        {
            return false;
        }

    }
}
