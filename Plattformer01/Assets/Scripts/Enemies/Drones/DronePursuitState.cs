using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Drone/PursuitState")]
public class DronePursuitState : DroneBaseState
{
    public override void Enter()
    {
        Debug.Log("Drone entered pursuit State!");
        owner.navMeshAgent.isStopped = false;
    }

    public override void HandleFixedUpdate()
    {
       // ScanForToggleGUI();
        owner.navMeshAgent.SetDestination(owner.player.transform.position);
    }
}
