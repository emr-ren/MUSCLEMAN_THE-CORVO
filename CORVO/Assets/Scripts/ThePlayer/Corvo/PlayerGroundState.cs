using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundState : PlayerState
{
    public PlayerGroundState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.F))
        {
            stateMachine.ChangeState(player.darkSideState);
        }

        if (Input.GetKeyDown(KeyCode.R) && HasNoShadowKnife())
        {
            stateMachine.ChangeState(player.shadowKnifeAimState);
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            stateMachine.ChangeState(player.counterAttackState);
        }

                    
        if (Input.GetKeyDown(KeyCode.Mouse0) /* Input.GetKey(KeyCode.Mouse0)*/)
        {
            stateMachine.ChangeState(player.primaryAttackState);
        }


        
        if (player.IsGroundDetected() == false) // yada (!player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.inTheAirState);
        }

                                               
        if(Input.GetKeyDown(KeyCode.Space) && player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.jumpState);
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
