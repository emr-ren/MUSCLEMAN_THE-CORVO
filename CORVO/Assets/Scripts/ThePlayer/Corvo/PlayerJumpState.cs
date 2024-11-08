using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        rb.velocity = new Vector2(rb.velocity.x, player.jump);
    }

    public override void Update()
    {
        base.Update();

        //Zipladiktan sonra hemen kontrol etmek icin bu kodu yazdim. Bu kodu eklemeden de olabilirdi.
        if (horizontalInput != 0)
        {
            player.SetVelocity(player.moveSpeed * horizontalInput, rb.velocity.y);
        }


        if (rb.velocity.y < 0)
        {
            stateMachine.ChangeState(player.inTheAirState);
        }
        if (Input.GetKeyDown(KeyCode.R) && HasNoShadowKnife())
        {
            stateMachine.ChangeState(player.shadowKnifeAimState);
        }
    }
    private bool HasNoShadowKnife()
    {
        if (!player.shadowKnife)
        {
            return true;
        }

        player.shadowKnife.GetComponent<ShadowKnifeSkillController>().ReturnShadowKnife();
        return false;
    }


    public override void Exit()
    {
        base.Exit();
       
    }
}

