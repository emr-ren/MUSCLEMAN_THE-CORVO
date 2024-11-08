using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneCorvoSkill : PlayerSkill
{
    [Header("Clone Info")]
    [SerializeField] private GameObject clonePrefab;
    [SerializeField] public float cloneCorvoDuration;
    [Space]
    [SerializeField] public bool canAttack;

    public void CreateCloneCorvo(Transform _cloneCorvoPosition, Vector3 _offset)
    {
        Debug.Log("Creating CloneCorvo at position: " + _cloneCorvoPosition.position);
        GameObject newClone = Instantiate(clonePrefab);
        CloneCorvoSkillController controller = newClone.GetComponent<CloneCorvoSkillController>();
       
        controller.CloneCorvoSetup(_cloneCorvoPosition, cloneCorvoDuration, canAttack, _offset,player);
        
    }
}
