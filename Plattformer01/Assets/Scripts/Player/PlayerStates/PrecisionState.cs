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
    public bool isJumping;

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
