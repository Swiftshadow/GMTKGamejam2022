//**********************************************
//Based on Doug Guzman's work in Disaster Golf
//**********************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Base version of single-variable channels
/// </summary>
/// <typeparam name="T">Variable type to be used in channel</typeparam>
public class ChannelOneVariable<T> : ScriptableObject
{
    /// <summary>
    /// Action(s) to be invoked when event is raised
    /// </summary>
    public UnityAction<T> OnEventRaised;

    /// <summary>
    /// Call all subscribed functions
    /// </summary>
    /// <param name="var">Value to be passed into event</param>
    public virtual void RaiseEvent(T val = default(T))
    {
        OnEventRaised?.Invoke(val);
    }
}
