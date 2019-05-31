﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/DashState")]
public class DashState : PlayerBaseState
{
    //Attributes
    private Vector3 dashDirection;
    private Vector3 dashVelocity;
    private bool isExecutingDash;

    [SerializeField] private float dashDistance;
    [SerializeField] private Timer dashDurationTimer;


    //Methods
    public override void Enter()
    {
        dashVelocity = Vector3.zero;
        isExecutingDash = true;

        if (Input.GetKey(KeyCode.Q))
        {            
            dashDirection = Camera.main.transform.rotation * Vector3.left;
        }

        if (Input.GetKey(KeyCode.E))
        {
            dashDirection = Camera.main.transform.rotation * Vector3.right;
        }

        owner.dashCooldownTimer.SetTimer();
        dashDurationTimer.SetTimer();
    }

    public override void HandleFixedUpdate()
    {
        if (Time.timeScale == 1)
        {
            //executing dash
            owner.PhysComp.AddVelocity(dashVelocity);
            owner.AddPhysics();
            owner.PhysComp.CollisionCalibration();
            owner.PhysComp.SubtractVelocity(dashVelocity);
        }
    }

    public override void HandleUpdate()
    {
        dashDurationTimer.SubtractTime();

        //checking for last frame of dash to cancel dash
        if (dashDurationTimer.IsReady() == true)
        {
            Debug.Log("poop");
            owner.TransitionBack();
        }

        //calculating dash
        if (isExecutingDash == true)
        {
            isExecutingDash = false;

            dashVelocity = dashDirection * dashDistance * Time.deltaTime;
        }

        RaycastHit hit = owner.RayCaster.GetCollisionData(dashVelocity, owner.PhysComp.GetSkinWidth());

        if (hit.collider != null)
        {
            owner.TransitionBack();
        }
    }


}
