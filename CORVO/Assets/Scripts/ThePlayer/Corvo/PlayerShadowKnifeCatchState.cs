using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShadowKnifeCatchState : PlayerState
{
    private Transform shadowKnife;
    public PlayerShadowKnifeCatchState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        shadowKnife = player.shadowKnife.transform;

        if (player.transform.position.x > shadowKnife.position.x && player.xScale == 1)
        {
            player.Flipper();
        }
        else if (player.transform.position.x < shadowKnife.position.x && player.xScale == -1)
        {
            player.Flipper();
        }

        rb.velocity = new Vector2(player.shadowKnifeReturnRecoil * -player.xScale, rb.velocity.y);
    }
    public override void Update()
    {
        base.Update();

        if (triggerCalled) 
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();

        //Firlatırken olusan bugu engelledim
        player.StartCoroutine("Busy", .1f);
    }

}
