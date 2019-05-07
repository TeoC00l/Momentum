using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repuslor : MonoBehaviour
{
    //Attributes
    public RayCasterCapsule rayCaster;
    public CapsuleCollider capsuleCollider;
    public GameObject character;
    public PhysicsComponent playerPhysComp;
    public Vector3 playerVector;

    public float repulsorMagnitude;
    public float baseRepulsorMagnitude;
    
    //Methods
    void Awake()
    {
        rayCaster = GetComponent<RayCasterCapsule>();
        character = GameObject.Find("Character");
        playerPhysComp = character.GetComponent<PhysicsComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        playerVector = (character.transform.position - transform.position);
        RaycastHit hit = rayCaster.GetCollisionData((playerVector.normalized * repulsorMagnitude), 0);

        if (hit.collider != null)
        {
            //character.transform.position += (repulsorMagnitude - hit.distance) * Vector3.ProjectOnPlane-hit.normal;
            playerPhysComp.velocity = ((-playerPhysComp.velocity /2) + (playerVector.normalized * baseRepulsorMagnitude * Time.deltaTime));
        }
    }
}
