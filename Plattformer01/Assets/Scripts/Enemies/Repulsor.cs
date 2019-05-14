using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repulsor : StateMachine
{
    //Attributes
    [HideInInspector] public MeshRenderer[] Renderer;
    [HideInInspector] public CapsuleCollider blastCollider;
    [HideInInspector] public GameObject character;
    [HideInInspector] public PhysicsComponent playerPhysComp;
    [SerializeField] public Timer repulsorCooldownTimer;
    [SerializeField] public Vector3 IncreaseSizeOfForceFieldEachSecond;
    [SerializeField] public Vector3 MaxSizeOfForceField;
    [SerializeField] public Vector3 MinSizeOfForceField;

    protected override void Awake()
    {

        Renderer = GetComponentsInChildren<MeshRenderer>();
        base.Awake();

    }
    public void ChangeSizeOfForceField()
    {
        Vector3 parentScale = transform.localScale;
        foreach (MeshRenderer R in Renderer)
        {
            R.transform.localScale += IncreaseSizeOfForceFieldEachSecond;
        }
        transform.localScale = parentScale;
    }
}
