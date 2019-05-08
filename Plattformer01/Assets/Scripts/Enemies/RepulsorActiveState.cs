using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Repulsor/ActiveState")]
public class RepulsorActiveState : RepulsorBaseState
{
    public override void HandleUpdate()
    {
        Debug.Log("Ya bish");
    }
}
