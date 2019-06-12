using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SoundPitcher : MonoBehaviour {

    //Main Author: Yukun

    [Header("Basics")]
    public float speed;

    public GameObject player;


	//private Rigidbody rb; // delete
	private int count;

    private AudioSource[] sources;
    private AudioSource source1;
    private AudioSource source2;
    private AudioSource source3;

    [Header("Pickup Sound effects")]
    public AudioClip[] pickup_sound;

    [Header("Jump Sound")]
    public AudioClip[] jumpNoises;
    public int clipIndex2;
    private float volLow = 0.1f;
    private float volHigh = 0.5f;
    private float pitchLow2 = 0.5f;
    private float pitchHigh2 = 1.0f;


    [Header("Other Sounds")]
    public AudioClip collide_wall_clip;
    public AudioClip rolling_noise;
    public AudioClip bumpfloor;
    public AudioClip jumpSound;



    public float playerVelocity;
    private int clipIndex;
    private float volumeLowRange = 0.33f;
    private float volHighRange = 0.5f;
    private float lowPitchRange = 0.1f;
    private float highPitchRange = 0.5f;


	// Use this for initialization
	void Start () {

        //rb = GetComponent<Rigidbody>();

        sources = GetComponents<AudioSource>();
        source1 = sources[0];

        source2 = sources[1];
        source2.clip = rolling_noise;
        source2.loop = true;

        source3 = sources[2];
        source3.clip = collide_wall_clip;
        source3.loop = false;

	}
	
	// Update is called once per frame
	void Update () {

        playerVelocity = player.GetComponent<PhysicsComponent>().GetVelocity().magnitude;

        //determines volume based on speed.
        source2.volume =  1;
        source2.pitch = playerVelocity / 50;
        if (playerVelocity < 0.3f)
        {
            source2.Pause();
        }
        else
        {
            if (!source2.isPlaying)
            {
                float deltaminus = 0.3f;
                source2.Play();
                deltaminus -= Time.deltaTime;
                
            }
        }

        //additional abilities
        if (Input.GetKeyDown(KeyCode.Space)) // not needed at the moment
        {
            source2.volume = Random.Range(volLow, volHigh);
            source2.volume = Random.Range(pitchLow2, pitchHigh2);

            Vector3 jump = new Vector3(Input.GetAxis("Horizontal"), 30, Input.GetAxis("Vertical"));
            //rb.AddForce(jump * speed);
            clipIndex2 = Random.Range(1, jumpNoises.Length);
            //if(clipIndex2 > -1 && clipIndex2 <= jumpNoises.Length)
            //{
            //    AudioClip clip2 = jumpNoises[clipIndex2];
            //    source2.PlayOneShot(jumpNoises[clipIndex2]);
            //    jumpNoises[clipIndex2] = jumpNoises[0];
            //    jumpNoises[0] = clip2;
            //}
           
        }


        //hit floor noise

	}

	// Updates before PhysicsEngine
	void FixedUpdate(){
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		//Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		//rb.AddForce (movement * speed);
        //playerVelocity = GetComponent<Rigidbody>().velocity.magnitude;
	}


	void OnTriggerEnter( Collider other ){
		
		//if ( other.gameObject.CompareTag ("PickUp") ){
		//	other.gameObject.SetActive (false);
		//	count++;
  //          clipIndex = Random.Range(0,pickup_sound.Length);
  //          source1.PlayOneShot(pickup_sound[clipIndex]);
		//}

        if (other.gameObject.CompareTag("Wall"))
        {
            float vol = Random.Range(volumeLowRange, volHighRange);
            source3.pitch = Random.Range(lowPitchRange, highPitchRange);
            source3.PlayOneShot(collide_wall_clip,vol);
        }

        if (other.gameObject.CompareTag("Floor"))
        {
            if (playerVelocity > 9.3f)
            {
                source3.PlayOneShot(bumpfloor);
            }
            
        }
    }

}
