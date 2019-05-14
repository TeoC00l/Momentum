using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Repulsor/IdleState")]
public class RepulsorIdleState : RepulsorBaseState
{

    public override void Enter()
    {
        Debug.Log("DecreaseField");
        owner.IncreaseSizeOfForceFieldEachSecond = new Vector3(-owner.IncreaseSizeOfForceFieldEachSecond.x, -owner.IncreaseSizeOfForceFieldEachSecond.y, -owner.IncreaseSizeOfForceFieldEachSecond.z);
        owner.InvokeRepeating("ChangeSizeOfForceField", 0f, 0.05f);
    }
    public override void HandleUpdate()
    {

        if (Vector3.Distance(owner.Renderer[1].transform.localScale, owner.MinSizeOfForceField) < 0.2f)
        {
            owner.CancelInvoke("ChangeSizeOfForceField");
            Debug.Log("StopForceField");
            owner.Transition<RepulsorActiveState>();
        }
    }
}
