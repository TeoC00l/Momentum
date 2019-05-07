﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Drone/BaseState")]
public class DroneBaseState : State
{
    //Attributes
    protected Drone2 owner;

    //Methods
    public override void Enter()
    {

    }

    public override void Initialize(StateMachine owner)
    {
        this.owner = (Drone2)owner;
    }

    protected bool CanSeePlayer()
    {
        Vector3 direction = owner.player.transform.position - owner.transform.position;

        return !Physics.Raycast(owner.transform.position, direction.normalized, owner.detectionDistance, owner.visionMask, QueryTriggerInteraction.UseGlobal);
    }
}