using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour

{
    //Attributes
    private Vector3 cameraPosition;
    private Vector3 cameraOffset = Vector3.zero;
    private ControllerInput controllerInput;
    [SerializeField] private GameObject player;

    //Shake effect attributes
    private bool shakeStart;
    private RaycastHit hit;
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
        Cursor.lockState = CursorLockMode.Locked;

        controllerInput = GameObject.FindObjectOfType<ControllerInput>() as ControllerInput;
        physComp = player.GetComponent<PhysicsComponent>();

        cameraPosition = Vector3.zero;
        cameraOffset = transform.position - player.transform.position;

        shakeStart = false;
        shakeCounter = 0;
        ray = false;
        hitAgain = 0;
    }

    void FixedUpdate()
    {
        if (Time.timeScale == 1)
        {
            AddRotation();
        }
        InitiateShake();
    }

    void Update()
    {

     //   changeCursorLockMode();
       

    }

    public void AddRotation()
    {
        float rotationY = controllerInput.GetRotationY();
        float rotationX = controllerInput.GetRotationX();

        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
        Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0);
        cameraPosition = rotation * cameraOffset;

        transform.rotation = rotation;
        transform.position = player.transform.position + cameraPosition;
        player.transform.rotation = transform.rotation;
        player.transform.rotation = Quaternion.Euler(0, player.transform.eulerAngles.y, 0);
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
        if (Cursor.lockState != CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (Cursor.visible)
        {
            Cursor.visible = false;
        }
    }

    public void InitiateShake()
    {
        //checkforhit

        if (shakeStart == false)
        {
            Vector3 Velocity = new Vector3(physComp.GetVelocity().x, 0f, physComp.GetVelocity().z);
            ray = Physics.Raycast(player.transform.position, Velocity.normalized, out hit, 3f, layerMask);
            Debug.DrawRay(player.transform.position, Velocity.normalized, Color.yellow, 3f);
            if (ray == true)
            {
                if (hit.collider.gameObject.tag == "Floor" && hit.collider.gameObject != hitGameObject)
                {

                    hitGameObject = hit.collider.gameObject;
                    shakeStart = true;

                }
            }
        }
        //check so we dont hit the same object we already hit
        else if (hitGameObject != null)
        {
            hitAgain += Time.deltaTime;
        }
        if (hitAgain > 0.05)
        {

            hitGameObject = null;
            hitAgain = 0;
        }
        //Shake Camera
        if (shakeStart == true)
        {
            ShakeCamera();

        }
        //Stop after shakelength
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
}

