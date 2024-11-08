using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    private Entity entity;
    private CharacterStats stats;
    private RectTransform theTransform;

    private Slider slider;
    private void Start()
    {
        theTransform = GetComponent<RectTransform>();
        
        entity = GetComponentInParent<Entity>();
        stats  = GetComponentInParent<CharacterStats>();

        slider = GetComponentInChildren<Slider>();



        entity.FlipTheUI += FlipUI;

        //Yeni can kontrolu - event olusturarak
        stats.Health += HealthControlUI;
        HealthControlUI();
    }

    private void Update()
    {   /* Eski can kontrolu
        HealthControlUI();
        */
    }

    private void HealthControlUI()
    {                                                  //Her 1 vitality puaninda 1*5 can
        slider.maxValue = stats.CalculateTotalHealth();
        slider.value = stats.currentHealth;
    }

    private void FlipUI() => theTransform.Rotate(0, 180, 0);

    private void OnDisable() 
    {
        entity.FlipTheUI -= FlipUI;
        stats.Health -= HealthControlUI;
    }

}
