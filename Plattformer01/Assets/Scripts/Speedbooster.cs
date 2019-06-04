using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speedbooster : MonoBehaviour
{
    
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PhysicsComponent physComp = other.GetComponent<PhysicsComponent>();
            physComp.SetVelocity(physComp.GetVelocity() * 2);
        }
    }
}
