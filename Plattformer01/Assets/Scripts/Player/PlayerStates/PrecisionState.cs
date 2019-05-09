using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/PrecisionState")]
public class PrecisionState : PlayerBaseState
{
    //Attributes
    public float acceleration = 1f;
    public float gravitationalForce = 1f;
    public float jumpMagnitude = 20.0f;
    public float staticFrictionCo = 0.7f;
    public float airResistance = 0.7f;
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


        if (!owner.physComp.GroundCheck())
        {
            owner.Transition<PrecisionAirbourneState>();
        }

        if (Input.GetKeyDown("space"))
        {
            jump = true;
        }
    }

    public override void HandleFixedUpdate()
    {
        if (jump)
        {
            owner.physComp.Jump();
            jump = false;
        }

        //Making adjustments to physics
        physComp.AddForces();

        owner.physComp.CollisionCalibration();
    }
}
