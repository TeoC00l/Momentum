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
    public bool jump;

    //Methods
    public override void Enter()
    {
        base.Enter();

        physComp.acceleration = this.acceleration;
        physComp.gravitationalForce = this.gravitationalForce;
        physComp.JumpMagnitude = this.jumpMagnitude;
        physComp.staticFrictionCo = this.staticFrictionCo;
        physComp.airResistance = this.airResistance;
    }

    public override void HandleUpdate()
    {
        //Checking for conditions to change state


        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            owner.Transition<MomentumState>();
        }

        owner.Dash();

        if (Input.GetKeyDown("space"))
        {
            jump = true;
        }
    }

    public override void HandleFixedUpdate()
    {
        if (!owner.physComp.GroundCheck())
        {
            owner.Transition<PrecisionAirbourneState>();
        }

        if (jump)
        {
            owner.physComp.Jump();
            jump = false;
        }

        if (owner.grounded)
        {
            owner.Transition<PrecisionAirbourneState>();
        }

        //Making adjustments to physics
        physComp.AddForces();

        owner.physComp.CollisionCalibration();
    }
}
