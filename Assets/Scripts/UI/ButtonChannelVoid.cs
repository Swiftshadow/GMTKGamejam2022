using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonChannelVoid : MonoBehaviour
{

    [SerializeField] private VoidChannel channel;

    public void OnClick()
    {
        channel.RaiseEvent();
    }

}
