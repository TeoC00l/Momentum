using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Drone/PursuitState")]
public class DronePursuitState : DroneBaseState
{
    public override void Enter()
    {
        owner.navMeshAgent.isStopped = false;
    }
    public override void HandleUpdate()
    {
        owner.navMeshAgent.SetDestination(owner.player.transform.position);
    }
}
