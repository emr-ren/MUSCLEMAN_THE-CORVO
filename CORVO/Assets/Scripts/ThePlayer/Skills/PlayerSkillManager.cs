using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillManager : MonoBehaviour
{
    public static PlayerSkillManager instance;

    #region Corvo's Skill

    public DashSkill dash { get; private set;}
    public CloneCorvoSkill cloneCorvo { get; private set;}
    public ShadowKnifeSkill shadowKnife { get; private set;}   
    public TheDarkSideSkill theDarkSideSkill { get; private set;}

    #endregion


    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        instance = this;
    }


    private void Start()
    {
        dash             = GetComponent<DashSkill>() ;
        cloneCorvo       = GetComponent<CloneCorvoSkill>() ;
        shadowKnife      = GetComponent<ShadowKnifeSkill>() ;
        theDarkSideSkill = GetComponent<TheDarkSideSkill>() ;   
    }
}
