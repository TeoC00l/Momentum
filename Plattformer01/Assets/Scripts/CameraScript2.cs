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
    private bool shakeStart = false;
    [SerializeField] private float shakeAmount;
    [SerializeField] private LayerMask layerMask;
    private RaycastHit hit;
    private RaycastHit hit2;
    [SerializeField] private float shakeLength;
    private float shakeCounter = 0;
    private bool ray = false;
    private GameObject hitGameObject = null;
    private float hitAgain = 0;
    private PhysicsComponent physComp;
    private List<MeshRenderer> meshArray = new List<MeshRenderer>();
    private List<MeshRenderer> newMeshArray = new List<MeshRenderer>();
    private MeshRenderer specifikMeshRender;

    //Methods
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        cameraOffset = transform.position - player.transform.position;
        physComp = player.GetComponent<PhysicsComponent>();
    }
    void FixedUpdate()
    {
        player.transform.rotation = transform.rotation;
        player.transform.rotation = Quaternion.Euler(0, player.transform.eulerAngles.y, 0);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            changeCursorLockMode();
        }

        AddRotation();
        if (shakeStart == false)
        {
            Vector3 Velocity = new Vector3(physComp.GetVelocity().x, 0f, physComp.GetVelocity().z);
            ray = Physics.Raycast(player.transform.position, Velocity.normalized, out hit, 3f, layerMask);
            Debug.DrawRay(player.transform.position,Velocity.normalized, Color.yellow, 3f);

        }
        if (ray == true)
        {
            if (hit.collider.gameObject.tag == "Floor" && hit.collider.gameObject != hitGameObject )
            {
                hitGameObject = hit.collider.gameObject;
                shakeStart = true;
                
            }
        }else if (hitGameObject != null)
        {
            hitAgain += Time.deltaTime;
        }
        if(hitAgain > 0.1)
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

    public void AddRotation()
    {
        rotationY += Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        rotationX -= Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
        Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0);
        cameraPosition = rotation * cameraOffset;

        //   transform.position = player.transform.position + cameraPosition;
        //   transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5);

        transform.rotation = rotation;

        transform.position =  player.transform.position + cameraPosition;
     //   player.transform.rotation = transform.rotation;
     //   player.transform.rotation = Quaternion.Euler(0, player.transform.eulerAngles.y, 0);



        //bool PlayerToCam = Physics.Linecast(player.transform.position, this.gameObject.transform.position,out hit2, layerMask, 0);
        //Debug.DrawLine(player.transform.position, this.gameObject.transform.position, Color.red, 1);
        //if (PlayerToCam)
        //{

        //    specifikMeshRender = hit2.collider.gameObject.GetComponent<MeshRenderer>();

        //    if (specifikMeshRender != null)
        //    {
        //        Debug.Log(specifikMeshRender);
        //        meshArray.Add(specifikMeshRender);
        //        specifikMeshRender.enabled = false;
        //    }
        //}
        //foreach (MeshRenderer R in meshArray)
        //{
        //    if(specifikMeshRender == R)
        //    {
        //        newMeshArray.Add(R);

        //    }
        //    else
        //    {
        //        Debug.Log("ReENABLED NOOOOW");
        //        R.enabled = true;

        //    }
        //}
        //meshArray = newMeshArray;
        //ray = Physics.Linecast(player.transform.position, Camera.main.transform.position, layerMask,0);
        //if(ray == true)
        //{
        //    Debug.Log("hitcamera");
        //    Camera.main.transform.position = hit.point;
        //}

    }
    public void ShakeCamera()
    {

            float tempShakeAmount = physComp.GetVelocity().magnitude * shakeAmount;
            Camera.main.transform.localPosition += new Vector3(Random.insideUnitSphere.x * tempShakeAmount, Random.insideUnitSphere.y * tempShakeAmount + 1.78f, 0f);
            Camera.main.transform.localPosition -= new Vector3(Random.insideUnitSphere.x * tempShakeAmount, Random.insideUnitSphere.y * tempShakeAmount + 1.78f, 0f);
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

