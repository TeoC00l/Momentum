using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    [Header("Basic Audio")]
    private AudioSource[] sources;
    private AudioSource basicsource;
    private AudioSource barksource;

    [Header("Walk Speeds grass")]
    public AudioClip[] walk_slow_grass;
    public AudioClip[] walk_medium_grass;
    public AudioClip[] walk_fast_grass;

    [Header("Walk Speeds wood")]
    public AudioClip[] walk_slow_wood;
    public AudioClip[] walk_medium_wood;
    public AudioClip[] walk_fast_wood;

    [Header("Wading Through Water")]
    public AudioClip walk_in_water;

    private float playerVelocity;
    private float playerVerticalVelocity; //used later to fix jumping + footstep issues.
    private float stepspeed = 0.03f;
    private AudioClip walkChoice;
    private string groundType= "Wood";
    private RaycastHit checkMaterial;
    private AudioClip noSound; //a fast way to mute footsteps.

    [Header("Barking")]
    private AudioSource jumpSounds;
    public AudioClip[] jump_vars;
    private float timeSounded;

    private int count;

    // Start is called before the first frame update
    void Start()
    {
        sources = GetComponents<AudioSource>();
        basicsource = sources[0];

        barksource = sources[1];
        //for(int i = 0; i < jump_variations.Length; i++)
        //{
        //    jumpSounds.clip = jump_variations[i];
        //}
    }



    // Update is called once per frame
    void Update()
    {
        playFootSteps();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            groundType = "Air";
            bark();
        }
    }

    void bark()
    {
        if(Time.time-timeSounded<0.25f) { return;  }
        int n = Random.Range(1, jump_vars.Length);
        AudioClip clip = jump_vars[n];
        barksource.PlayOneShot(clip);
        jump_vars[n] = jump_vars[0];
        jump_vars[0] = clip;
        timeSounded = Time.time;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wooden"))
        {
            groundType = "Wood";
        }
        if (other.gameObject.CompareTag("Mud"))
        {
            groundType = "Grass";
        }
        if (other.gameObject.CompareTag("Water"))
        {
            groundType = "Water";
        }
        

    }

    void FixedUpdate()
    {
        playerVelocity = GetComponent<Rigidbody>().velocity.magnitude;
    }

    void playFootSteps()
    {
        basicsource.volume = playerVelocity / 10;
        if (!basicsource.isPlaying)
        {
            StaticFootStepsAudio();
        }
    }

    //this class determines whether player is walking fast or slow.
    void StaticFootStepsAudio()
    {
        if (playerVelocity >= 1f && playerVelocity < 5.9f)
        {
            StaticWalkSlow();
        }
        else if (playerVelocity >= 5.9f)
        {
            StaticWalkMedium();
        }
        else if (playerVelocity >= 10f)
        {
            StaticWalkFast();
        }
    }

    //autochecks what ground type is.
    void StaticWalkSlow()
    {
        if (groundType == "Grass")
        {
            
            basicsource.PlayOneShot(walk_slow_grass[Random.Range(0,walk_slow_grass.Length)]);
        } else if (groundType == "Wood")
        {
            basicsource.PlayOneShot(walk_slow_wood[Random.Range(0, walk_slow_wood.Length)]);
        } else if (groundType == "Water")
        {
            basicsource.PlayOneShot(walk_in_water);
        } else if (groundType == "Air")
        {
            basicsource.PlayOneShot(noSound);
        }
    }

    void StaticWalkMedium()
    {
        if (groundType == "Grass")
        {
            basicsource.PlayOneShot(walk_medium_grass[Random.Range(0, walk_medium_grass.Length)]);
        } else if (groundType == "Wood")
        {
            basicsource.PlayOneShot(walk_medium_wood[Random.Range(0, walk_medium_wood.Length)]);
        }
        else if (groundType == "Water")
        {
            basicsource.PlayOneShot(walk_in_water);
        }
        else if (groundType == "Air")
        {
            basicsource.PlayOneShot(noSound);
        }
    }
    void StaticWalkFast()
    {
        if (groundType == "Grass")
        {
            basicsource.PlayOneShot(walk_fast_grass[Random.Range(0, walk_slow_grass.Length)]);
        } else if (groundType == "Wood")
        {
            basicsource.PlayOneShot(walk_fast_wood[Random.Range(0, walk_slow_grass.Length)]);
        }
        else if (groundType == "Water")
        {
            basicsource.PlayOneShot(walk_in_water);
        }
         else if (groundType == "Air")
        {
            basicsource.PlayOneShot(noSound);
        }
    }
}
