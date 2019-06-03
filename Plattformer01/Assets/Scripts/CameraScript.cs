using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour

{
    //Attributes

        //Basic camera attributes
    private float rotationX;
    private float rotationY;
    private Vector3 cameraPosition;
    private Vector3 cameraOffset = Vector3.zero;
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private GameObject player;
    
        //Shake effect
    private bool shakeStart;
    private RaycastHit hit;
    private RaycastHit hit2;

    [SerializeField] private float shakeAmount;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float shakeLength;

    private float shakeCounter;
    private bool ray;
    private GameObject hitGameObject;
    private float hitAgain;
    private PhysicsComponent physComp;

    //Methods
    void Start()
    {
        rotationX = 0.0f;
        rotationY = 0.0f;
        cameraPosition = Vector3.zero;
        cameraOffset = transform.position - player.transform.position;

        Cursor.lockState = CursorLockMode.Locked;
        physComp = player.GetComponent<PhysicsComponent>();

        shakeStart = false;
        shakeCounter = 0;
        ray = false;
        hitAgain = 0;
    }

    void FixedUpdate()
    {        
        InitiateShake();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            changeCursorLockMode();
        }

        AddRotation();
        player.transform.rotation = transform.rotation;
        player.transform.rotation = Quaternion.Euler(0, player.transform.eulerAngles.y, 0);
    }

    public void AddRotation()
    {
        rotationY += Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        rotationX -= Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
        Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0);
        cameraPosition = rotation * cameraOffset;

        transform.rotation = rotation;
        transform.position =  player.transform.position + cameraPosition;
    }

    public void ShakeCamera()
    {

            float tempShakeAmount = physComp.GetVelocity().magnitude * shakeAmount;
        UnityEngine.Camera.main.transform.localPosition += new Vector3(Random.insideUnitSphere.x * tempShakeAmount, Random.insideUnitSphere.y * tempShakeAmount + 1.78f, 0f);
        UnityEngine.Camera.main.transform.localPosition -= new Vector3(Random.insideUnitSphere.x * tempShakeAmount, Random.insideUnitSphere.y * tempShakeAmount + 1.78f, 0f);
            shakeCounter += Time.deltaTime;
            
    }

    public void changeCursorLockMode()
    {
        if (Cursor.lockState == CursorLockMode.Confined)
        {
        Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void InitiateShake()
    {
        if (shakeStart == false)
        {
            Vector3 Velocity = new Vector3(physComp.GetVelocity().x, 0f, physComp.GetVelocity().z);
            ray = Physics.Raycast(player.transform.position, Velocity.normalized, out hit, 3f, layerMask);
            Debug.DrawRay(player.transform.position, Velocity.normalized, Color.yellow, 3f);
        }

        if (ray == true)
        {
            if (hit.collider.gameObject.tag == "Floor" && hit.collider.gameObject != hitGameObject)
            {
                hitGameObject = hit.collider.gameObject;
                shakeStart = true;

            }
        }
        else if (hitGameObject != null)
        {
            hitAgain += Time.deltaTime;
        }
        if (hitAgain > 0.1)
        {

            hitGameObject = null;
            hitAgain = 0;
        }
        if (shakeStart == true)
        {
            ShakeCamera();

        }
        if (shakeCounter > shakeLength)
        {
            shakeStart = false;
            shakeCounter = 0;
        }
    }

    //GETTERS AND SETTERS

    public void setShakeStart(bool set)
    {
        shakeStart = set;
    }
    public bool getShakeStart()
    {
        return shakeStart;
    }
    public void SetYAxisRotation(float rotationY)
    {
        this.rotationY = rotationY;
    }
}

