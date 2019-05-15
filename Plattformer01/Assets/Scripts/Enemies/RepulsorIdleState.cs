using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Repulsor/IdleState")]
public class RepulsorIdleState : RepulsorBaseState
{
    [SerializeField]private float waitBeforeDescreasing;
    [SerializeField] private float decreaseSizeOfForceFieldSpeed;
    public override void Enter()
    {

        owner.IncreaseSizeOfForceFieldEachSecond = new Vector3(-owner.IncreaseSizeOfForceFieldEachSecond.x, -owner.IncreaseSizeOfForceFieldEachSecond.y, -owner.IncreaseSizeOfForceFieldEachSecond.z);
        owner.InvokeRepeating("ChangeSizeOfForceField", waitBeforeDescreasing, decreaseSizeOfForceFieldSpeed);
    }
    public override void HandleUpdate()
    {

        if (Vector3.Distance(owner.Renderer[1].transform.localScale, owner.MinSizeOfForceField) < 0.2f)
        {
            owner.CancelInvoke("ChangeSizeOfForceField");

            owner.Transition<RepulsorActiveState>();
        }
    }
}
