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

    //Methods
    public override void Enter()
    {
        base.Enter();
        physComp.SetAcceleration(acceleration);
        physComp.SetGravitationalForce(gravitationalForce);
        physComp.SetJumpMagnitude(jumpMagnitude);
        physComp.SetStaticFrictionCo(staticFrictionCo);
        physComp.SetAirResistance(airResistance);
    }

    public override void HandleFixedUpdate()
    {
        //Checking for conditions to change state
        if (owner.physComp.GroundCheck() == false)
        {
            owner.Transition<PrecisionAirbourneState>();
        }

        //Making adjustments to physics
        owner.AddPhysics();
        owner.physComp.CollisionCalibration();
    }

    public override void HandleUpdate()
    {
        //Checking for conditions to change state
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            owner.Transition<MomentumState>();
        }

        if (Input.GetKeyDown("space"))
        {
            owner.Transition<JumpState>();
        }
    }
}
