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
/// <typeparam name="T1">Variable type 1 to be used in channel</typeparam>
/// <typeparam name="T2">Variable type 2 to be used in channel</typeparam>
public class ChannelTwoVariable<T1, T2> : ScriptableObject
{
    /// <summary>
    /// Action(s) to be invoked when event is raised
    /// </summary>
    public UnityAction<T1,T2> OnEventRaised;

    /// <summary>
    /// Call all subscribed functions
    /// </summary>
    /// <param name="var1">Value 1 to be passed into event</param>
    /// <param name="var2">Value 2 to be passed into event</param>
    public virtual void RaiseEvent(T1 val1 = default(T1), T2 val2 = default(T2))
    {
        OnEventRaised?.Invoke(val1, val2);
    }
}
