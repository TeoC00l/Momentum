using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Repulsor/ActiveState")]
public class RepulsorAttractorActiveState : RepulsorBaseState
{
    //Attributes
    [SerializeField] private float waitBeforeIncreasing;
    [SerializeField] private float increaseSizeOfForceFieldSpeed;

    //Methods
    public override void Enter()
    {
        owner.SetIncreaseSizeOfForceFIeldEachSecond(new Vector3(Mathf.Abs(owner.GetIncreaseSizeOfForceFieldEachSecond().x), Mathf.Abs(owner.GetIncreaseSizeOfForceFieldEachSecond().y), Mathf.Abs(owner.GetIncreaseSizeOfForceFieldEachSecond().z)));
        owner.InvokeRepeating("ChangeSizeOfForceField", waitBeforeIncreasing, increaseSizeOfForceFieldSpeed);
    }

    public override void HandleUpdate()
    {
        if (owner.GetMeshRenderers()[1].transform.localScale.x > owner.GetMaxSizeOfForceField().x)
        {
            owner.CancelInvoke("ChangeSizeOfForceField");
            owner.Transition<RepulsorAttractorIdleState>();
        }
    }
}
