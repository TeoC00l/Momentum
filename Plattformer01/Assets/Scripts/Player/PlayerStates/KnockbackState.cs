using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Player/KnockbackState")]
public class KnockbackState : PlayerBaseState
{

    private Rigidbody rigid;
    [SerializeField] private float knockback;
    [SerializeField] private float angle;
    private float timer = 0;

    void Awake()
    {
     //  rigid = owner.gameObject.GetComponent<Rigidbody>();
       
    }
   
    // Update is called once per frame
    public override void HandleUpdate()
    {
        owner.rigid.isKinematic = false;
        owner.rigid.useGravity = true;
        Vector3 dir = (-owner.transform.forward);
        dir.y += 0.8f;
        owner.rigid.AddForce(dir * knockback);
        timer += Time.deltaTime;
        if(timer > 0.3)
        {
            owner.rigid.isKinematic = true;
            owner.rigid.useGravity = false;
            timer = 0;
            owner.Transition<PrecisionState>();
            
        }
        
    }
}
