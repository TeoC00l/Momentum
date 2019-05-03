using UnityEngine;
using System.Collections;


public class DestructibleObject : MonoBehaviour
{
    //Attributes
    [HideInInspector] private GameObject destructibleObject;
    [HideInInspector] private PhysicsComponent collidingPhysComp;

    [SerializeField] protected float destructionVelocity;

    //Methods
    void Awake()
    {
        destructibleObject = gameObject.transform.parent.gameObject;
    }

    void OnTriggerEnter(Collider other)
    {
        collidingPhysComp = other.GetComponent<PhysicsComponent>();

        if (collidingPhysComp.GetVelocity().magnitude >= destructionVelocity*Time.deltaTime)
        {
            Object.Destroy(destructibleObject);
        }
        else
        {
            destructibleObject.layer = LayerMask.NameToLayer("Scoop");
        }
    }
}
