using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/KineticBatteryState")]
public class KineticBatteryState : PlayerBaseState
{
    //Attributes
    Vector3 oldVelocity;

    //Methodss
    public override void Enter()
    {
        base.Enter();
        owner.kineticBatteryActive = true;
        oldVelocity = physComp.velocity;
        physComp.velocity = Vector3.zero;
    }

    public override void HandleUpdate()
    {
        //Adjusting direction
        RaycastHit hit = rayCaster.GetCollisionData(Vector3.down, 0.5f);
        float skinWidth = physComp.skinWidth;
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 input = new Vector3(0, 0, 1);
        input = Camera.main.transform.rotation * input.normalized;

        input = Vector3.ProjectOnPlane(input, hit.normal);
        input = input.normalized;

        Debug.DrawRay(owner.transform.position, input, Color.red, 1f);

        //Redirecting velocity
        if (Input.GetMouseButtonDown(0))
        {
            physComp.velocity = input * oldVelocity.magnitude;
            owner.AddPhysics();
            owner.physComp.CollisionCalibration();
            owner.Transition<MomentumState>();
        }
    }

    public override void Exit()
    {
        owner.kineticBatteryActive = false;
    }

}
