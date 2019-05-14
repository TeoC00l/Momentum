﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Repulsor/ActiveState")]
public class RepulsorActiveState : RepulsorBaseState
{
    [SerializeField] private float waitBeforeIncreasing;
    [SerializeField] private float increaseSizeOfForceFieldSpeed;
    public override void Enter()
    {
        Debug.Log("IncreaseField");
        owner.IncreaseSizeOfForceFieldEachSecond = new Vector3(Mathf.Abs(owner.IncreaseSizeOfForceFieldEachSecond.x), Mathf.Abs(owner.IncreaseSizeOfForceFieldEachSecond.y), Mathf.Abs(owner.IncreaseSizeOfForceFieldEachSecond.z));

        owner.InvokeRepeating("ChangeSizeOfForceField", waitBeforeIncreasing, increaseSizeOfForceFieldSpeed);
    }
    public override void HandleUpdate()
    {
        if (Vector3.Distance(owner.Renderer[1].transform.localScale, owner.MaxSizeOfForceField) < 0.3f)
        {
            owner.CancelInvoke("ChangeSizeOfForceField");
            Debug.Log("StopForceField");
            owner.Transition<RepulsorIdleState>();
        }
    }
}
