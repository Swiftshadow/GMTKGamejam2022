using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonChannel : MonoBehaviour
{

    [SerializeField] private IntChannel channel;

    public void OnClick(int value)
    {
        channel.RaiseEvent(value);
    }

}
