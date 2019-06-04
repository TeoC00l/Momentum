using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private bool follow = false;
    private GameObject Player;
    private Transform child;
    [SerializeField]private float speed;
    private GemSpeedPickUP gemPickup;


    // Update is called once per frame
    void FixedUpdate()
    {
        if(follow == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, speed * Time.smoothDeltaTime);
            speed += 0.2f;
            if (Vector3.Distance(child.position, transform.position) < 1f)
            {
                Player.GetComponent<ParticleSystem>().Play(true);
                gemPickup.UpdateGems();
                Destroy(this.gameObject);
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
            gemPickup = GameObject.FindWithTag("TextCanvas").GetComponent<GemSpeedPickUP>();
        }
    }
}
