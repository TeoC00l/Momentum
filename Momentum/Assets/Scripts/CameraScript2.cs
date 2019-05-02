using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript2 : MonoBehaviour

{
    //Attributes
    public float rotationX = 0.0f;
    public float rotationY = 0.0f;
    public float mouseSensitivity = 3f;
    Vector3 cameraPosition = Vector3.zero;
    public Vector3 cameraOffset = Vector3.zero;
    public GameObject player;
    
    //Methods
    void Start()
    {
        cameraOffset = transform.position - player.transform.position;
    }

    void Update()
    {
        AddRotation();
    }

    public void AddRotation()
    {
        rotationY += Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        rotationX -= Input.GetAxisRaw("Mouse Y") *mouseSensitivity;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
        Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0);
        cameraPosition = rotation * cameraOffset;
        transform.rotation = rotation;
        transform.position = player.transform.position + cameraPosition;
    }
}

