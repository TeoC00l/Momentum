using UnityEngine;
using UnityEngine.UI;

public class UITimer : MonoBehaviour
{
    [SerializeField] private Text TimerText; 
    [SerializeField]private bool playing;
    private float Timer;
    private float minutes;
    private float seconds;
    private float milliseconds;

    void Update () {

 	if(playing == true){
  
	  Timer += Time.deltaTime;
	  minutes = Mathf.Floor(Timer / 60F);
	  seconds = Mathf.Floor(Timer % 60F);
	  milliseconds = Mathf.Floor((Timer * 100F) % 100F);
	  TimerText.text = minutes.ToString ("00") + ":" + seconds.ToString ("00") + ":" + milliseconds.ToString("00");


    }

  }
    public void SetPlayable(bool set)
    {
        this.playing = set;
    }
    public float GetTimeAsFloat()
    {
        return ((minutes + (seconds / 100)) + (milliseconds / 10000));
    }
    public string goodLookingTimer()
    {
        return minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + milliseconds.ToString("00");
    }

}

