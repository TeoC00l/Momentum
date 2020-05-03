using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/PrecisionState")]
public class PrecisionState : PlayerBaseState
{
    //Attributes
    [Header("STATE ATTRIBUTES")]
    [SerializeField] protected float acceleration;
    [SerializeField] protected float gravitationalForce;

    [SerializeField] protected float jumpMagnitude;
    [Header("0 = no friction/ resistance  1 = maximum")]
    [Range(0.0f, 1.0f)]
    [SerializeField] protected float frictionCoefficient;
    [Range(0.0f, 1.0f)]
    [SerializeField] protected float airResistance;
    [Header("0 = can't strafe  1 = maximum strafe magnitude")]
    [Range(0.0f, 1.0f)]
    [SerializeField] protected float strafeCoefficient;

    //Methods
    public override void Enter()
    {
        base.Enter();
        PhysComp.SetAcceleration(acceleration);
        PhysComp.SetGravitationalForce(gravitationalForce);
        PhysComp.SetJumpMagnitude(jumpMagnitude);
        PhysComp.SetStaticFrictionCo(frictionCoefficient);
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
