using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class StateMachine : MonoBehaviour
{
    //Attributes
    [SerializeField] private State[] states;

    private Dictionary<Type, State> stateDictionary = new Dictionary<Type, State>();
    private State currentState;

    //Methods
    protected virtual void Awake()
    {
        foreach (State state in states)
        {
            State instance = Instantiate(state);
            instance.Initialize(this);
            stateDictionary.Add(instance.GetType(), instance);

            if (currentState == null)
                {
                currentState = instance;
                }

            currentState.Enter();
        }
    }

    public void Transition<T>() where T : State
    {
        currentState.Exit();
        currentState = stateDictionary[typeof(T)];
        currentState.Enter();
    }

    private void Update()
    {
        currentState.HandleUpdate();
    }
}
