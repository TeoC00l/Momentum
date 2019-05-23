using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionToLevelChange : MonoBehaviour
{
    [SerializeField] private Transform targetLocation;
    [SerializeField] private float speedToTarget;
    private GameObject player;
    private Player playerScript;
    private PhysicsComponent physComp;
    private bool hitTrigger = false;
    private float step;
    private UITimer timer;
    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        timer = GameObject.FindWithTag("Canvas").GetComponent<UITimer>();

        playerScript = player.GetComponent<Player>();
        physComp = player.GetComponent<PhysicsComponent>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            hitTrigger = true;
            timer.SetPlayable(false);
          //  player.transform.LookAt(targetLocation);
            playerScript.SetNeutralizeInput(true);

        }
    }
    void FixedUpdate()
    {
        if (hitTrigger)
        {
            player.transform.LookAt(targetLocation);
            player.transform.position += new Vector3(player.transform.forward.x * speedToTarget, player.transform.forward.y, player.transform.forward.z * speedToTarget) ;
            physComp.SetDirection(player.transform.forward * step);
            physComp.SetVelocity(player.transform.forward * step);
        //    Vector3.MoveTowards(player.transform.position, targetLocation.position, step);

        }
        if(Vector3.Distance(player.transform.position, targetLocation.position) < 1f)
        {
            Debug.Log("gogogo");
        }
    }
}
