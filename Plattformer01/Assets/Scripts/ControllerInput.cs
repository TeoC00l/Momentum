using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControllerInput : MonoBehaviour
{
    //Attributes
    [SerializeField] private float xboxInputSensitivity;
    [SerializeField] private float mouseInputSensitivity;

    private float rotationY;
    private float rotationX;

    private float verticalInput;
    private float horizontalInput;
    private bool isJumping;
    private bool isDashingLeft;
    private bool isDashingRight;
    private bool isPrecisionModeActive;
    private bool isKineticBatteryActive;

    private bool isXboxController;
    private bool isKineticBatteryAcquired;
    private float dashLeftOrRight;

    void Awake()
    {
        isXboxController = false;

        if (SceneManager.GetActiveScene().name == "Level 2")
        {
          isKineticBatteryAcquired = true;
        }
        else
        {
            isKineticBatteryAcquired = false;
        }
        isDashingRight = false;
        isDashingLeft = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            changeController();
        }

        if (isXboxController == true)
        {
            HandleXboxInput();
        }
        else
        {
            HandleMouseKeyboardInput();
        }      
    }

    public void HandleXboxInput()
    {
        rotationY += Input.GetAxisRaw("Joystick X") * xboxInputSensitivity;
        rotationY += Input.GetAxisRaw("Horizontal") * xboxInputSensitivity;
        rotationX -= Input.GetAxisRaw("Joystick Y") * xboxInputSensitivity;

        verticalInput = Input.GetAxisRaw("Vertical");
        horizontalInput = Input.GetAxisRaw("Horizontal");

        isJumping = Input.GetKeyDown("joystick button 0");
        isDashingLeft = Input.GetKeyDown("joystick button 4");
        isDashingRight = Input.GetKeyDown("joystick button 5");
        if (dashLeftOrRight == 0)
        {
            dashset();
        }
        isKineticBatteryActive = isKineticBatteryAcquired && (Input.GetAxisRaw("Joystick R trigger") > 0);
        isPrecisionModeActive = Input.GetAxisRaw("Joystick L trigger") > 0;
    }

    public void HandleMouseKeyboardInput()
    {
        rotationY += Input.GetAxisRaw("Mouse X") * mouseInputSensitivity;
        rotationX -= Input.GetAxisRaw("Mouse Y") * mouseInputSensitivity;

        verticalInput = Input.GetAxisRaw("Vertical");
        horizontalInput = Input.GetAxisRaw("Horizontal");

        isJumping = Input.GetKeyDown(KeyCode.Space);
        isDashingLeft = Input.GetKey(KeyCode.Q);
        isDashingRight = Input.GetKey(KeyCode.E);
        if(dashLeftOrRight == 0)
        {
            dashset();
        }
        isKineticBatteryActive = isKineticBatteryAcquired && Input.GetMouseButton(0);
        isPrecisionModeActive = Input.GetKeyDown(KeyCode.LeftShift);
    }

    public void changeController()
    {
        if (isXboxController == true)
        {
            isXboxController = false;
        }
        else
        {
            isXboxController = true;
        }
    }

    //GETTERS/SETTERS
    public float GetRotationY() { return rotationY; }
    public float GetRotationX() { return rotationX; }
    public float GetVerticalInput() { return verticalInput; }
    public float GetHorizontalInput() { return horizontalInput; }
    public bool GetIsJumping() { return isJumping; }
    public bool GetIsDashingLeft() { return isDashingLeft; }
    public bool GetIsDashingRight() { return isDashingRight; }
    public bool GetIsPrecisionModeActive() { return isPrecisionModeActive; }
    public bool GetIsKineticBatteryActive() { return isKineticBatteryActive; }
    public bool GetIsXboxController() { return isXboxController; }
    public bool GetIsKineticBatteryAcquired() { return isKineticBatteryAcquired; }

    public void SetIsKineticBatteryAcquired(bool isKineticBatteryAcquired) { this.isKineticBatteryAcquired = isKineticBatteryAcquired; }
    public void SetIsXboxController (bool isXboxController) { this.isXboxController = isXboxController; }
    public void SetRotationY(float rotationY) { this.rotationY = rotationY; }
    public float GetDashingLeftOrRight() { return dashLeftOrRight; }

    public void dashset()
    {
        if (isDashingLeft)
        {
            dashLeftOrRight = -1;
        }
        else if (isDashingRight)
        {
            dashLeftOrRight = 1;

        }
    }
    public void resetDash()
    {
        dashLeftOrRight = 0;
    }
}
