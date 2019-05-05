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
    public List<GameObject> debris = new List<GameObject>();
    private GameObject player;
    private GameObject itself;
    public GameObject explosionEffects;


    //Methods
    void Awake()
    {
        destructibleObject = gameObject.transform.parent.gameObject;
        player = GameObject.Find("Character");
        
    }

    void OnTriggerEnter(Collider other)
    {
        collidingPhysComp = other.GetComponent<PhysicsComponent>();

        if (collidingPhysComp.GetVelocity().magnitude >= destructionVelocity * Time.deltaTime)
        {
            //Particlescript start
            var obj = Instantiate(explosionEffects, destructibleObject.GetComponent<Transform>().position, Quaternion.identity);
            obj.AddComponent<ParticleSplash>();
            //Particlescript stop

            //spawnDebris();
            Object.Destroy(destructibleObject);
            //destructibleObject.GetComponent<Renderer>().enabled = false;

        }
        else
        {
            destructibleObject.layer = LayerMask.NameToLayer("Scoop");
        }
    }



    //depreciate this
    void spawnDebris()
    {
        //location of object.
        Transform spawnPos = destructibleObject.GetComponent<Transform>();

        //iterate through debris spawnlist
        for (int i = 0; i < debris.Count; i++)
        {
            var obj = Instantiate(debris[i], destructibleObject.GetComponent<Transform>().position, Quaternion.identity);
            obj.AddComponent<Debris>(); //attach script to debris after it spawns.
            //whats happening now is that you should identity it to.
        }
    }
}