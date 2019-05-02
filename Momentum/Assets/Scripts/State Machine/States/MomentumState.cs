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

    //Methods
    public override void Enter()
    {
        base.Enter();
        batteryTimer = 1f;
        physComp.acceleration = this.acceleration;
        physComp.gravitationalForce = this.gravitationalForce;
        physComp.JumpMagnitude = this.jumpMagnitude;
        physComp.staticFrictionCo = this.staticFrictionCo;
        physComp.airResistance = this.airResistance;
    }

    public override void HandleUpdate()
    {
        //Adjusting timer
        if (batteryTimer > 0)
        {
            batteryTimer -= Time.deltaTime;
        }

        //Checking for conditions to change state
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            owner.Transition<PrecisionState>();
        }

        if (!owner.physComp.GroundCheck())
        {
            owner.Transition<MomentumAirbourneState>();
        }

        if (Input.GetMouseButtonDown(0) && batteryTimer <= 0)
        {
            owner.Transition<KineticBatteryState>();
        }

        //Making adjustments to physics
        owner.AddPhysics();

        owner.Dash();

        if (Input.GetKeyDown("space"))
        {
            owner.physComp.Jump();
        }

        owner.physComp.CollisionCalibration();
    }
}
