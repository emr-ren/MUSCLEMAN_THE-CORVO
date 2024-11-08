using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BODAnimationTriggers : MonoBehaviour
{
    private BOD enemy => GetComponentInParent<BOD>();

    private void AnimationTrigger()
    {
        enemy.AnimationFinishTrigger();
    }
    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackCheckRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Player>() != null)
            {
                CorvoStats target = hit.GetComponent<CorvoStats>();
                enemy.characterStats.DoDamage(target);
            }
        }
    }

    private void CounterWindow() => enemy.CounterAttackWindow();
    private void CloseCounterWindow() => enemy.CloseCounterAttackWindow();
}
