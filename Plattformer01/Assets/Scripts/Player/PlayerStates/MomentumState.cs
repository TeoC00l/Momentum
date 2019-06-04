using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/MomentumState")]
public class MomentumState : PlayerBaseState
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
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetAxisRaw("Joystick L trigger") > 0)
        {
            owner.Transition<PrecisionState>();
        }

        if (Input.GetMouseButtonDown(0) || (Input.GetAxisRaw("Joystick R trigger") > 0) && owner.kineticBatteryCooldownTimer.IsReady())
        {
            owner.Transition<KineticBatteryState>();
        }

        if (Input.GetKeyDown("space") || Input.GetKeyDown("joystick button 0"))
        {
            owner.Transition<JumpState>();
        }

        if (owner.dashCooldownTimer.IsReady() && (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E)) || Input.GetKeyDown("joystick button 4") || Input.GetKeyDown("joystick button 5"))
        {
            owner.Transition<DashState>();
        }
    }
}
