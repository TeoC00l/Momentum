using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractorField : MonoBehaviour
{
    [SerializeField] public float gravityPullForce = .78f;
    [SerializeField] public float gravityPullForceVersion2 = 40f;
    public static float m_GravityRadius = 1f;
    [HideInInspector] public GameObject character;
    [HideInInspector] public PhysicsComponent playerPhysComp;
    private bool continueGravity = true;
    [SerializeField] private bool version1 = true;

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
            playerPhysComp.velocity = Vector3.zero;
            InvokeRepeating("Attract", 0, 0.1f);

           
            if (Vector3.Distance(transform.parent.position, other.transform.position) < 0.4f)
            {
                Debug.Log("PlayerHitAttractor");
                continueGravity = false;
            }
          //  playerPhysComp.velocity = Vector3.zero;
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && continueGravity == true)
        {
          
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            CancelInvoke("Attract");
            continueGravity = true;
        }
    }
    private void Attract()
    {
        //  playerPhysComp.velocity = Vector3.zero;
        float gravityIntensity = Vector3.Distance(transform.parent.position, character.transform.position) / m_GravityRadius * gravityPullForce;
        // playerPhysComp.velocity = Vector3.zero;
        if (version1 == true)
        {
            playerPhysComp.velocity = (transform.parent.position - character.transform.position) * gravityIntensity * gravityPullForce * Time.smoothDeltaTime;
            playerPhysComp.SetDirection((transform.parent.position - character.transform.position));
        }
        else
        {
            gravityPullForce = gravityPullForceVersion2;
            playerPhysComp.SetDirection((transform.parent.position - character.transform.position) * gravityPullForce);
        }
       
        playerPhysComp.AddForces();
        Debug.DrawRay(character.transform.position, transform.parent.position - character.transform.position);
    }
}

