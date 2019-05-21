using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIKineticArrow : MonoBehaviour
{
    private GameObject player; //player
   
    private Player playerScript;

 
    void Awake()
    {
        player = GameObject.FindWithTag("Player");  //all players should be called Character
        playerScript = player.GetComponent<Player>();
        //set to 0 at start 
    }

    // Update is called once per frame
    void Update()
    {
        if(playerScript.GetKineticActive() == true)
        {
            Debug.Log("true");
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);

        }
    }
}
