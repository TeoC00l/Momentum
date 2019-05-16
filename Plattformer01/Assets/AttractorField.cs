﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractorField : MonoBehaviour
{
    [SerializeField] public float gravityPullForce = .78f;
    [SerializeField] public float gravityPullForceVersion2 = 40f;
    [HideInInspector] public GameObject character;
    [HideInInspector] public PhysicsComponent playerPhysComp;
 
    [SerializeField] private bool version1 = true;
    private float m_GravityRadius = 1f;

    private bool continueGravity = true;
    private Vector3 oldVelocity;
    private float rotationMargin = -1f;
    private float turnSpeed = 25;


    void Awake()
    {
        character = GameObject.FindWithTag("Player");

        m_GravityRadius = GetComponent<SphereCollider>().radius;
        playerPhysComp = character.GetComponent<PhysicsComponent>();

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            oldVelocity = playerPhysComp.GetVelocity();
            playerPhysComp.SetVelocity(playerPhysComp.GetVelocity() / 10);

            playerPhysComp.SetDirection(Vector3.zero);
         //   InvokeRepeating("Attract", 0, 0.1f);


            if (Vector3.Distance(transform.parent.position, other.transform.position) < 0.4f)
            {
                Debug.Log("PlayerHitAttractor");
             //   continueGravity = false;
            }
          //  playerPhysComp.velocity = Vector3.zero;
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && continueGravity == true)
        {

            playerPhysComp.SetVelocity(playerPhysComp.GetVelocity() * 0.8f);
            Attract();
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Vector3 dir = playerPhysComp.GetDirection();
            playerPhysComp.SetVelocity(dir * playerPhysComp.GetVelocity().magnitude);
            playerPhysComp.SetVelocity(oldVelocity);
            CancelInvoke("Attract");
            continueGravity = true;
        }
    }
    private void Attract()
    {
        //  playerPhysComp.velocity = Vector3.zero;
        float gravityIntensity = Vector3.Distance(transform.parent.position, character.transform.position) / m_GravityRadius;
        // playerPhysComp.velocity = Vector3.zero;
        if (version1 == true)
        {
            //   RotateAwayFrom(transform.parent.position);
            //   character.transform.rotation = Quaternion.Euler(transform.parent.position.x - character.transform.position.x, transform.parent.position.y - character.transform.position.y, transform.parent.position.z - character.transform.position.z);

            playerPhysComp.AddToVelocity((transform.parent.position - character.transform.position) * Time.smoothDeltaTime);
             playerPhysComp.AddToDirection((transform.parent.position - character.transform.position));
            character.transform.RotateAround(transform.parent.position, transform.parent.up, 300*Time.smoothDeltaTime);

        }
        else
        {
            gravityPullForce = gravityPullForceVersion2;
            playerPhysComp.SetDirection((transform.parent.position - character.transform.position) * gravityPullForce);
        }
       
       // playerPhysComp.AddForces();
        Debug.DrawRay(character.transform.position, transform.parent.position - character.transform.position);
    }
    private void RotateAwayFrom(Vector3 position)
    {
        Vector3 facing = character.transform.position - position;
        if (facing.magnitude < rotationMargin) { return; }

        // Rotate the rotation AWAY from the player...
        Quaternion awayRotation = Quaternion.LookRotation(facing);
        Vector3 euler = awayRotation.eulerAngles;
        euler.y -= 180;
        awayRotation = Quaternion.Euler(euler);

        // Rotate the game object.
        character.transform.rotation = Quaternion.Slerp(character.transform.rotation, awayRotation, turnSpeed * Time.deltaTime);
        character.transform.eulerAngles = new Vector3(0, character.transform.eulerAngles.y, 0);
    }
}

