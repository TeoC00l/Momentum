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
    // Start is called before the first frame update
    void Awake()
    {
        pickUpText = transform.GetChild(2).GetComponent<Text>();
        physComp = GameObject.FindWithTag("Player").GetComponent<PhysicsComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateGems()
    {
        pickUpAmount += addThisMuchSpeedInProcentForEachGem;
        pickUpText.text = "+" + pickUpAmount + "%";
        physComp.AddToSpeedIncrease(addThisMuchSpeedInProcentForEachGem / 100);
    }
}
