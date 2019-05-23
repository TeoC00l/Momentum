using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/DashState")]
public class DashState : PlayerBaseState
{
    //Attributes
    private Vector3 dash;
    private bool executeDash;

    public float dashDistance;
    [SerializeField] private Timer dashDurationTimer;


    //Methods
    public override void Enter()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            executeDash = true;
            dash = Camera.main.transform.rotation * Vector3.left;
        }

        if (Input.GetKey(KeyCode.E))
        {
            executeDash = true;
            dash = Camera.main.transform.rotation * Vector3.right;
        }
    }

    public override void HandleFixedUpdate()
    {
        RaycastHit hit = owner.RayCaster.GetCollisionData(dash, owner.PhysComp.GetSkinWidth());

        //executing dash
        if (executeDash == true)
        {
            executeDash = false;
            dash *= dashDistance * Time.deltaTime;

            if (hit.collider != null)
            {
                dash = Vector3.zero;
                owner.TransitionBack();
            }

            owner.PhysComp.AddVelocity(dash);
            owner.dashCooldownTimer.SetTimer();
            dashDurationTimer.SetTimer();
        }

        //checking for last frame of dash to cancel dash
        if (dashDurationTimer.CheckLastFrame() == true)
        {
            owner.PhysComp.SubtractVelocity(dash);
            owner.TransitionBack();
        }

        //checking for collision to cancel dash
        if (hit.collider != null)
        {
            owner.PhysComp.SubtractVelocity(dash);
            owner.TransitionBack();
        }

        owner.AddPhysics();
        owner.PhysComp.CollisionCalibration();
    }
}
