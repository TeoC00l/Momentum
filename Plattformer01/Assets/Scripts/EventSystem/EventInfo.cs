using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EventInfo
{
    public string EventDescription;
}

public class DebugEvent : EventInfo
{
}

public class DieEvent : EventInfo
{
    public GameObject UnitGameObject;
}

