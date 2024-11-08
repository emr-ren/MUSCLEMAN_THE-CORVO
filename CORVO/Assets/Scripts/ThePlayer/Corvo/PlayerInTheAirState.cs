using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInTheAirState : PlayerState
{
    public PlayerInTheAirState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();


        if (player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.idleState);
        }

        if (horizontalInput != 0)
        {
            player.SetVelocity(player.moveSpeed * .8f * horizontalInput, rb.velocity.y);
        }
        
        if (player.IsWallDetected())
        {
            stateMachine.ChangeState(player.wallSlideState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        
    }
}
