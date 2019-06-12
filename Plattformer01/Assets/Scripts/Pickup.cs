using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private bool follow = false;
    private GameObject Player;
    private Transform child;
    [SerializeField]private float speed;
    [Range(0.001f, 1f)]
    [SerializeField]
    private float addThisMuchSpeedInProcentForEachGem;

    private PhysicsComponent physcomp;
    private void Awake()
    {

        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(follow == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, speed * Time.smoothDeltaTime);
            speed += 1f;
            if (Vector3.Distance(child.position, transform.position) < 0.8f)
            {
                Player.GetComponent<ParticleSystem>().Play(true);
                Debug.Log("hit pickup");
                physcomp.AddToSpeedIncrease(addThisMuchSpeedInProcentForEachGem / 100);
                this.gameObject.SetActive(false);
            }
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            follow = true;
            Player = other.gameObject;
            child = Player.transform.GetChild(0);
            physcomp = Player.GetComponent<PhysicsComponent>();

        }
    }
}
