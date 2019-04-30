using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform player;
    public Transform locationOut;
    public GameObject playerObj;


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("something IN");
        if (other.gameObject.tag == "Player" || other.gameObject.layer == LayerMask.NameToLayer("Default"))
        {
            //playerObj.GetComponent<PhysicsComponent>().setPrecisionMode();
            Debug.Log("player IN");
            player.position = locationOut.position;
            
        }
        
    }


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
