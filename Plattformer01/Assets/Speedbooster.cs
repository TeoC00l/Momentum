using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speedbooster : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PhysicsComponent physComp = other.GetComponent<PhysicsComponent>();
            physComp.SetVelocity(physComp.GetVelocity() * 2);
        }
    }
}
