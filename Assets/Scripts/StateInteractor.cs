using System;
using UnityEngine;

public abstract class StateInteractor : MonoBehaviour
{
    [SerializeField] private IntChannel stateChangedChannel;

    [SerializeField] protected IntChannel requestStateChange;


    protected virtual void OnEnable()
    {
        stateChangedChannel.OnEventRaised += OnStateChange;
    }

    protected virtual void OnStateChange(int arg0)
    {
        throw new System.NotImplementedException();
    }

    protected virtual void OnDisable()
    {
        stateChangedChannel.OnEventRaised -= OnStateChange;
    }
}