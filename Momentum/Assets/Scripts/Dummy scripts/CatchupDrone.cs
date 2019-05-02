using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchupDrone : MonoBehaviour
{
    public GameObject player;
    public Transform playerLocation;
    public Transform droneLoc;
    public Transform pursuitMarker;
    public GameObject cameras;
    public float gameOverTime = 30;
    public float time;


    // Start is called before the first frame update
    void Start()
    {
        droneLoc = GetComponent<Transform>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.layer == LayerMask.NameToLayer("Default"))
        {
            cameras.transform.parent = null;
            Destroy(player);

        }
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime / gameOverTime;
        droneLoc.position = Vector3.Lerp(pursuitMarker.transform.position, playerLocation.transform.position, time);
    }
}
