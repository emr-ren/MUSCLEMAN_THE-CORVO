using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Entity : MonoBehaviour
{

    #region Components

    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public EntityFX fx { get; private set; }
    public SpriteRenderer sr { get; private set; }
    public CharacterStats characterStats { get; private set; }
    public CapsuleCollider2D capsuleCD {  get; private set; } 

    #endregion

    #region Collision Info

    [Header("Collision Info")]
    //Varligin yeri tanimlamasi icin; yer collisionu ile varligin col.'unun carpisma bilgisini vericek. 
    [SerializeField] protected Transform groundChecker;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected LayerMask groundLayerMask;
    
    //Varligin duvari tanimlamasi icin; duvar collisionu ile varligin col.'unun carpisma bilgisini vericek. 
    [SerializeField] protected Transform wallChecker;
    [SerializeField] protected float wallCheckDistance;
    #endregion

    [Header("Attack Settings")]
    public float AttackSpeed;
    public Transform attackCheck;
    public float attackCheckRadius;


    [Header("Knockback Info")]
    [SerializeField] protected Vector2 knockback;
    [SerializeField] protected float knockbackForce;
    protected bool isKnocked;



    public int xScale { get; private set; } = 1;
    protected bool scaleRight = true;


    //Event olusturdun HealthBarda kullanicaksin unutma UI icin
    public System.Action FlipTheUI;



    protected virtual void Awake()   
    {

    }

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        fx = GetComponent<EntityFX>();

        characterStats = GetComponent<CharacterStats>();

        anim = GetComponentInChildren<Animator>(); //Animator, Corvo'nun alt ogesi oldugundan Animator'u tanıması icin InChildren olarak yazilir.

        sr= GetComponentInChildren<SpriteRenderer>();

        capsuleCD = GetComponent<CapsuleCollider2D>();

    }

    protected virtual void Update()
    {

    }

    public virtual void DamageEffect() => StartCoroutine("HitKnockback");
    

    protected virtual IEnumerator HitKnockback()
    {
        isKnocked = true;

        rb.velocity = new Vector2(knockback.x * -xScale, knockback.y);

        yield return new WaitForSeconds(knockbackForce);
        isKnocked = false;
    }


    #region Velocity
            //Hareketsizlik= Zero Velocity
    public void Immobility()
    {
        if (isKnocked)
        {
            return;
        }
        rb.velocity = new Vector2(0, 0);
    }

    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        if (isKnocked)
        {
            return;
        }

        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        CharacterFlip(_xVelocity);

    }
    #endregion


    #region Character Flip

    //Oyuncunun karakteri saga yada sola hareket ettirirken, karakterin goruntusunu 1 olacak sekilde saga, -1 olacak sekilde sola çevirecek


    public void Flipper()
    {
        xScale = xScale * -1;
        scaleRight = !scaleRight;  //True ise false, false ise true yapar
        transform.Rotate(0, 180, 0);

        //Donunce  UI hatasini engelledim 
        if (FlipTheUI !=null)     
            FlipTheUI();

        /*{  CEVİRME ESKİ KODUM
         *Sprite'nin Scale X'ini karakter saga dogru ilerlerken 1 degeri, sola dogru ilerlerken - 1 degerini atiyor. 
            scaleRight = !scaleRight;
            Vector3 scale=transform.localScale;
            scale.x *= -1;
            transform.localScale=scale;
           }
         */
    }

    public void CharacterFlip(float _xMove)
    {
        //Rb'nin x derecesinin buyumesini pozitifse ve saga gitmesi icin scaleRight degeri True olmali
        if (_xMove > 0 && !scaleRight)
        {
            Flipper();
        }
        //Rb'nin x derecesinin buyumesini negatifse ve sola gitesi icin sclaeRight False olmali
        else if (_xMove < 0 && scaleRight)
        {
            Flipper();
        }
    }
    #endregion

    #region Collision Impact
    public virtual bool IsGroundDetected() => Physics2D.Raycast(groundChecker.position, Vector2.down, groundCheckDistance, groundLayerMask);

    public virtual bool IsWallDetected() => Physics2D.Raycast(wallChecker.position, Vector2.right * xScale, wallCheckDistance, groundLayerMask);

    protected virtual void OnDrawGizmos()
    {
        //Karakterimden yere dogru bir lazer gondericem bu da benim yerde olup olmadigimi anlayacak
        Gizmos.DrawLine(groundChecker.position, new Vector3(groundChecker.position.x, groundChecker.position.y - groundCheckDistance));

        //Karakterimin baktigi tarafa dogru bir lazer gondericem
        Gizmos.DrawLine(wallChecker.position, new Vector3(wallChecker.position.x + wallCheckDistance, wallChecker.position.y));

        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }
    #endregion
    

    public virtual void Die()
    {

    }

}
