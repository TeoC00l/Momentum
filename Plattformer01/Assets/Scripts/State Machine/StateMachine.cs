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
    private State lastState;

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
        lastState = currentState;
        currentState.Exit();
        currentState = stateDictionary[typeof(T)];
        currentState.Enter();
    }

    public void TransitionBack()
    {
        State __lastState = currentState;

        currentState.Exit();
        currentState = lastState;
        currentState.Enter();

        lastState = __lastState;
    }

    protected virtual void FixedUpdate()
    {
        currentState.HandleFixedUpdate();
    }

    protected virtual void Update()
    {
        currentState.HandleUpdate();
    }

    //Getters and Setters
    public State GetLastState()
    {
        return lastState;
    }
    public State GetCurrentState()
    {
        return currentState;
    }
    public State[] getSpecificState()
    {
        return states;
    }
}
