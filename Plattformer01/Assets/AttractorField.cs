using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractorField : MonoBehaviour
{
    [SerializeField] public float gravityPullForce = .78f;
    [SerializeField] public float gravityPullForceVersion2 = 40f;
    private float m_GravityRadius = 1f;
    [HideInInspector] public GameObject character;
    [HideInInspector] public PhysicsComponent playerPhysComp;
    private bool continueGravity = true;
    [SerializeField] private bool version1 = true;
    Vector3 oldVelocity;
    public float RotationMargin = -1f;
    public float TurnSpeed = 25;


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
            oldVelocity = playerPhysComp.velocity;
            playerPhysComp.velocity = playerPhysComp.velocity / 10;

            playerPhysComp.direction = Vector3.zero;
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
            playerPhysComp.SetVelocity(playerPhysComp.velocity * 0.8f);
            Attract();
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerPhysComp.velocity = playerPhysComp.direction * playerPhysComp.GetVelocity().magnitude;
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

             playerPhysComp.velocity += (transform.parent.position - character.transform.position)  *  Time.smoothDeltaTime;
             playerPhysComp.direction += (transform.parent.position - character.transform.position) ;
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
        if (facing.magnitude < RotationMargin) { return; }

        // Rotate the rotation AWAY from the player...
        Quaternion awayRotation = Quaternion.LookRotation(facing);
        Vector3 euler = awayRotation.eulerAngles;
        euler.y -= 180;
        awayRotation = Quaternion.Euler(euler);

        // Rotate the game object.
        character.transform.rotation = Quaternion.Slerp(character.transform.rotation, awayRotation, TurnSpeed * Time.deltaTime);
        character.transform.eulerAngles = new Vector3(0, character.transform.eulerAngles.y, 0);
    }
}

