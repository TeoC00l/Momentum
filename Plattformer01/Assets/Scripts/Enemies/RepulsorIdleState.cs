using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Repulsor/IdleState")]
public class RepulsorIdleState : RepulsorBaseState
{
    [SerializeField]private float waitBeforeDescreasing;
    [SerializeField] private float decreaseSizeOfForceFieldSpeed;
    Vector3 notOnCourse;
    public override void Enter()
    {

        owner.IncreaseSizeOfForceFieldEachSecond = new Vector3(-owner.IncreaseSizeOfForceFieldEachSecond.x, -owner.IncreaseSizeOfForceFieldEachSecond.y, -owner.IncreaseSizeOfForceFieldEachSecond.z);
        owner.InvokeRepeating("ChangeSizeOfForceField", waitBeforeDescreasing, decreaseSizeOfForceFieldSpeed);
    }
   
    public override void HandleUpdate()
    {


        if (owner.Renderer[1].transform.localScale.y < owner.MinSizeOfForceField.y)
        {
            owner.GetComponentInChildren<SphereCollider>().enabled = false;
            owner.CancelInvoke("ChangeSizeOfForceField");

            owner.Transition<RepulsorActiveState>();
        }
    }
}
