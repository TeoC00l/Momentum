using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Timer")]
public class Timer : ScriptableObject
{
    [SerializeField] private float timerSetValue;
    [HideInInspector] private float timerCount;


    // Start is called before the first frame update
    void Awake()
    {
        timerCount = 0;
    }

    public void SetTimer()
    {
        timerCount = timerSetValue;
    }

    public void RestartTimer()
    {
        timerCount = 0;
    }


    public bool IsReady()
    {
        if (timerCount > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void SubtractTime()
    {
        if (timerCount > 0)
        {
            timerCount -= Time.deltaTime;
        }
    }

    public bool CheckLastFrame()
    {
        if (timerCount > 0 && timerCount -Time.deltaTime <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    public bool IsCountingDown()
    {
        if (IsReady())
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public float GetTimerSetValue()
    {
        return timerSetValue;
    }

    public float GetTimerCount()
    {
        return timerCount;
    }

}
