using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixAnimationMove : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 animationOffset = Vector3.zero;

    [SerializeField] private GameObject player;
    void Start()
    {
        animationOffset = transform.position - player.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;
        transform.rotation = player.transform.rotation;
        //transform.rotation = Quaternion.Euler(player.transform.rotation.x, player.transform.rotation.y + 90f, player.transform.rotation.z);

    }
}
