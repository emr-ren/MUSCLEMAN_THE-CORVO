using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorvoStats : CharacterStats
{
    private Player player;
    protected override void Start()
    {
        base.Start();

        player = GetComponent<Player>();
    }

    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);
    }

    protected override void Dead()
    {
        base.Dead();

        player.Die();
    }
}
