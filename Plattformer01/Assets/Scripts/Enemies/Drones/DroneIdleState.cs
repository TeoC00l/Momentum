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
        Debug.Log("Scoop");
        base.Enter();
    }

    public override void HandleUpdate()
    {

        if (CanSeePlayer())
        {
            PursuitStartupTimer.SetTimer();
        }

        if (PursuitStartupTimer.CheckLastFrame())
        {
            Debug.Log("Poop");
            owner.Transition<DronePursuitState>();
        }

        PursuitStartupTimer.SubtractTime();
    }

}


