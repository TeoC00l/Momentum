using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repuslor : MonoBehaviour
{
    public RayCasterCapsule rayCaster;
    PhysicsComponent physComp;
    public CapsuleCollider capsuleCollider;

    // Start is called before the first frame update
    void Awake()
    {
        rayCaster = GetComponent<RayCasterCapsule>();
        capsuleCollider = GetComponent<CapsuleCollider>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 poop = capsuleCollider.transform.position;
        RaycastHit hit = rayCaster.GetCollisionData(poop, 1);
        if (hit.collider != null)
        {
            physComp = hit.collider.GetComponent<PhysicsComponent>();
            physComp.velocity = (-physComp.velocity.normalized * 4);
            Debug.Log("poop");
        }
    }
}
