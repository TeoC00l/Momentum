using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/MomentumState")]
public class MomentumState : PlayerBaseState
{
    //Attributes
    [Header("STATE ATTRIBUTES")]
    [SerializeField]protected float acceleration;
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
        physicsComponent.SetAcceleration(acceleration);
        physicsComponent.SetGravitationalForce(gravitationalForce);
        physicsComponent.SetJumpMagnitude(jumpMagnitude);
        physicsComponent.SetStaticFrictionCo(frictionCoefficient);
        physicsComponent.SetAirResistance(airResistance);
        owner.SetStrafeMultiplier(strafeCoefficient);
    }

    public override void HandleFixedUpdate()
    {
        if (!owner.physicsComponent.GroundCheck())
        {
            owner.Transition<MomentumAirbourneState>();
        }

        owner.AddPhysics();
    }

    public override void HandleUpdate()
    {
        if (owner.controllerInput.GetIsPrecisionModeActive())
        {
            owner.Transition<PrecisionState>();
        }

        if (owner.controllerInput.GetIsJumping())
        {
            owner.Transition<JumpState>();
        }

        if (owner.dashCooldownTimer.IsReady() && (owner.controllerInput.GetIsDashingLeft() || owner.controllerInput.GetIsDashingRight()))
        {
            owner.Transition<DashState>();
        }

        if (owner.controllerInput.GetIsKineticBatteryActive() && owner.kineticBatteryCooldownTimer.IsReady())
        {
            owner.Transition<KineticBatteryState>();
        }
    }
}
