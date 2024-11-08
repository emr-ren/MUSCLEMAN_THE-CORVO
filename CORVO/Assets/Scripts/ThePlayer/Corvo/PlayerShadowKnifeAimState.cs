using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShadowKnifeAimState : PlayerState
{
    public PlayerShadowKnifeAimState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.skill.shadowKnife.DotsActive(true);
    }
    public override void Update()
    {
        base.Update();
        
        //player.Immobility();

        if (player.IsGroundDetected())
        {
            player.Immobility();
        }


        if (Input.GetKeyUp(KeyCode.R))
        {
            stateMachine.ChangeState(player.idleState); 
        }

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (player.transform.position.x > mousePosition.x && player.xScale == 1)
        {
            player.Flipper();
        }
        else if (player.transform.position.x < mousePosition.x && player.xScale == -1)
        {
            player.Flipper();
        }
    }

    public override void Exit()
    {
        base.Exit();
        
        //Firlatırken olusan bugu engelledim
        player.StartCoroutine("Busy", .2f);
    }

}
