﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepulsorAttractor : StateMachine
{
    //Attributes
    public Timer repulsorCooldownTimer;
    [SerializeField] private Vector3 increaseSizeOfForceFieldEachSecond;
    [SerializeField] private Vector3 MaxSizeOfForceField;
    [SerializeField] private Vector3 MinSizeOfForceField;
    private MeshRenderer[] Renderer;


    //Methods
    protected override void Awake()
    {
        Renderer = GetComponentsInChildren<MeshRenderer>();
        base.Awake();
    }

    public void ChangeSizeOfForceField()
    {
        if (GetComponentInChildren<SphereCollider>().enabled == false)
        {
            GetComponentInChildren<SphereCollider>().enabled = true;
        }

        Vector3 parentScale = transform.localScale;

        foreach (MeshRenderer R in Renderer)
        {
            R.transform.localScale += increaseSizeOfForceFieldEachSecond;
        }

        transform.localScale = parentScale;
    }

    //GETTERS/SETTERS
    public Vector3 GetMinSizeOfForceField()
    {
        return MinSizeOfForceField;
    }
    public Vector3 GetMaxSizeOfForceField()
    {
        return MaxSizeOfForceField;
    }
    public Vector3 GetIncreaseSizeOfForceFieldEachSecond()
    {
        return increaseSizeOfForceFieldEachSecond;
    }
    public void SetIncreaseSizeOfForceFIeldEachSecond(Vector3 set)
    {
        increaseSizeOfForceFieldEachSecond = set;
    }
    public MeshRenderer[] GetMeshRenderers()
    {
        return Renderer;
    }
}
