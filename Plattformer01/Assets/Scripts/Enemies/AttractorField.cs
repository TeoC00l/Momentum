using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractorField : MonoBehaviour
{
    [SerializeField] private float gravityPullForce = .78f;



    private PhysicsComponent playerPhysComp;
    private GameObject character;
    private Vector3 oldVelocity;
    private bool continueGravity = true;
    private float m_GravityRadius = 1f;
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
    void Update()
    {
        transform.Rotate(10f * Time.deltaTime, 5f * Time.deltaTime, 1f * Time.deltaTime);
    }
    private void Attract()
    {
        //  playerPhysComp.velocity = Vector3.zero;
        float gravityIntensity = Vector3.Distance(transform.parent.position, character.transform.position) / m_GravityRadius;
        //   RotateAwayFrom(transform.parent.position);
        //   character.transform.rotation = Quaternion.Euler(transform.parent.position.x - character.transform.position.x, transform.parent.position.y - character.transform.position.y, transform.parent.position.z - character.transform.position.z);
        playerPhysComp.AddToVelocity((transform.parent.position - character.transform.position) * Time.smoothDeltaTime);
        playerPhysComp.AddToDirection((transform.parent.position - character.transform.position));
        character.transform.RotateAround(transform.parent.position, transform.parent.up, 300*Time.smoothDeltaTime);
        //   Camera.main.gameObject.transform.RotateAround(transform.parent.position, transform.parent.up, 300 * Time.smoothDeltaTime);
        
        Debug.DrawRay(character.transform.position, transform.parent.position - character.transform.position);
    }
}

