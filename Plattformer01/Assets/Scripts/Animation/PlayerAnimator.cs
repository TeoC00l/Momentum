using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Animator anim; //dont change this please.
    public Animator animPrecis;
    public Animator animMoment;

    public float speed;
    public float direction;

    public bool momentumToggle = false;
    public bool canKinetic = false; //maybe not so nice.
    public string currentMode = "precision";
    // "precision" "momentum"

    

    void Start()
    {
        anim = GetComponent<Animator>();
        animPrecis = GetComponent<Animator>();
        animMoment = GetComponent<Animator>();
        
    }

    void Update()
    {
        direction = -Input.GetAxis("Horizontal");
        speed = Input.GetAxis("Vertical");
        anim.SetFloat("Speed", speed); //setzero eventually to speed direction etc.
        anim.SetFloat("Direction", direction);

        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("Taunt"); //click button
            //precisionMode(); precision mode shift toggle.
            momentumToggle = !momentumToggle;

            //playSoundClick to show that a mode is changed.
        }

        modeCentral();
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
