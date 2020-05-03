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
        owner.physicsComponent.SetVelocity(Vector3.zero);
        owner.physicsComponent.SetDirection(Vector3.zero);
    }

    public override void HandleFixedUpdate()
    {
        owner.physicsComponent.AddForces();
        //owner.PhysComp.AddNormalForces();
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
