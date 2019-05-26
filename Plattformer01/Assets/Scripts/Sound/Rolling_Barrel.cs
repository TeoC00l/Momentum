using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rolling_Barrel : MonoBehaviour
{
    private AudioSource[] sources;
    private AudioSource source1;
    private AudioSource source2;
    private AudioSource source3;
    [Header("Sounds")]
    public AudioClip collide_wall_clip;
    public AudioClip rolling_noise;
    public AudioClip bumpfloor;
    private float playerVeloci;


    //private sound thresholds
    private float volumeLowRange = 0.33f;
    private float volHighRange = 1.0f;
    private float lowPitchRange = 0.1f;
    private float highPitchRange = 1.0f;

    // Start is called before the first frame update
    void Start()
    {

        sources = GetComponents<AudioSource>();
        source1 = sources[0];

        source2 = sources[1];
        source2.clip = rolling_noise;
        source2.loop = true;

        source3 = sources[2];
        source3.clip = collide_wall_clip;
        source3.loop = false;
    }
    private void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.CompareTag("Walls")) {
            Debug.Log("Collided with Wall");
            float vol = Random.Range(volumeLowRange, volHighRange);
            source3.pitch = Random.Range(lowPitchRange, highPitchRange);
            source3.PlayOneShot(collide_wall_clip, vol);
        }
    }

    // Update is called once per frame
    void Update()
    {
        source2.volume = playerVeloci / 10;
        source2.pitch = playerVeloci / 5;
        if (playerVeloci < 0.3f)
        {
            source2.Pause();
        }
        else
        {
            if (!source2.isPlaying)
            {
                float wow = 0.3f;
                source2.Play();
                wow -= Time.deltaTime;
            }
        }
    }

    private void FixedUpdate()
    {
        playerVeloci = GetComponent<Rigidbody>().velocity.magnitude;
    }
}
