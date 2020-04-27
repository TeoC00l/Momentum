using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixAnimationMove : MonoBehaviour
{
    private Vector3 animationOffset = Vector3.zero;

    [SerializeField] private GameObject player;
    void Start()
    {
        //animationOffset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = player.transform.position;
        transform.rotation = player.transform.rotation;
    }
}
