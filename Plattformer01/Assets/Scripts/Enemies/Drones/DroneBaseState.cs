using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Drone/BaseState")]
public class DroneBaseState : State
{
    //Attributes
    protected Drone owner;

    //Methods
    public override void Enter()
    {
    }

    public override void Initialize(StateMachine owner)
    {
        this.owner = (Drone)owner;
    }

    protected bool CanSeePlayer()
    {
        return ScanForPlayer(owner.detectionDistance);
    }

    protected void ScanForToggleGUI()
    {
        Vector3 directionToTarget = owner.player.transform.position - owner.transform.position;
        float angle = Vector3.Angle(owner.player.transform.forward, directionToTarget);

        if (Mathf.Abs(angle) < 90)
        {
            if (ScanForPlayer(owner.GUIToggleDistance) == true)
            {
               // owner.ToggleGUI(true);
            }
            
        }
        else
        {
           // owner.ToggleGUI(false);
        }
    }

    protected bool ScanForPlayer(float scanDistance)
    {
        Vector3 direction = owner.player.gameObject.transform.position - owner.gameObject.transform.position;
        return Physics.Raycast(owner.transform.position, direction.normalized, scanDistance, owner.visionMask);
    }
}
