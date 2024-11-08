using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneCorvoSkillController : MonoBehaviour
{
    private Player player;
    private SpriteRenderer sr;
    private Animator anim;

    [SerializeField] private float colorLoosingSpeed;
    private float cloneCorvoTimer;

    [SerializeField] private Transform attackCheck;
    [SerializeField] private float attackCheckRadius;

    private Transform closestEnemy;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>(); // Animator bileşenini alın
    }

    private void Update()
    {
        cloneCorvoTimer -= Time.deltaTime;
        if (cloneCorvoTimer < 0)
        {
            sr.color = new Color(1, 1, 1, sr.color.a - (Time.deltaTime * colorLoosingSpeed));
            if (sr.color.a <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void AnimationTrigger()
    {
        cloneCorvoTimer -= 0.8f;
    }

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackCheck.position, attackCheckRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                //hit.GetComponent<Enemy>().DamageEffect();
                player.characterStats.DoDamage(hit.GetComponent<CharacterStats>());
            }
        }
    }


    public void CloneCorvoSetup(Transform _newTransform, float _cloneCorvoDuration, bool _canAttack, Vector3 _offset, Player _player)
    {
        if (_canAttack)
        {
            if (anim != null)
            {
                anim.SetInteger("AttackNumber", Random.Range(1, 3)); // Saldırı animasyonunu ayarlayın
            }
        }
        else
        {
            if (anim != null)
            {
                anim.SetTrigger("Idle"); // Idle animasyonunu tetikleyin
            }
        }

        player = _player;

        transform.position = _newTransform.position + _offset ;
        cloneCorvoTimer = _cloneCorvoDuration;


        FocusTheTarget();
    }
   
    
    //Dusman bulmak ve en yakindakine vurması icin yazdim ilerde kisalticam
    private void FocusTheTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 25);

        float closestDistance = Mathf.Infinity;

        foreach ( var hit in colliders)
        {
            if ((hit.GetComponent<Enemy>() != null))
            {
                float distanceToEnemy = Vector2.Distance(transform.position,hit.transform.position); 

                if (distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    closestEnemy = hit.transform;
                }
            }
        }
        //Clon saldirirken arkaya donme sorununu cozdum
        if (closestEnemy != null)
        {
            if (transform.position.x > closestEnemy.position.x)
            {
                transform.Rotate(0, 180, 0);
            }
        }
    }
}
