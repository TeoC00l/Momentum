﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepulsorField : MonoBehaviour
{

    //Attributes
    [SerializeField] private float bounceKnockback = 2;


    private PhysicsComponent playerPhysComp;
    private MeshRenderer forceFieldMesh;
    private GameObject character;
    private RepulsorAttractor repulsor;
    private Vector3 oldScale;
    private Vector3 expand;
    private bool bouncing;
    private float timesTooExpand = 0;

    //Methods
    public void Awake()
    {
        character = GameObject.FindWithTag("Player");
        playerPhysComp = character.GetComponent<PhysicsComponent>();
        forceFieldMesh = GetComponent<MeshRenderer>();
        repulsor = transform.parent.GetComponent<RepulsorAttractor>();

    }
    private void Update()
    {
        transform.Rotate(10f * Time.deltaTime, 5f * Time.deltaTime, 1f * Time.deltaTime);

    }

    void OnTriggerEnter(Collider other)
    {
        if (bouncing == false && other.tag == "Player")
        {
            oldScale = transform.localScale;
            CancelInvoke("BouncyRepulors");
            timesTooExpand = 0;
            expand = new Vector3(-2f, -2f, -2f);
            if (playerPhysComp.GetVelocity().magnitude == 0)
            {
                playerPhysComp.SetVelocity(transform.forward);
                playerPhysComp.SetVelocity(-playerPhysComp.GetVelocity() * 1 - transform.forward * bounceKnockback);
            }
            else if (playerPhysComp.GetVelocity().magnitude < 30)
            {
                playerPhysComp.SetVelocity(-playerPhysComp.GetVelocity() * 3 - transform.forward * bounceKnockback);
            }
            else
            {
                playerPhysComp.SetVelocity(-playerPhysComp.GetVelocity() - transform.forward * bounceKnockback);

            }
            InvokeRepeating("BouncyRepulors", 0f, 0.1f);
            bouncing = true;
        }
    }

    public void BouncyRepulors()
    {

        if (timesTooExpand < 1)
        {          
            timesTooExpand++;
            forceFieldMesh.transform.localScale += expand;
        }else
        {
            transform.localScale = oldScale;
            StopBounce();
        }
    }

    public void StopBounce()
    {
        CancelInvoke("BouncyRepulors");
        bouncing = false; 
        repulsor.Transition<RepulsorAttractorActiveState>();      
    }
}
