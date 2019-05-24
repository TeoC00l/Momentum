using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/RespawnState")]
public class RespawnState : PlayerBaseState
{
    //Attributes 
    public Timer RespawnStateExitTimer;

    //Methods
    public override void Enter()
    {
        RespawnStateExitTimer.SetTimer();
        owner.PhysComp.SetVelocity(Vector3.zero);
    }

    public override void HandleFixedUpdate()
    {
        owner.PhysComp.AddForces();
        owner.PhysComp.CollisionCalibration();
    }

    public override void HandleUpdate()
    {
        RespawnStateExitTimer.SubtractTime();

        if (RespawnStateExitTimer.IsReady())
        {
            owner.Transition<MomentumState>();
        }
    }
}
