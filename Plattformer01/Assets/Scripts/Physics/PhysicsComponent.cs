//Author: Teodor Tysklind
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsComponent : MonoBehaviour
{
//ATTRIBUTES
    private const int NUMBER_OF_NORMALFORCE_CALCULATIONS_LIMIT = 10000;

    private Vector3 velocity;
    private Vector3 direction;
    private RayCasterCapsule rayCaster;

    private float accelerationSpeed;
    private float gravitationalForce;
    private float jumpMagnitude;
    private float staticFrictionMultiplier;
    private float airResistance;
    private float accelerationMultiplier;
    [SerializeField] private float skinWidth;
    [SerializeField] private float groundCheckDistance;

//METHODS 
    void Awake()
    {
        velocity = Vector3.zero;
        direction = Vector3.zero;
        rayCaster = GetComponent<RayCasterCapsule>();
        accelerationMultiplier = 1;
    }

    public void AddForces()
    {      
        velocity = PhysicsCalculations.CalculateAccelerationMagnitude(velocity, direction, accelerationSpeed);
        AddGravity();
        velocity = PhysicsCalculations.AddAirResistance(velocity, airResistance);
        if (AddNormalForces())
        {
            MovePosition();
        }
    }

    public void AddGravity()
    {
        Vector3 gravity = PhysicsCalculations.CalculateGravity(gravitationalForce);
        velocity += gravity;
    }

    private bool AddNormalForces()
    {
        Vector3 totalNormalForce = Vector3.zero;
        int numberOfCalculationsDone = 0;
        RaycastHit hit;

        //Modify velocity until no more collisions occur, or until object is deemed stuck
        do
        {
            numberOfCalculationsDone++;

            Vector3 normalForce = MeasureNormalForce();
            velocity += normalForce;
            totalNormalForce += normalForce;

            hit = rayCaster.GetCollisionData(velocity, 0);
        }
        while (hit.collider != null && numberOfCalculationsDone < NUMBER_OF_NORMALFORCE_CALCULATIONS_LIMIT);

        AddFriction(totalNormalForce);

        //Return true if not stuck
        if (hit.collider == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void MovePosition()
    {
        transform.position += (velocity * accelerationMultiplier) * Time.deltaTime;
    }

    private void AddFriction(Vector3 normalForce)
    {
        velocity = PhysicsCalculations.CalculateFriction(velocity, normalForce, staticFrictionMultiplier);
    }

    private Vector3 MeasureNormalForce()
    {
        Vector3 normalForce = Vector3.zero;
        RaycastHit hit = rayCaster.GetCollisionData(velocity, skinWidth);
        normalForce = PhysicsCalculations.CalculateNormalForce(velocity, hit);

        return normalForce;
    }

    public bool GroundCheck()
    {
        RaycastHit hit = rayCaster.CheckForGround(groundCheckDistance + skinWidth);

        if (hit.collider != null)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.Cross(transform.right, hit.normal), hit.normal), Time.deltaTime * 50f);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Jump()
    {
        velocity = PhysicsCalculations.CalculateJump(velocity, jumpMagnitude);
    }

//GETTERS AND SETTERS
    public void SetAcceleration(float acceleration)
    {
        this.accelerationSpeed = acceleration;
    }
    public Vector3 GetVelocity()
    {
        return this.velocity;
    }
    public void SetVelocity(Vector3 velocity)
    {
        this.velocity = velocity;
    }
    public void AddVelocity(Vector3 addVelocity)
    {
        velocity += addVelocity;
    }
    public void SubtractVelocity(Vector3 subtractVelocity)
    {
        velocity -= subtractVelocity;
    }
    public void SetDirection(Vector3 direction)
    {
        this.direction = direction;
    }
    public Vector3 GetDirection()
    {
        return direction;
    }
    public void SetGravitationalForce(float gravitationalForce)
    {
        this.gravitationalForce = gravitationalForce;
    }
    public void SetJumpMagnitude(float jumpMagnitude)
    {
        this.jumpMagnitude = jumpMagnitude;
    }
    public void SetStaticFrictionCo(float staticFrictionCo)
    {
        this.staticFrictionMultiplier = staticFrictionCo;
    }
    public void SetAirResistance(float airResistance)
    {
        this.airResistance = airResistance;
    }
    public float GetSkinWidth()
    {
        return skinWidth;
    }
    public void AddToDirection(Vector3 add)
    {
        direction += add;
    }
    public void AddToSpeedIncrease(float add)
    {
        accelerationMultiplier += add;
    }
    public void SetSpeedIncrease(float set)
    {
        accelerationMultiplier = set;
    }
    public float GetSpeedIncrease()
    {
        return accelerationMultiplier;
    }
}


