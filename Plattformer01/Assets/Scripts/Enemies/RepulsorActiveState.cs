using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Repulsor/ActiveState")]
public class RepulsorActiveState : RepulsorBaseState
{
    public override void Enter()
    {
        Debug.Log("IncreaseField");
        owner.IncreaseSizeOfForceFieldEachSecond = new Vector3(Mathf.Abs(owner.IncreaseSizeOfForceFieldEachSecond.x), Mathf.Abs(owner.IncreaseSizeOfForceFieldEachSecond.y), Mathf.Abs(owner.IncreaseSizeOfForceFieldEachSecond.z));

        owner.InvokeRepeating("ChangeSizeOfForceField", 0f, 0.05f);
    }
    public override void HandleUpdate()
    {
        Debug.Log("Increasing" + owner.Renderer[1].transform.localScale + "other" + owner.MaxSizeOfForceField);
        if (Vector3.Distance(owner.Renderer[1].transform.localScale, owner.MaxSizeOfForceField) < 0.3f)
        {
            owner.CancelInvoke("ChangeSizeOfForceField");
            Debug.Log("StopForceField");
            owner.Transition<RepulsorIdleState>();
        }
    }
}
