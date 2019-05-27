using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardSaver : MonoBehaviour
{
    void Awake()
    {
       DontDestroyOnLoad(this.gameObject); // and make this object persistant as we load new scenes
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
