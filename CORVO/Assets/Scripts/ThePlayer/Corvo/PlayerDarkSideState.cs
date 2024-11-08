using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDarkSideState : PlayerState
{
    private float flyTime = .4f;
    private bool darkSideSkillUsed;

    private float defaultGravity;

    public PlayerDarkSideState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void Enter()
    {
        base.Enter();
        defaultGravity = player.rb.gravityScale;
        darkSideSkillUsed = false;
        stateTimer = flyTime;
        rb.gravityScale = 0;
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer > 0)
            rb.velocity = new Vector2(0, 15);

        if (stateTimer < 0)
        {
            rb.velocity = new Vector2(0, -.1f);

            if (!darkSideSkillUsed)
            {
                if (player.skill.theDarkSideSkill.CanUseSkill())
                    darkSideSkillUsed = true;
            }
        }

        if (player.skill.theDarkSideSkill.DarkSideEnd())
            stateMachine.ChangeState(player.inTheAirState);
        
    }
    public override void Exit()
    {
        base.Exit();

        player.rb.gravityScale = defaultGravity;

        //Gorunmezlikten cikmasi icin   DarkSideSkillController = PlayerManager.instance.player.MakeTransprent(true);
        player.fx.MakeTransprent(false);
    }

}
