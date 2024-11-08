using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashSkill : PlayerSkill
{
    public override void UseSkill()
    {
        base.UseSkill();

        Debug.Log("Clone behind");
    }
}
