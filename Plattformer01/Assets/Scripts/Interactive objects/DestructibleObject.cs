using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class DestructibleObject : MonoBehaviour
{
    //Attributes
    [HideInInspector] private GameObject destructibleObject;
    [HideInInspector] private PhysicsComponent collidingPhysComp;
    [SerializeField] protected float destructionVelocity;
    

    //YL attributes
    [SerializeField]private List<GameObject> debris = new List<GameObject>();
    private GameObject player;
    private GameObject itself;
    [SerializeField] private GameObject explosionEffects;


    //Methods
    void Awake()
    {
        destructibleObject = gameObject.transform.parent.gameObject;
        player = GameObject.Find("Character");
        
    }

    void OnTriggerEnter(Collider other)
    {
        collidingPhysComp = other.GetComponent<PhysicsComponent>();
        if (collidingPhysComp != null)
        {
            if (collidingPhysComp.GetVelocity().magnitude >= destructionVelocity * Time.deltaTime)
            {

                var obj = Instantiate(explosionEffects, destructibleObject.GetComponent<Transform>().position, Quaternion.identity);
                obj.AddComponent<ParticleSplash>();

                destructibleObject.SetActive(false);

            }
            else
            {
                destructibleObject.layer = LayerMask.NameToLayer("Scoop");
            }
        }       
    }

    public void ResetDestructibleObject()
    {
        destructibleObject.SetActive(true);

    }
}