using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour
{
    public GameObject player;
    private Vector3 ejectVelocity;
    public float randomize = 1.5f;
    public float ejectSpeed = 20;
    public float despawnTime = 5;
    private bool despawningOn = true;
    private float r1;
    private float r2;
    private float r3;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("Character");
        r1 = Random.Range(-ejectSpeed / 2, ejectSpeed);
        r2 = Random.Range(0.1f, ejectSpeed);
        r3 = Random.Range(-ejectSpeed / 2, ejectSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        
        ejectVelocity = player.GetComponent<PhysicsComponent>().GetVelocity() + new Vector3(
            player.GetComponent<PhysicsComponent>().GetVelocity().x + r1,
            player.GetComponent<PhysicsComponent>().GetVelocity().y + r2,
            player.GetComponent<PhysicsComponent>().GetVelocity().z + r3);  

        transform.Translate(ejectVelocity * Time.deltaTime, Space.World);
    }

    //
}
