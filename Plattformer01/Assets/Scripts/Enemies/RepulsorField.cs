using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepulsorField : MonoBehaviour
{

    //Attributes
    [SerializeField] private float bounceKnockback = 2;


    private PhysicsComponent playerPhysComp;
    private MeshRenderer forceFieldMesh;
    private GameObject character;
    private Repulsor repulsor;
    private Vector3 oldScale;
    private Vector3 expand;
    private bool bouncing;
    private float timesTooExpand = 0;

    public void Awake()
    {
        character = GameObject.FindWithTag("Player");
        playerPhysComp = character.GetComponent<PhysicsComponent>();
        forceFieldMesh = GetComponent<MeshRenderer>();
        repulsor = transform.parent.GetComponent<Repulsor>();
        expand = new Vector3(4f, 4f, 4f);

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
        repulsor.Transition<RepulsorActiveState>();      
    }

}
