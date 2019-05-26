using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ambience : MonoBehaviour
{
    [Header("Ambience")]
    private AudioSource[] source;
    private AudioSource primary;
    private AudioSource secondary;

    public AudioClip areaSound_var1;
    public AudioClip areaSound_var2;
    public AudioClip secondarySound;
    [Header("Set Vol for each sound")]
    public float vol1 = 0.31f;
    public float vol2 = 0.21f;
    [Header("Delay between each sound")]
    public float minDelay = 0.5f;
    public float maxDelay = 3.0f;
    private bool areaExited = false;
    private bool areaEntered = false;
    [Header("How fast you want the sound to fade in/out")]
    public float fadespeed = 3;

    private int fadingType = 0;


    // Start is called before the first frame update
    void Start()
    {
        source = GetComponents<AudioSource>();
        if (!source[0].isPlaying || source[1].isPlaying) //flag
        {
            primary = source[0];
            primary.volume = 0;
            primary.PlayOneShot(areaSound_var1);    
            source[0].clip = areaSound_var1;
            source[1].clip = areaSound_var2;

            secondary = source[1];
            secondary.volume = 0;
            secondary.PlayOneShot(secondarySound);
            
        }
    }

    //Due to the unique shape of the shorelines and areas. I decided to make this code instead of focusing on SpotFX's. 
    //Those can be used by the torches. with extension to using this. You get the versatility of marking unique areas.

    private void OnTriggerExit(Collider other)
    {
        
        areaExited = true;
        fadingType = 2;
    }

    private void OnTriggerEnter(Collider other)
    {
        
        areaEntered = true;
        fadingType = 1;
    }

    void Update()
    {
        if (!areaExited)
        {
            if (!source[0].isPlaying)
            {
                float d = Random.Range(minDelay, maxDelay);
                source[0].PlayDelayed(d);
            }
            if (!source[1].isPlaying)
            {
                float d = Random.Range(minDelay, maxDelay);
                source[1].PlayDelayed(d);
            }
        }
        if (areaExited || areaEntered)
        {
            checkFadeType();
        }


        
    }

    void checkFadeType()
    {
        if (fadingType==1)
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
        } else if (primary.volume >= vol1)
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
