using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsComponent : MonoBehaviour

{
    //Attributes
    public Vector3 velocity = Vector3.zero;
    public Vector3 direction = Vector3.zero;
    public RayCasterCapsule rayCaster;

    public float skinWidth;
    public float acceleration;
    public float gravitationalForce;
    public float JumpMagnitude;
    public float staticFrictionCo;
    public float airResistance;

    //Methods
    void Awake()
    {
        rayCaster = GetComponent<RayCasterCapsule>();

        //skinWidth = 0.5f;
        //acceleration = 1f;
        //gravitationalForce = 1.0f;
        //staticFrictionCo = 0.7f;
        //JumpMagnitude = 20.0f;
        //airResistance = 0.7f;
    }

    public void AddForces()
    {
        //Calculate velocity, add gravity
        Vector3 gravity = Calculations2.CalculateGravity(gravitationalForce);
        velocity = Calculations2.CalculateAcceleration(velocity, direction, acceleration);
        velocity += gravity;

        //Add Air resistance
        velocity = Calculations2.AddAirResistance(velocity, airResistance);
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
            //Add one cycle
            noOfCycles++;

            //Make adjustments to normalforce
            Vector3 normalForce = MeasureNormalForce();
            velocity += normalForce;
            totalNormalForce += normalForce;

            //Look for new collisions
            hit = rayCaster.GetCollisionData(velocity, 0);
        }
        while (hit.collider != null && noOfCycles < 1000);

        velocity = Calculations2.CalculateFriction(velocity, totalNormalForce, staticFrictionCo);

        if (hit.collider == null)
        {
            transform.position += velocity;
        } 
    }

    public bool GroundCheck()
    {
        RaycastHit hit = rayCaster.GetCollisionData(Vector3.down, skinWidth*2);
        if (hit.collider != null)
        {
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
            velocity = Calculations2.CalculateJump(velocity, JumpMagnitude);
        }
    }




    //GETTERS AND SETTERS


    //velocity
    public Vector3 GetVelocity()
    {
        return this.velocity;
    }

    public void SetVelocity(Vector3 velocity)
    {
        this.velocity = velocity;
    }

    public void SetVelocityMagnitude(float magnitude)
    {
        velocity = velocity.normalized * magnitude;
    }


    //direction
    public void SetDirection(Vector3 direction)
    {
        this.direction = direction;
    }

    public Vector3 GetDirection(Vector3 direction)
    {
       return direction;
    }


    //gravitationalForce
    public float GetGravitationalForce(float gravitationalForce)
    {
        return this.gravitationalForce;
    }

    public void SetGravitationalForce(float gravitationalForce)
    {
        this.gravitationalForce = gravitationalForce;
    }
}


