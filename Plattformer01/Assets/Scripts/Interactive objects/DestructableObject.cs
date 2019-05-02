using UnityEngine;
using System.Collections;


public class DestructableObject : MonoBehaviour
{
    GameObject destructableObject;

    void Awake()
    {
        destructableObject = this.gameObject;
    }

    void OnTriggerEnter(Collider other)
    {
       Object.Destroy(destructableObject);
    }
}
