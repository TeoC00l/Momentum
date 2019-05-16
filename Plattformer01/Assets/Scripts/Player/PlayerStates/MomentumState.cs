using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/MomentumState")]
public class MomentumState : PlayerBaseState
{
    //Attributes
    public float acceleration = 1f;
    public float gravitationalForce = 1f;
    public float jumpMagnitude = 20.0f;
    public float staticFrictionCo = 0.7f;
    public float airResistance = 0.7f;

    public float batteryCooldown = 1f;

    //Methods
    public override void Enter()
    {
        base.Enter();
        batteryTimer = batteryCooldown;
        physComp.acceleration = this.acceleration;
        physComp.gravitationalForce = this.gravitationalForce;
        physComp.JumpMagnitude = this.jumpMagnitude;
        physComp.staticFrictionCo = this.staticFrictionCo;
        physComp.airResistance = this.airResistance;
    }

    public override void HandleFixedUpdate()
    {
        //Making adjustments to physics
        physComp.AddForces();
        owner.physComp.CollisionCalibration();
    }


    public override void HandleUpdate()
    {
        //Checking for conditions to change state
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            owner.Transition<PrecisionState>();
        }

        if (!owner.physComp.GroundCheck())
        {
            owner.Transition<MomentumAirbourneState>();
        }

        if (Input.GetMouseButtonDown(0) && owner.kineticBatteryCooldownTimer.IsReady())
        {
            owner.Transition<KineticBatteryState>();
        }

        if (Input.GetKeyDown("space"))
        {
            owner.Transition<JumpState>();

        }
    }
}
