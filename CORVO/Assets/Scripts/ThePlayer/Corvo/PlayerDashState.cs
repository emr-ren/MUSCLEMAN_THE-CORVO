using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        //Player scriptinde 'skill = PlayerSkillManager.instance; ' diye tanıttım.

        stateTimer = player.dashDuration;
    }
    public override void Update()
    { 
        base.Update();
                          
        //Dash'ten sonra duvara hemen yapismasi ve wallslide durumda iken duvara dash atma bugunu onledim.
        if (player.IsGroundDetected() && player.IsWallDetected()) 
        {
            stateMachine.ChangeState(player.wallSlideState);            
        }
                                                            //Yercekimini kaldirmak ve keskin atlama icin '0' yapabiliriz
        player.SetVelocity(player.dashSpeed * player.dashDir, rb.velocity.y*0.3f);

        if (stateTimer < 0)
            stateMachine.ChangeState(player.idleState);
    }

    public override void Exit()
    {
        base.Exit(); 
        
        player.SetVelocity(rb.velocity.x , rb.velocity.y);

    }

}
