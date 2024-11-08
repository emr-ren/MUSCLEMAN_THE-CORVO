using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class ShadowKnifeSkillController : MonoBehaviour
{

    private Animator anim;
    private Rigidbody2D rb;
    private CircleCollider2D cd;
    private Player player;

    private bool canRotate = true;
    private bool isReturning;


    [Header("ShadowKnife Time Info")]
    private float freezeTimeDuration;
    private float returnSpeed;


    [Header("Pierce Info")]
    private float enemyTargetPierce;

    [Header("Bounce Info")]
    private float bounceSpeed;
    private bool isBouncing;
    private int enemyTargetBounce;
    private List<Transform> enemyTarget;
    private int targetIndex;

    [Header("Spin Info")]
    private float maxShadowKnifeDistance;
    private float spinDuration;
    private float spinTimer;
    private bool wasStopped;
    private bool isSpinning;

    private float hitTimer;
    private float hitCooldown;

    private float spinDirection;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<CircleCollider2D>();
    }
    private void DestroyShadowKnife()
    {
        Destroy(gameObject);
    }
    public void SetupShadowKnife(Vector2 _dir, float _gravityScale, Player _player, float _freezeTimerDuration, float _returnSpeed)
    {
        player = _player;
        freezeTimeDuration = _freezeTimerDuration;
        returnSpeed = _returnSpeed;

        rb.velocity = _dir;
        rb.gravityScale = _gravityScale;

        if (enemyTargetPierce <= 0)
            anim.SetBool("Rotation", true);

        spinDirection = Mathf.Clamp(rb.velocity.x, -1, 1);

        Invoke("DestroyShadowKnife",16);
    }

    public void SetupBounce(bool _isBouncing, int _enemyTargetBounce, float _bounceSpeed)
    {
        isBouncing = _isBouncing;
        enemyTargetBounce = _enemyTargetBounce;
        bounceSpeed = _bounceSpeed;

        enemyTarget = new List<Transform>();
    }

    public void SetupPierce(int _enemyTargetPierce)
    {
        enemyTargetPierce = _enemyTargetPierce; 
    }

    public void SetupSpin(bool _isSpinning, float _maxShadowKnifeDistance, float _spinDuration, float _hitCoolDown)
    {
        isSpinning = _isSpinning;
        maxShadowKnifeDistance = _maxShadowKnifeDistance;
        spinDuration = _spinDuration;
        hitCooldown = _hitCoolDown;
    }

    public void ReturnShadowKnife()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        //rb.isKinematic = false;
        transform.parent = null;
        isReturning = true;

    }

    private void Update()
    {
        if (canRotate)
        {
            transform.right = rb.velocity;
        }

        if (isReturning)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, returnSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, player.transform.position) < 1)
            {
                player.CatchTheShadowKnife();
            }
        }

        BouncingMethod();

        SpinningMethod();
    }

    private void SpinningMethod()
    {
        if (isSpinning)
        {
            if (Vector2.Distance(player.transform.position, transform.position) > maxShadowKnifeDistance && !wasStopped)
            {
                StopWhenSpinning();
            }

            if (wasStopped)
            {
                spinTimer -= Time.deltaTime;

                transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x + spinDirection, transform.position.y), 1f*Time.deltaTime);

                if (spinTimer < 0)
                {
                    isReturning = true;
                    isSpinning = false;
                }

                hitTimer -= Time.deltaTime;

                if (hitTimer < 0)
                {
                    hitTimer = hitCooldown;
                    Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 2);

                    foreach (var hit in colliders)
                    {
                        if (hit.GetComponent<Enemy>() != null) ;
                    }
                }
            }
        }
    }

    private void StopWhenSpinning()
    {
        wasStopped = true;
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        spinTimer = spinDuration;
    }

    private void BouncingMethod()
    {
        if (isBouncing && enemyTarget.Count > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, enemyTarget[targetIndex].position, bounceSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, enemyTarget[targetIndex].position) < .1f)
            {
                //ZAMAN DURAKLATMASI EKLEDIM USTTEKI DURDURUYOR ASAGIDAKI DURDURMUYOR
                ShadowKnifeSkillDamage(enemyTarget[targetIndex].GetComponent<Enemy>());
                
                //enemyTarget[targetIndex].GetComponent<Enemy>().DamageEffect();

                targetIndex++;
                enemyTargetBounce--;

                if (enemyTargetBounce <= 0)
                {
                    isBouncing = false;

                    //SAPLANIP KALMASI ICIN, OTOMATIK GELMESI ICIN DE ASAGIDAKI isReturning = true; KODU 
                    targetIndex = Mathf.Clamp(targetIndex, 0, enemyTarget.Count - 1);
                    StuckInto(enemyTarget[targetIndex].GetComponent<Collider2D>()); // Çarptığı düşmanda kalmasını sağla
                    return; // BouncingMethod'dan çık


                    /*BOUNCE BITTIKTEN SONRA OTOMATIK GERI DONMESI ICIN
                    isReturning = true;
                    */
                }

                //Olan dusmanlardan sektikten sonra ilk dusmaan geri gelmememe sorununu cozdum
                if (targetIndex >= enemyTarget.Count)
                    targetIndex = 0;

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Bicagi attigim an geri cekince dusmana carparsa duz geliyor bunu düzelttim
        if (isReturning)
        {
            return;
        }

        if (collision.GetComponent<Enemy>() != null)
        {
            Enemy enemy = collision.GetComponent<Enemy>();

            //DUSMANLARI DURDURMAK ICIN USTTEKI, ALTTAKI NORMAL HASAR
            //ShadowKnifeSkillDamage(enemy);

            //HASAR VERMEK ICIN KOD
            player.characterStats.DoDamage(collision.GetComponent<CharacterStats>());
        }


        /* Eski hasar verme kodu 
        var enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.DamageEffect();
        }
        */

        SetupEnemyTargetBounce(collision);

        StuckInto(collision);
    }

    private void ShadowKnifeSkillDamage(Enemy enemy)
    {
        player.characterStats.DoDamage(enemy.GetComponent<CharacterStats>());
        enemy.StartCoroutine("FreezeTimer", freezeTimeDuration);
    }

    private void SetupEnemyTargetBounce(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            if (isBouncing && enemyTarget.Count <= 0)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 10);

                foreach (var hit in colliders)
                {
                    if (hit.GetComponent<Enemy>() != null)
                    {
                        enemyTarget.Add(hit.transform);
                    }
                }
            }
        }
    }

    private void StuckInto(Collider2D collision)
    {
        if (isSpinning)
        { 
            StopWhenSpinning();
            return;
        }
        

        if (enemyTargetPierce > 0 && collision.GetComponent<Enemy>() != null )
        {
            enemyTargetPierce--;
            return;
        }

        canRotate = false;
        cd.enabled = false;

        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        if (isBouncing && enemyTarget.Count > 0)
            return;

        anim.SetBool("Rotation", false);

        transform.parent = collision.transform;
    }
}
