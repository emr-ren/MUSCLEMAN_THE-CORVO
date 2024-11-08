using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ParllaxEffect : MonoBehaviour
{
    [SerializeField] GameObject cam;
    [SerializeField] private float parallaxEffect;
    private float parallaxOffset;
    private float length;

    void Start()
    {
        /* Kamera'nin ismini degistirirsem bu yanlis olur 
         * cam = GameObject.Find("Main Camera");
        */
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        parallaxOffset = transform.position.x;

    }

    // Update is called once per frame
    void Update()
    {
        float distanceMoved = cam.transform.position.x * (1 - parallaxEffect);
        float moveDistance = cam.transform.position.x * parallaxEffect;
        transform.position = new Vector3(parallaxOffset + moveDistance, transform.position.y);

        if (distanceMoved > parallaxOffset + length)
        {
            parallaxOffset = parallaxOffset + length;
        }
        else if (distanceMoved < parallaxOffset - length)
        {
            parallaxOffset = parallaxOffset - length;
        }
    }
}
