using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] protected LayerMask isThatCorvo;
    [SerializeField] protected float RangeOfVision;
    [SerializeField] public float SenseOfStealth;

    [Header("Stun Info")]
    public float stunDuration;
    public Vector2 stunDirection;
    protected bool canBeStunned;
    [SerializeField] protected GameObject counterImage;

    [Header("Move Info")]
    public float moveSpeed;
    public float idleTime;
    public float battleTime;
    public float forgettingDistance;
    private float defaultMoveSpeed;


    [Header("Attack Info")]
    public float attackRange;
    public float attackCooldown;
    [HideInInspector] public float lastestAttackTime;


    public EnemyStateMachine stateMachine { get; private set; }

    public string lastAnimBoolName { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();

        defaultMoveSpeed = moveSpeed;
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
    }

    public virtual void AssingLastAnimName(string _animBoolName)
    {
        lastAnimBoolName = _animBoolName;
    }
    
    public virtual void FreezeTime(bool _freezeTime)
    {
        if (_freezeTime)
        {
            moveSpeed = 0;
            anim.speed = 0; 
        }
        else
        {
            moveSpeed = defaultMoveSpeed;
            anim.speed = 1;

        }
    }

    protected virtual IEnumerator FreezeTimer(float _seconds)
    {
        FreezeTime(true);
        yield return new WaitForSeconds(_seconds);
        
        FreezeTime(false);
    }


    #region Counter Attack Window
    public virtual void CounterAttackWindow()
    {
        canBeStunned = true;
        counterImage.SetActive(true);
    }
    public virtual void CloseCounterAttackWindow()
    {
        canBeStunned = false;
        counterImage.SetActive(false);
    }

    public virtual bool CanBeStunned()
    {
        if (canBeStunned)
        {
            CloseCounterAttackWindow();            
            return true;
        }
        return false;
    }
    #endregion


    public virtual void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    public virtual RaycastHit2D isPlayerDetected()
    {        
        RaycastHit2D playerDetected = Physics2D.Raycast(wallChecker.position, Vector2.right * xScale, RangeOfVision, isThatCorvo);
        RaycastHit2D wallDetected = Physics2D.Raycast(wallChecker.position, Vector2.right * xScale, RangeOfVision, groundLayerMask);

        if (wallDetected)
        {
            if (wallDetected.distance < playerDetected.distance)
                return default(RaycastHit2D); 
        }
        return playerDetected;
    }
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
         
        Gizmos.color= Color.red;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + attackRange * xScale, transform.position.y));
    }
}
