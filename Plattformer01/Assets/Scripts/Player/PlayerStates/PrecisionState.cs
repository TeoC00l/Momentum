using System.Collections;
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
        owner.SetStrafeMultiplier(strafeCoefficient);
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
        //owner.PhysComp.AddNormalForces();
    }

    public override void HandleUpdate()
    {
        //Checking for conditions to change state
        if (!owner.controllerInput.GetIsPrecisionModeActive())
        {
            owner.Transition<MomentumState>();
        }

        if (owner.controllerInput.GetIsJumping())
        {
            owner.Transition<JumpState>();
        }

        if (owner.dashCooldownTimer.IsReady() && owner.controllerInput.GetIsDashingLeft() || owner.controllerInput.GetIsDashingRight())
        {
            owner.Transition<DashState>();
        }
    }
}
