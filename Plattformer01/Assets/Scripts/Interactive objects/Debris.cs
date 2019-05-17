using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour
{
    private GameObject player;
    private Vector3 ejectVelocity;
    [SerializeField] private float randomize = 1.5f;
    [SerializeField] private float ejectSpeed = 20;
    [SerializeField] private float despawnTime = 5;
    private bool despawningOn = true;
    private float r1;
    private float r2;
    private float r3;
    private PhysicsComponent playerPhysComp;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        r1 = Random.Range(-ejectSpeed / 2, ejectSpeed);
        r2 = Random.Range(0.1f, ejectSpeed);
        r3 = Random.Range(-ejectSpeed / 2, ejectSpeed);
        playerPhysComp  = player.GetComponent<PhysicsComponent>();
    }

    // Update is called once per frame
    void Update()
    {

        ejectVelocity = playerPhysComp.GetVelocity() + new Vector3(
        playerPhysComp.GetVelocity().x + r1,
        playerPhysComp.GetVelocity().y + r2,
        playerPhysComp.GetVelocity().z + r3);
        transform.Translate(ejectVelocity * Time.deltaTime, Space.World);
    }

    //
}