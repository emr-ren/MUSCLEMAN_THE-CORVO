using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{

    private int comboCounter;
    private float lastHitTimer;
    private float timeForNextAttack= 2;



    public PlayerPrimaryAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.anim.speed = player.AttackSpeed;

        if (comboCounter>2 || Time.time>=lastHitTimer + timeForNextAttack)
        {
            comboCounter = 0;
        }

        player.anim.SetInteger("ComboCounter", comboCounter);

        horizontalInput = 0;  //Saldırdığında istediğim yere vurmuyordu geçici düzelttin fonksiyon yaz...

        stateTimer = .1f;

        #region Attack Direction 
        float attackDir = player.xScale;
        if (horizontalInput !=0)
        {
            attackDir = horizontalInput;
        }
        #endregion
        player.SetVelocity(player.attackStepping[comboCounter].x * attackDir /*player.xScale*/, player.attackStepping[comboCounter].y);
    }
    public override void Update()
    {
        base.Update();

        //Yuruken yada kosarken vurmayi engelledim. Artik Corvo saldirdigi zaman oldugu yerde durarak vurucak (Inertia)
        if (stateTimer < 0)
        {
            player.Immobility();
        }

        if (triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();

        //StartCoroutine, Unity'de zamanla ilgili işlemleri, gecikmeleri veya uzun süreli ☺eylemleri kesintisiz bir şekilde yürütmek için kullanılır.
        //Attacktan sonra belirlenen zamanda hareket etmemesi icin =Idle'a gecicek
        player.StartCoroutine("Busy", .1f);

        player.anim.speed = 1f;

        comboCounter++;

        //vurusu gercek zamana esitledim, bekleme suresi sayaci
        lastHitTimer = Time.time;
    }


}
