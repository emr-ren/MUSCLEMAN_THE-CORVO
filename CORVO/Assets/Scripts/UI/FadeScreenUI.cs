using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScreenUI : MonoBehaviour
{
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        FadeToOut();
    }

    public void DarkToIn() => anim.SetTrigger("FadeOut");

    public void FadeToOut() => anim.SetTrigger("FadeIn");
}
