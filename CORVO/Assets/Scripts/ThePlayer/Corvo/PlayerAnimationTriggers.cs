using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    //Player nesnesini hazirda var olan ebeveyininden cekmesini sagladim
    public Player player => GetComponentInParent<Player>();
    private void AnimationTrigger()
    {
         player.AnimationFinishTrigger();
    }

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                EnemyStats _target = hit.GetComponent<EnemyStats>();
                player.characterStats.DoDamage(_target);


            }
        }
    }

    private void ThrowShadowKnife()
    {
        PlayerSkillManager.instance.shadowKnife.CreateShadowKnife();
    }
}
