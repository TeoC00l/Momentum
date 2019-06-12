﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Main Author: Yukun

public class PlayerAnimator : MonoBehaviour
{
    public GameObject player;
    public Animator anim; //dont change this please.
    public Animator animPrecis;
    public Animator animMoment;

    public float speed;
    public float direction;

    public bool momentumToggle = false;
    public bool canKinetic = false; //maybe not so nice.
    public string currentMode = "precision";
    // "precision" "momentum

    public bool kineticOn = false;

    

    void Start()
    {
        //you need to set player in the console.
        anim = GetComponent<Animator>();
        animPrecis = GetComponent<Animator>();
        animMoment = GetComponent<Animator>();
        
    }

    void Update()
    {

        if (player.GetComponent<Player>().GetKineticActive()){ kineticOn = true; }
        if (!player.GetComponent<Player>().GetKineticActive()) { kineticOn = false; }


        if (!kineticOn)
        {
            direction = -Input.GetAxis("Horizontal");
            speed = Input.GetAxis("Vertical");
            anim.SetFloat("Speed", speed); //setzero eventually to speed direction etc.
            anim.SetFloat("Direction", direction);

            modeCentral();
        }

        if (kineticOn)
        {
            anim.Play("kineticActivate");
            if (Input.GetButtonDown("Fire1"))
            {
                //anim.SetTrigger("Taunt");
                kineticOn = false;
                //anim.Play("kineticActivate");
                
            }
        }
    }

    void modeCentral()
    {
        //use getter here instead.
        if (momentumToggle)
        {
            momentumMode();
            currentMode = "momentum";
        }
        if (!momentumToggle)
        {
            precisionMode();
            currentMode = "precision";
        }
    }



    //model modes. changes modes for skateboard to playermodel simultaneously.
    void precisionMode()
    {
        skatePrecision();
        gameObject.GetComponent<PlayerAnimator>().anim = animPrecis; //this changes to correct blendstate.
        //playSoundprecis
    }

    void momentumMode()
    {
        skateMomentum();
        gameObject.GetComponent<PlayerAnimator>().anim = animMoment; //this changes to correct blendstate.
        //playSoundMoment
    }

    void kineticMode()
    {
        if (!currentMode.Equals("precision"))
        {
            skateKinetic();
        }
    }


    //skateboard modes
    void skatePrecision()
    {
        //activate precision animation
        //play kinetic sound. maybe extra code for intermittance to kinetic reaving.
    }

    void skateMomentum()
    {
        //activate into momentum mode
    }

    void skateKinetic()
    {
        //start animation. but immediatly stops it when mouse is clicked.
    }



}
