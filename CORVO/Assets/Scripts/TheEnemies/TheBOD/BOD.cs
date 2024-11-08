using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOD : Enemy
{

    #region States
    public BODIdleState           idleState { get; private set; }
    public BODMoveState         moveState   { get; private set; }
    public BODBattleState       battleState { get; private set; }
    public BODAttackState       attackState { get; private set; }
    public BODStundnedState    stunnedState { get; private set; }
    public BODDeadState           deadState { get; private set; }

    #endregion
    protected override void Awake()
    {
        base.Awake();
        idleState = new BODIdleState(this, stateMachine, "Idle", this);

        moveState   = new   BODMoveState(this,   stateMachine, "Move", this);

        battleState = new   BODBattleState(this, stateMachine, "Battle", this);

        attackState = new BODAttackState(this, stateMachine, "Attack", this);

        stunnedState = new BODStundnedState(this, stateMachine, "Stunned", this);

        deadState = new BODDeadState(this, stateMachine, "Dead", this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.InceptionState(idleState);


    }

    protected override void Update()
    {   
        base.Update();
        /*
        //Silinecek deneme
        if (Input.GetKeyDown(KeyCode.U))
        {
            stateMachine.ChangeState(stunnedState);
        }
        */
    }

    public override bool CanBeStunned()
    {
        if (base.CanBeStunned())
        {
            stateMachine.ChangeState(stunnedState);
                return true;
        }
        return false;
    }

    public override void Die()
    {
        base.Die();

        stateMachine.ChangeState(deadState);
        
    }
}
