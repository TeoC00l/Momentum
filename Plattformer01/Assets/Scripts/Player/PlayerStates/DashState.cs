using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/DashState")]
public class DashState : PlayerBaseState
{
    //Attributes
    private Vector3 dashVelocity;
    private bool isExecutingDash;

    public float dashDistance;
    [SerializeField] private Timer dashDurationTimer;


    //Methods
    public override void Enter()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            isExecutingDash = true;
            dashVelocity = Camera.main.transform.rotation * Vector3.left;
        }

        if (Input.GetKey(KeyCode.E))
        {
            isExecutingDash = true;
            dashVelocity = Camera.main.transform.rotation * Vector3.right;
        }
    }

    public override void HandleFixedUpdate()
    {
        if (Time.timeScale == 1)
        {
            RaycastHit hit = owner.RayCaster.GetCollisionData(dashVelocity, owner.PhysComp.GetSkinWidth());

            //executing dash
            if (isExecutingDash == true)
            {
                isExecutingDash = false;
                dashVelocity *= dashDistance * Time.deltaTime;

                if (hit.collider != null)
                {
                    dashVelocity = Vector3.zero;
                    owner.TransitionBack();
                }

                owner.PhysComp.AddVelocity(dashVelocity);
                owner.dashCooldownTimer.SetTimer();
                dashDurationTimer.SetTimer();
            }

            //checking for last frame of dash to cancel dash
            if (dashDurationTimer.CheckLastFrame() == true)
            {
                owner.PhysComp.SubtractVelocity(dashVelocity);
                owner.TransitionBack();
            }

            //checking for collision to cancel dash
            if (hit.collider != null)
            {
                owner.PhysComp.SubtractVelocity(dashVelocity);
                owner.TransitionBack();
            }

            owner.AddPhysics();
            owner.PhysComp.CollisionCalibration();
        }
    }
}
