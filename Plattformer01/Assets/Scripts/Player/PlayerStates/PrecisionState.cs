﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/PrecisionState")]
public class PrecisionState : PlayerBaseState
{
    //Attributes
    public float acceleration;
    public float gravitationalForce;
    public float jumpMagnitude;
    public float staticFrictionCo;
    public float airResistance;
    public float strafeCoefficient;

    //Methods
    public override void Enter()
    {
        base.Enter();
        PhysComp.SetAcceleration(acceleration);
        PhysComp.SetGravitationalForce(gravitationalForce);
        PhysComp.SetJumpMagnitude(jumpMagnitude);
        PhysComp.SetStaticFrictionCo(staticFrictionCo);
        PhysComp.SetAirResistance(airResistance);
        owner.SetStrafeCoefficient(strafeCoefficient);
    }

    public override void HandleFixedUpdate()
    {
        //Checking for conditions to change state
        if (owner.PhysComp.GroundCheck() == false)
        {
            owner.Transition<PrecisionAirbourneState>();
        }

        //Making adjustments to physics
        owner.AddPhysics();
        owner.PhysComp.CollisionCalibration();
    }

    public override void HandleUpdate()
    {
        if (Time.timeScale == 1)
        {
            //Checking for conditions to change state
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                owner.Transition<MomentumState>();
            }

            if (Input.GetKeyDown("space"))
            {
                owner.Transition<JumpState>();
            }

            if (owner.dashCooldownTimer.IsReady() && (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E)))
            {
                owner.Transition<DashState>();
            }
        }
    }
}
