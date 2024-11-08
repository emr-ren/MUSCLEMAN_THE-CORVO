using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Entity
{
    #region Corvo'ya Ozgu Degerler
    ////////////////////////////////////////////
    [Header("Attack Info")]
    public Vector2[] attackStepping;
    public float counterAttackDuration;

    public bool isBusy { get; private set; }


    [Header("Horizontal Info")]
    public float moveSpeed ;
    public float shadowKnifeReturnRecoil;


    [Header("Dash Info")]
    public float dashSpeed;
    public float dashDuration;
    public float dashDir { get; private set; }


    [Header("Vertical Info")]
    public float jump;
    public float wallJump;

    ////////////////////////////////////////////
    #endregion

    public PlayerSkillManager skill { get; private set; }
    public GameObject shadowKnife {  get; private set; }


    #region States
    public PlayerStateMachine                    stateMachine{ get; private set; }
    public PlayerIdleState                          idleState{ get; private set; }
    public PlayerMoveState                          moveState{ get; private set; }
    public PlayerInTheAirState                  inTheAirState{ get; private set; } 
    public PlayerJumpState                          jumpState{ get; private set; }
    public PlayerDashState                          dashState{ get; private set; } 
    public PlayerWallSlideState                wallSlideState{ get; private set; }
    public PlayerWallJumpState                  wallJumpState{ get; private set; }
    public PlayerPrimaryAttackState        primaryAttackState{ get; private set; }
    public PlayerCounterAttackState        counterAttackState{ get; private set; }
    public PlayerShadowKnifeAimState     shadowKnifeAimState { get; private set; }
    public PlayerShadowKnifeCatchState shadowKnifeCatchState { get; private set; }
    public PlayerDarkSideState                  darkSideState{ get; private set; }
    public PlayerDeadState                         deadState { get; private set; }

    #endregion


    protected override void Awake()
    {
        base.Awake();
        stateMachine = new PlayerStateMachine();

        idleState =             new PlayerIdleState(this ,   stateMachine,"Idle");

        moveState =             new PlayerMoveState(this ,  stateMachine, "Move");
        
        // Havada olma durumu ile ziplama durumu icin blendtree olsuturdum bu sayede ikisini de kullanabildim ve islem tasarrufu yaptim
        jumpState     =         new PlayerJumpState(    this , stateMachine, "Jump");
        inTheAirState =         new PlayerInTheAirState(this,  stateMachine, "Jump");
        wallJumpState =         new PlayerWallJumpState(this,  stateMachine, "Jump");
        darkSideState =         new PlayerDarkSideState(this,  stateMachine, "Jump");

        dashState =             new PlayerDashState(this ,  stateMachine, "Dash");

        wallSlideState = new PlayerWallSlideState(this, stateMachine, "WallSlide");
       
        primaryAttackState = new PlayerPrimaryAttackState(this, stateMachine, "Attack");

        counterAttackState = new PlayerCounterAttackState(this, stateMachine, "CounterAttack");

        shadowKnifeAimState = new PlayerShadowKnifeAimState(this, stateMachine, "ShadowKnifeAim");

        shadowKnifeCatchState = new PlayerShadowKnifeCatchState(this, stateMachine, "ShadowKinfeCatch");

        deadState = new PlayerDeadState(this, stateMachine, "Dead");

    }
    protected override void Start()
    {
        base.Start();

        skill = PlayerSkillManager.instance;

        //Animasyon durumunu baslatmak icin PlayerStateMachine'in icine olusturdugum fonksiyonu cagirdim
        //Idle animasyonumu default olarak _startState'e ayarladım 
        stateMachine.InceptionState(idleState);

        
    }
    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
        CloneCorveSkill();
        Dash();
    }
    
    public void AssingNewShadowKnife(GameObject _newShadowKnife)
    {
        shadowKnife = _newShadowKnife;

    }

    public void CatchTheShadowKnife()
    {
        stateMachine.ChangeState(shadowKnifeCatchState);
        Destroy(shadowKnife);
    }

    //Animasyon bitiren kodu cagirdim
    public void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    //PlayerPrimaryAttackState exit'inde bulunan saldiri esnasinda ufak bir saniye ile hareketi onlemek icin
    public IEnumerator Busy(float _second)
    {
        isBusy = true;
        yield return new WaitForSeconds(_second);
        isBusy = false;
    }

    private void Dash()
    {
        //DashState'e yaptigim gibi 2. bug engelleme yontemim,
        //Duvar algilanirsa, duvardan kayma yada duvardan zıplamada dash atilamayacak. 'return' geri kalan kismi calistirmadan fonksiyonu hemen sonlandirir ve fonksiyonu cagiran koda geri dondurcek.
        if (IsWallDetected())
        {
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            dashDir = Input.GetAxisRaw("Horizontal");

            //Dash atarken yon degistirmemesi icin
            if (dashDir == 0)
            {
                dashDir = xScale;
            }

            stateMachine.ChangeState(dashState);
        }
    }

    public void CloneCorveSkill()
    {
        if (Input.GetKeyDown(KeyCode.C) && IsGroundDetected())
        {
            PlayerSkillManager.instance.dash.CanUseSkill();
            skill.cloneCorvo.CreateCloneCorvo(transform, Vector3.zero);
        }
    }

    public override void Die()
    {
        base.Die();

        stateMachine.ChangeState(deadState);
    }

}
