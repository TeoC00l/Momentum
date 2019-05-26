using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//depreciated
public class Music_Handler : MonoBehaviour
{
    [Header("Ambience")]
    private AudioSource[] source;
    private AudioSource primary;
    private AudioSource secondary;

    public AudioClip music1;
    public AudioClip music2;
    [Header("Set Vol threshold for music")]
    public float vol1 = 0.31f; //max
    public float vol2 = 0; //min
    [Header("fadeout when walk out of area")]
    public float minDelay = 0.5f;
    public float maxDelay = 3.0f;
    private bool areaExited = false;
    private bool areaEntered = false;
    [Header("How fast you want the sound to fade in/out")]
    public float fadespeed = 3;

    private int fadingType = 0;
    private bool playtoggle = false; //what track is playing?


    // Start is called before the first frame update
    void Start()
    {
        source = GetComponents<AudioSource>();
        if (!source[0].isPlaying || source[1].isPlaying) //flag
        {
            primary = source[0];
            primary.volume = 0;
            primary.PlayOneShot(music1);
            source[0].clip = music1;
            source[0].PlayOneShot(music1);
            source[0].volume = vol1;
            source[1].clip = music2;
            source[1].PlayOneShot(music2);
            source[1].volume = vol2;

            playtoggle = false;


        }
    }

    //Due to the unique shape of the shorelines and areas. I decided to make this code instead of focusing on SpotFX's. 
    //Those can be used by the torches. with extension to using this. You get the versatility of marking unique areas.

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exited");
        Debug.Log(primary.volume + " " + areaExited);
        areaExited = true;
        fadingType = 2;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("AreaEntered");
        areaEntered = true;
        fadingType = 1;
    }

    void Update()
    {
        
            if (Input.GetKeyDown(KeyCode.B))
            {
                if (!source[0].isPlaying)
                {
                    if (!playtoggle)
                    {
                        source[0].volume = vol1;
                        source[1].volume = vol2;
                        playtoggle = true;
                    }
                }
                if (!source[0].isPlaying)
                {
                    if (playtoggle)
                    {
                        source[0].volume = vol2;
                        source[1].volume = vol1;
                        playtoggle = false;
                    }
                }
                
            }
        
            
        
        if (areaExited || areaEntered)
        {
            checkFadeType();
        }



    }

    void checkFadeType()
    {
        if (fadingType == 1)
        {
            FadeIn();

        }
        if (fadingType == 2)
        {
            FadeOut();
        }
    }

    void FadeIn()
    {
        if (primary.volume < vol1)
        {
            primary.volume = primary.volume + (Time.deltaTime / fadespeed);
        }
        else if (primary.volume >= vol1)
        {
            fadingType = 0;
        }
        if (secondary.volume < vol2)
        {
            secondary.volume = secondary.volume + (Time.deltaTime / fadespeed);
        }
    }

    void FadeOut()
    {
        if (primary.volume < 1)
        {
            primary.volume = primary.volume - (Time.deltaTime / (fadespeed + 1));
        }
        else if (primary.volume >= vol1)
        {
            fadingType = 0;
        }
        if (secondary.volume < 1)
        {
            secondary.volume = secondary.volume - (Time.deltaTime / (fadespeed + 1));
        }
    }
}