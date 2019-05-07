using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Drone/DroneIdleState")]
public class DroneIdleState : DroneBaseState
{
    //Attributes
    public Timer PursuitStartupTimer;

    //Methods
    public override void HandleUpdate()
    {

        if (owner.CheckForPlayer())
        {
            owner.Transition<DronePursuitState>();
        }
    }

}


