using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractorField : MonoBehaviour
{
    //Attributes
    private PhysicsComponent playerPhysComp;
    private GameObject character;
    private Vector3 oldVelocity;
    private float m_GravityRadius = 1f;

    //Methods
    void Awake()
    {
        character = GameObject.FindWithTag("Player");
        m_GravityRadius = GetComponent<SphereCollider>().radius;
        playerPhysComp = character.GetComponent<PhysicsComponent>();

    }

    //Slow Player Down When Entering field
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            oldVelocity = playerPhysComp.GetVelocity();
            playerPhysComp.SetVelocity(playerPhysComp.GetVelocity() / 10);
            playerPhysComp.SetDirection(Vector3.zero);        
        }
    }

    //Keep Player Stuck in field
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            playerPhysComp.SetVelocity(playerPhysComp.GetVelocity() * 0.8f);
            
            Attract();
        }
    }

    //Shoot Player Away When Exiting
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Vector3 dir = playerPhysComp.GetDirection();
            playerPhysComp.SetVelocity(dir * playerPhysComp.GetVelocity().magnitude);
            playerPhysComp.SetVelocity(oldVelocity);
            CancelInvoke("Attract");
        }
    }

    //Rotate Field for cool Effect
    void Update()
    {
        transform.Rotate(10f * Time.deltaTime, 5f * Time.deltaTime, 1f * Time.deltaTime);
    }

    //Keep Player Stuck in field
    private void Attract()
    {
        float gravityIntensity = Vector3.Distance(transform.parent.position, character.transform.position) / m_GravityRadius;
     
        playerPhysComp.AddToVelocity((transform.parent.position - character.transform.position) * Time.smoothDeltaTime);
        playerPhysComp.AddToDirection((transform.parent.position - character.transform.position));
        character.transform.RotateAround(transform.parent.position, transform.parent.up, 300*Time.smoothDeltaTime);
        
        Debug.DrawRay(character.transform.position, transform.parent.position - character.transform.position);
    }
}

