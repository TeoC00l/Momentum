using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemSpeedPickUP : MonoBehaviour
{
    private Text pickUpText;
    private float pickUpAmount = 0;
    private PhysicsComponent physComp;
    [Range(0.001f,1f)]
    [SerializeField]
    private float addThisMuchSpeedInProcentForEachGem;
    void Awake()
    {
        pickUpText = transform.GetChild(2).GetComponent<Text>();
        physComp = GameObject.FindWithTag("Player").GetComponent<PhysicsComponent>();
    }
    //Update Gem Text
    public void UpdateGems()
    {
        pickUpAmount += addThisMuchSpeedInProcentForEachGem;
        pickUpText.text = "+" + pickUpAmount + "%";
        Debug.Log(pickUpAmount);
        physComp.AddToSpeedIncrease(addThisMuchSpeedInProcentForEachGem / 100);
    }
    public void SetPickUpAmount(float set)
    {
        this.pickUpAmount = set;
    }
    public float GetPickUpAmount()
    {
        return pickUpAmount;
    }
}
