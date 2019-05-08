using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Drone/DroneIdleState")]
public class DroneIdleState : DroneBaseState
{
    //Attributes
    public Timer PursuitStartupTimer;

    //Methods

    public override void Enter()
    {
        base.Enter();
    }

    public override void HandleUpdate()
    {

        if (CanSeePlayer())
        {
            if (!PursuitStartupTimer.IsCountingDown())
            {
                PursuitStartupTimer.SetTimer();
            }
        }

        if (PursuitStartupTimer.CheckLastFrame())
        {
            owner.Transition<DronePursuitState>();
        }

        PursuitStartupTimer.SubtractTime();
    }

}


