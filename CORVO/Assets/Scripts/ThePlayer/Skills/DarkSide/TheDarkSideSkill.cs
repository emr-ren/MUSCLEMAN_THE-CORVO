using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheDarkSideSkill : PlayerSkill
{
    [Header("Dark Side Attack")]
    [SerializeField] private int darkSideNumerOfAttacks;
    [SerializeField] private float darkSideAttackCooldown;
    [SerializeField] private float theDarkSideDuration;
    [Space]
    [Header("Dark Side Area")]
    [SerializeField] private GameObject darkSideAreaPrefab;
    [SerializeField] private float maxSize;
    [SerializeField] private float growSpeed;
    [SerializeField] private float closeSpeed;

    DarkSideSkillController darkSideScript;


    public override bool CanUseSkill()
    {
        return base.CanUseSkill();
    }

    public override void UseSkill()
    {
        base.UseSkill();
        GameObject newDarkSideArea = Instantiate(darkSideAreaPrefab, player.transform.position, Quaternion.identity);

        darkSideScript = newDarkSideArea.GetComponent<DarkSideSkillController>();

        darkSideScript.SetupDarkSide(maxSize, growSpeed, closeSpeed, darkSideNumerOfAttacks, darkSideAttackCooldown, theDarkSideDuration);
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public bool DarkSideEnd()
    {
        if (!darkSideScript)
            return false;
        


        if (darkSideScript.playerCanExitState)
        {
            darkSideScript = null;
            return true;
        }
        return false;
    }
}
