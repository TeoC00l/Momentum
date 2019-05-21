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
        if (!owner.PhysComp.GroundCheck())
        {
            owner.Transition<MomentumAirbourneState>();
        }
        //Making adjustments to physics

        owner.AddPhysics();
        owner.PhysComp.CollisionCalibration();
    }

    public override void HandleUpdate()
    {
        //Checking for conditions to change state
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            owner.Transition<PrecisionState>();
        }

        if (Input.GetMouseButtonDown(0) && owner.kineticBatteryCooldownTimer.IsReady())
        {
            owner.Transition<KineticBatteryState>();
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
