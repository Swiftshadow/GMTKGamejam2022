using System;
using UnityEngine;

public abstract class StateInteractor : MonoBehaviour
{
    [SerializeField] private IntChannel stateChangedChannel;

    [SerializeField] protected IntChannel requestStateChange;


    private void OnEnable()
    {
        stateChangedChannel.OnEventRaised += OnStateChange;
    }

    protected virtual void OnStateChange(int arg0)
    {
        throw new System.NotImplementedException();
    }

    private void OnDisable()
    {
        stateChangedChannel.OnEventRaised -= OnStateChange;
    }
}