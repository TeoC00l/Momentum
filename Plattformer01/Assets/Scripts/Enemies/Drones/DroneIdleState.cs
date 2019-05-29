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
        owner.navMeshAgent.isStopped = true;
        owner.navMeshAgent.ResetPath();
        owner.navMeshAgent.velocity = Vector3.zero;
    }

    public override void HandleFixedUpdate()
    {
        if (CanSeePlayer() == true)
        {
            if (!PursuitStartupTimer.IsCountingDown())
            {
                Debug.Log("Player detected by " + owner.name);
                PursuitStartupTimer.SetTimer();
            }
        }
    }

    public override void HandleUpdate()
    {
        if (PursuitStartupTimer.CheckLastFrame())
        {
            owner.Transition<DronePursuitState>();
        }

        PursuitStartupTimer.SubtractTime();
    }      
}


