using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(player.wallJumpState);
            return;
        }

        if (horizontalInput !=0 && player.xScale != horizontalInput)
        {
            stateMachine.ChangeState(player.idleState);
        }

        if (player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.idleState);
            player.Flipper();  //Yuzunu duvara donmesin diye karakteri cevirdim , olmasa da olur
        }
        
        //Asagi tusa basildigi zaman 0 dan kucuk degerler geliyor
        if (verticalInput<0)
            rb.velocity = new Vector2(0, rb.velocity.y);
        else
        {                                   //Asagi inme hizi
            rb.velocity = new Vector2(0, rb.velocity.y * .7f);
        }
           
        
       
    }

    public override void Exit()
    {
        base.Exit();
    }


}
