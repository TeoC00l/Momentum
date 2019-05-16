using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepulsorField : MonoBehaviour
{

    //Attributes
    [HideInInspector] public GameObject character;
    [HideInInspector] public PhysicsComponent playerPhysComp;
    private MeshRenderer forceFieldMesh;
    private Vector3 expand = new Vector3(4f, 4f, 4f);
    private State oldState;
    private RepulsorActiveState active;
    private float timesTooExpand = 0;
    private Repulsor repulsor;
    private Vector3 oldScale;
    private bool bouncing;
    [SerializeField]private float bounceKnockback;
    public void Awake()
    {
        character = GameObject.FindWithTag("Player");
        playerPhysComp = character.GetComponent<PhysicsComponent>();
        forceFieldMesh = GetComponent<MeshRenderer>();
        repulsor = transform.parent.GetComponent<Repulsor>();

    }

    void OnTriggerEnter(Collider other)
    {
        if (bouncing == false)
        {
            oldScale = transform.localScale;
            CancelInvoke("BouncyRepulors");
            timesTooExpand = 0;
            expand = new Vector3(-2f, -2f, -2f);
            oldState = repulsor.GetCurrentState();
            active = (RepulsorActiveState)repulsor.getSpecificState()[0];
            if (playerPhysComp.GetVelocity().magnitude == 0)
            {
                playerPhysComp.SetVelocity(transform.forward);
                playerPhysComp.SetVelocity(-playerPhysComp.GetVelocity() * 1 - transform.forward * bounceKnockback);
            }
            if (playerPhysComp.GetVelocity().magnitude < 30)
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
    //    forceFieldMesh.transform.localScale += Expand;
    //    forceFieldMesh.transform.localScale -= Expand;
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
