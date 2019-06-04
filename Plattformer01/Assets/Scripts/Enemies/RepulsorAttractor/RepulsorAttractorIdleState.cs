using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "RepulsorAttractor/IdleState")]
public class RepulsorAttractorIdleState : RepulsorBaseState
{
    [SerializeField] private float waitBeforeDescreasing = 0.2f;
    [SerializeField] private float decreaseSizeOfForceFieldSpeed = 0.1f;
    public override void Enter()
    {

        owner.SetIncreaseSizeOfForceFIeldEachSecond(new Vector3(-Mathf.Abs(owner.GetIncreaseSizeOfForceFieldEachSecond().x), -Mathf.Abs(owner.GetIncreaseSizeOfForceFieldEachSecond().y), -Mathf.Abs(owner.GetIncreaseSizeOfForceFieldEachSecond().z)));
        owner.InvokeRepeating("ChangeSizeOfForceField", waitBeforeDescreasing, decreaseSizeOfForceFieldSpeed);
    }

    //Decrease Size Of ForceFields
    public override void HandleUpdate()
    {
        if (owner.GetMeshRenderers()[1].transform.localScale.y < owner.GetMinSizeOfForceField().y)
        {
            owner.GetComponentInChildren<SphereCollider>().enabled = false;
            owner.CancelInvoke("ChangeSizeOfForceField");

            owner.Transition<RepulsorAttractorActiveState>();
        }
    }
}
