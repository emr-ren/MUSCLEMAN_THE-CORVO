using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EntityFX : MonoBehaviour
{
    private SpriteRenderer sr;

    [Header("Hit FX")]
    
    [SerializeField] private Material hitMaterial;
    [SerializeField] private float hitDuration;
    private Material defaultMaterial;

    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        defaultMaterial = sr.material;
    }

    public void MakeTransprent(bool _transprent)
    {
        if (_transprent)
            sr.color = Color.clear;
        else
            sr.color = Color.white;
    }


    private IEnumerator HitFX()
    {
        sr.material = hitMaterial;

        yield return new WaitForSeconds(hitDuration); 
        sr.material = defaultMaterial;
    
    }

    private void ColorBlink()
    {
        if (sr.color != Color.white)
        {
            sr.color = Color.white;
        }
        else
        {
            sr.color = Color.red;
        }
    }

    private void CancelBlink()
    {
        CancelInvoke();
        sr.color = Color.white;
    }

}
