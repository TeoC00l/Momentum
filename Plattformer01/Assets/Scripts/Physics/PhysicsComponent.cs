using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsComponent : MonoBehaviour

{
    //Attributes
    private Vector3 velocity = Vector3.zero;
    private Vector3 direction = Vector3.zero;
    private RayCasterCapsule rayCaster;

    [SerializeField] private float skinWidth;
    [SerializeField] private float groundCheckDistance;

    private float acceleration;
    private float gravitationalForce;
    private float jumpMagnitude;
    private float staticFrictionCo;
    private float airResistance;

    //Methods
    void Awake()
    {
        rayCaster = GetComponent<RayCasterCapsule>();
    }

    public void AddForces()
    {
       
        velocity = Calculations2.CalculateAcceleration(velocity, direction, acceleration);
        AddGravity();
        velocity = Calculations2.AddAirResistance(velocity, airResistance);

    }
    public void AddGravity()
    {
        Vector3 gravity = Calculations2.CalculateGravity(gravitationalForce);
        velocity += gravity;

    }

    public Vector3 MeasureNormalForce()
    {
        Vector3 normalForce = Vector3.zero;
        RaycastHit hit = rayCaster.GetCollisionData(velocity, 0);
        normalForce = Calculations2.CalculateNormalForce(velocity, hit);
        return normalForce;

    }

    public void CollisionCalibration()
    {
        Vector3 totalNormalForce = Vector3.zero;
        int noOfCycles = 0;
        RaycastHit hit = rayCaster.GetCollisionData(velocity, 0);

        do
        {
            noOfCycles++;

            Vector3 normalForce = MeasureNormalForce();
            velocity += normalForce;
            totalNormalForce += normalForce;

            hit = rayCaster.GetCollisionData(velocity, 0);
        }
        while (hit.collider != null && noOfCycles < 10000);

        velocity = Calculations2.CalculateFriction(velocity, totalNormalForce, staticFrictionCo);

        if (hit.collider == null)
        {
            transform.position += velocity * Time.deltaTime;
        }
    }

    public bool GroundCheck()
    {
        RaycastHit hit = rayCaster.GetCollisionData(Vector3.down * groundCheckDistance, skinWidth);
        if (hit.collider != null)
        {
          //  transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.Cross(transform.right, hit.normal), hit.normal), Time.smoothDeltaTime * 500f);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Jump()
    {
        {
            velocity = Calculations2.CalculateJump(velocity, jumpMagnitude);
        }
    }








    //GETTERS AND SETTERS
    public void SetAcceleration(float acceleration)
    {
        this.acceleration = acceleration;
    }

    public float GetAcceleration()
    {
        return acceleration;
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

    public void SetVelocityMagnitude(float magnitude)
    {
        velocity = velocity.normalized * magnitude;
    }

    public void SetDirection(Vector3 direction)
    {
        this.direction = direction;
    }

    public Vector3 GetDirection()
    {
        return direction;
    }

    public float GetGravitationalForce(float gravitationalForce)
    {
        return this.gravitationalForce;
    }

    public void SetGravitationalForce(float gravitationalForce)
    {
        this.gravitationalForce = gravitationalForce;
    }

    public void SetJumpMagnitude(float jumpMagnitude)
    {
        this.jumpMagnitude = jumpMagnitude;
    }

    public float GetJumpMagnitude()
    {
        return jumpMagnitude;
    }
    
    public void SetStaticFrictionCo(float staticFrictionCo)
    {
        this.staticFrictionCo = staticFrictionCo;
    }

    public float GetStaticFrictionCo()
    {
        return staticFrictionCo;
    }

    public void SetAirResistance(float airResistance)
    {
        this.airResistance = airResistance;
    }

    public float GetAirResistance()
    {
        return airResistance;
    }

    public float GetSkinWidth()
    {
        return skinWidth;
    }
    public void AddToVelocity(Vector3 add)
    {
        velocity += add;
    }
    public void AddToDirection(Vector3 add)
    {
        direction += add;
    }

}


