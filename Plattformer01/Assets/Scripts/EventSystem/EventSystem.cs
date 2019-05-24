using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : MonoBehaviour
{
    static private EventSystem __Current;
    static public EventSystem Current
    {
        get
        {
            if (__Current == null)
            {
                __Current = GameObject.FindObjectOfType<EventSystem>();
            }

            return __Current;
        }
    }

    public delegate void EventListener(EventInfo ei);
    Dictionary<EVENT_TYPE, List<EventListener>> eventListeners;

    public void RegisterListener(EVENT_TYPE eventType, EventListener listener)
    {
        if (eventListeners == null)
        {
            eventListeners = new Dictionary<EVENT_TYPE, List<EventListener>>();
        }

        if (eventListeners.ContainsKey(eventType) == false || eventListeners[eventType] == null)
        {
            eventListeners[eventType] = new List<EventListener>();
        }

        eventListeners[eventType].Add(listener);
    }

    public void UnregisterListener(EVENT_TYPE eventType, EventListener listener)
    {
        if (eventListeners.ContainsKey(eventType) == true && eventListeners[eventType].Contains(listener))
        {
            eventListeners[eventType].Remove(listener);
            Debug.Log("listener removed");
        }
    }

    public void FireEvent(EVENT_TYPE eventType, EventInfo eventInfo)
    {
        if (eventListeners == null || eventListeners[eventType] == null)
        {
            return;
        }

        foreach(EventListener el in eventListeners[eventType])
        {
            el(eventInfo);
        }
    }
}

