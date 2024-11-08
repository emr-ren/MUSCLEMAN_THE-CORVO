using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerState
{
    public PlayerWallJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = .4f;                 
        player.SetVelocity(player.wallJump * -player.xScale, player.jump);
    }
    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
        {
            stateMachine.ChangeState(player.inTheAirState);
        }

        /*Zipladiktan sonra hemen kontrol etmek icin bu kodu yazdim. Kodu ekledigimde buga giriyor eklemeye gerek yok.
        if (horizontalInput != 0)
        {
            player.SetVelocity(player.moveSpeed * horizontalInput, rb.velocity.y);
        }
        */

        //Dusus animasyonunun bugunu engelledim yere deydigi an idle'a gecis yapiyor
        if (player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

}
