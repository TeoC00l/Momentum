using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    public Transform player;
    //public Movement2 playerSpeed;
    public PhysicsComponent playerSpeed2;
    public GameObject playerModel;
    public Transform drone;
    public Transform playerRespawn; 
    public float patroldistanceMax = 300;
    public float rotationspeed = 10f;
    public float speed = 1f;
    public float accelleration = 3;
    public float maxSpeed = 6f;
    public bool isPatroller = false;
    private float saveAccel;
    public UnityEngine.Events.UnityEvent damagePlayer;
    // Start is called before the first frame update
    void Start()
    {
        drone = GetComponent<Transform>();
        saveAccel = accelleration;
    }

    public void startPursuit()
    {
        isPatroller = false;
        //otherwise reload temp values.
    }

    public void stopPursuit()
    {
        //saveTempValues();
        isPatroller = true;
        speed = 0;
        accelleration = 0;
    }

    public void stopAcce()
    {
        accelleration = 5;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.layer == LayerMask.NameToLayer("Default"))
        {
            Debug.Log("DESTROYED. HA. HA.");
            player.transform.position = playerRespawn.transform.position;
            damagePlayer.Invoke();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //maxSpeed = playerSpeed.getSpeed();

        //maxSpeed = playerSpeed2.getVelocity().magnitude;

        drone.rotation = Quaternion.Slerp(drone.rotation, Quaternion.LookRotation(player.position - drone.position), rotationspeed * Time.deltaTime);

        if (!isPatroller)
        {
            if(maxSpeed > accelleration)
            {
                drone.position = Vector3.MoveTowards(drone.position, player.position, speed * Time.deltaTime * 5 * accelleration);
                accelleration += Time.deltaTime;
                saveAccel = accelleration;
            } else if (maxSpeed < accelleration)
            {
                drone.position = Vector3.MoveTowards(drone.position, player.position, speed * Time.deltaTime * 5 * accelleration/10);
            }
            
        }

        if (isPatroller)
        {
            //if player is near it, start pursuit
            if (Vector3.Distance(player.position, drone.position) > patroldistanceMax){
            drone.position = Vector3.MoveTowards(drone.position, player.position, speed * Time.deltaTime);
            }
        }

    }
}
