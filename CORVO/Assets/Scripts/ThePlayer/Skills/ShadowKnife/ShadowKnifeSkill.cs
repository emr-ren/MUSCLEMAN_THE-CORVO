using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShadowKnifeType
{
    Standart,
    Bounce,
    Pierce,
    Spin
}


public class ShadowKnifeSkill : PlayerSkill
{
    public ShadowKnifeType shadowKnifeType = ShadowKnifeType.Standart; 

    [Header("Bounce Info")]
    [SerializeField] private int enemyTargetBounce;
    [SerializeField] private float bounceGravity;
    [SerializeField] private float bounceSpeed;
    [Header("Pierce Info")]
    [SerializeField] private int enemyTargetPierce;
    [SerializeField] private float pierceGravity;

    [Header("Spin Info")]
    [SerializeField] private float hitCoolDown;
    [SerializeField] private float maxShadowKnifeDistance;
    [SerializeField] private float spinDuration;
    [SerializeField] private float spinGravity;

    [Header("Shadow Knife Info")]
    [SerializeField] private GameObject shadowKnifePrefab;
    [SerializeField] private Vector2 throwingForce;
    [SerializeField] private float shadowKnifeGravity;
    [SerializeField] private float freezeTimeDuration;
    [SerializeField] private float returnSpeed;

    private Vector2 finalDir;

    [Header("Aim")]
    [SerializeField] private int numberOfDots;
    [SerializeField] private float spaceBeetwenDots;
    [SerializeField] private GameObject dotPrefab;
    [SerializeField] private Transform dotsParent;
    private GameObject[] dots;



    protected override void Start()
    {
        base.Start();

        GenerateDots();

        SetupGravity();
    }

    private void SetupGravity()
    {
        if (shadowKnifeType == ShadowKnifeType.Bounce)
            shadowKnifeGravity = bounceGravity;

        else if (shadowKnifeType == ShadowKnifeType.Pierce)
            shadowKnifeGravity = pierceGravity;

        else if (shadowKnifeType == ShadowKnifeType.Spin)
            shadowKnifeGravity = spinGravity;

    }

    protected override void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            finalDir = new Vector2(TheAim().normalized.x * throwingForce.x, TheAim().normalized.y* throwingForce.y);
        }

        if (Input.GetKey(KeyCode.R))
        {
            for (int i = 0; i < dots.Length; i++)
            {
                dots[i].transform.position = DotsPosition(i * spaceBeetwenDots);
            }
        }
    }

    public void CreateShadowKnife()
    {
        GameObject theShadowKnife = Instantiate(shadowKnifePrefab, player.transform.position, transform.rotation);
        ShadowKnifeSkillController theShadowKnifeScript =  theShadowKnife.GetComponent<ShadowKnifeSkillController>();

        if (shadowKnifeType == ShadowKnifeType.Bounce)
            theShadowKnifeScript.SetupBounce(true, enemyTargetBounce, bounceSpeed);

        else if (shadowKnifeType == ShadowKnifeType.Pierce)
            theShadowKnifeScript.SetupPierce(enemyTargetPierce);

        else if (shadowKnifeType == ShadowKnifeType.Spin)
            theShadowKnifeScript.SetupSpin(true, maxShadowKnifeDistance, spinDuration, hitCoolDown);



        theShadowKnifeScript.SetupShadowKnife(finalDir, shadowKnifeGravity, player , freezeTimeDuration, returnSpeed);

        player.AssingNewShadowKnife(theShadowKnife);

        DotsActive(false);
    }



    #region Shadow Knife Aim 
    public Vector2 TheAim() 
    {
        Vector2 playerPosition = player.transform.position;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - playerPosition;

        return direction;
    }

    public void DotsActive(bool _isActive)
    {
        for (int i = 0; i < dots.Length; i++)
        {
            dots[i].SetActive(_isActive);
        }
    }
    private void GenerateDots()
    {
        dots = new GameObject[numberOfDots];
        
        for (int i = 0; i < numberOfDots; i++) 
        {
            dots[i] = Instantiate(dotPrefab, player.transform.position , Quaternion.identity, dotsParent);
            dots[i].SetActive(false);

        }
    }
    private Vector2 DotsPosition(float t) 
    {
        Vector2 position = (Vector2)player.transform.position +
            new Vector2(TheAim().normalized.x * throwingForce.x,
                        TheAim().normalized.y * throwingForce.y)* t +
                        0.5f * (Physics2D.gravity*shadowKnifeGravity) * (t*t);
        return position;
    }

    #endregion

}
