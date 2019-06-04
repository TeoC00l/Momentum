using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Calculations
{
    public static Vector3 CalculateAcceleration(Vector3 velocity, Vector3 direction, float acceleration)
    {
        velocity += (direction * acceleration * Time.deltaTime);

        return velocity;
    }

    public static Vector3 CalculateJump(Vector3 velocity, float jumpMagnitude)
    {
        velocity += (Vector3.up * jumpMagnitude * Time.deltaTime);

        return velocity;
    }

    public static Vector3 CalculateFriction(Vector3 velocity, Vector3 normalForce, float staticFrictionCo)
    {
        float dynamicFrictionCo = staticFrictionCo *0.65f;
        float staticFriction = normalForce.magnitude * staticFrictionCo;
        float frictionMagnitude = normalForce.magnitude * dynamicFrictionCo;

        if (velocity.magnitude < staticFriction)
        {
            velocity = Vector3.zero;
            return velocity;
        }
        else
        {
                velocity = velocity + -velocity.normalized * frictionMagnitude;
        }
        return velocity;
    }


    public static Vector3 CalculateNormalForce(Vector3 velocity, RaycastHit hit)
    {
        float dot = Vector3.Dot(velocity, hit.normal);

        if (dot > 0)
        {
            dot = 0;
        }

        Vector3 projection = dot * hit.normal;

        return -projection;
    }

    public static Vector3 CalculateGravity(float gravitationalForce)
    {
        Vector3 direction = Vector3.down;

        Vector3 gravity = direction * gravitationalForce * Time.deltaTime;

        return gravity;
    }

    public static Vector3 AddAirResistance(Vector3 velocity, float airResistance)
    {
        velocity *= Mathf.Pow(airResistance, Time.deltaTime);
        return velocity;
    }

}
