using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Repulsor/ActiveState")]
public class RepulsorActiveState : RepulsorBaseState
{
    [SerializeField] private float waitBeforeIncreasing;
    [SerializeField] private float increaseSizeOfForceFieldSpeed;
    private Vector3 notOnCourse;
    public override void Enter()
    {

        owner.IncreaseSizeOfForceFieldEachSecond = new Vector3(Mathf.Abs(owner.IncreaseSizeOfForceFieldEachSecond.x), Mathf.Abs(owner.IncreaseSizeOfForceFieldEachSecond.y), Mathf.Abs(owner.IncreaseSizeOfForceFieldEachSecond.z));
        owner.InvokeRepeating("ChangeSizeOfForceField", waitBeforeIncreasing, increaseSizeOfForceFieldSpeed);
    }
    public override void HandleUpdate()
    {
        if (owner.Renderer[1].transform.localScale.x > owner.MaxSizeOfForceField.x)
        {
            owner.CancelInvoke("ChangeSizeOfForceField");

            owner.Transition<RepulsorIdleState>();
        }
    }
}
