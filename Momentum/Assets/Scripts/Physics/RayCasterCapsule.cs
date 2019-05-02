using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCasterCapsule : MonoBehaviour
{
    //Attributes
    [SerializeField] protected LayerMask layerMask;
    [SerializeField] protected CapsuleCollider capsuleCollider;
    [HideInInspector]private RaycastHit hit;

    //Methods
    private void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    public RaycastHit GetCollisionData(Vector3 collisionVelocity, float skinWidth)
    {
        Vector3 point1 = (capsuleCollider.center + Vector3.up) * (capsuleCollider.height / 2 - capsuleCollider.radius);
        Vector3 point2 = (capsuleCollider.center + Vector3.down) * (capsuleCollider.height / 2 - capsuleCollider.radius);
        Physics.CapsuleCast(capsuleCollider.transform.position + point1, capsuleCollider.transform.position + point2, capsuleCollider.radius, collisionVelocity.normalized, out hit, collisionVelocity.magnitude + skinWidth, layerMask);

        return hit;
    }
}
