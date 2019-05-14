using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/KineticBatteryState")]
public class KineticBatteryState : PlayerBaseState
{
    //Attributes
    Vector3 returnVelocity;
    bool StopOnce;
    //Methods
    public override void Enter()
    {
        base.Enter();
        owner.kineticBatteryActive = true;
        owner.oldVelocity = physComp.velocity;
        returnVelocity = physComp.velocity;
        StopOnce = false;
        owner.kineticTimer = 1000;
        owner.divideValue = owner.kineticTimer;
        owner.InvokeRepeating("DecreaseVelocity", 0f, 0.005f);
    }

    public override void HandleUpdate()
    {
        if (physComp.velocity == Vector3.zero && StopOnce == false)
        {
            StopOnce = true;
            owner.CancelInvoke("DecreaseVelocity");
        }
        if (StopOnce == false)
        {
            owner.physComp.AddForces();
            owner.physComp.CollisionCalibration();
        }

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
            owner.CancelInvoke("DecreaseVelocity");
            owner.kineticTimer = 0;
            physComp.velocity = input * returnVelocity.magnitude;
            owner.AddPhysics();
            owner.physComp.CollisionCalibration();
            owner.Transition<MomentumState>();
        }
    }

    public override void Exit()
    {
        owner.kineticBatteryActive = false;
        owner.kineticBatteryCooldownTimer.SetTimer();
    }
   

}
