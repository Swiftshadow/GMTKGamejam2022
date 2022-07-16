using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatShop : MonoBehaviour
{

    [SerializeField] private IntChannel buyStatChannel;

    [SerializeField] private IntChannel sellStatChannel;
    
    public void BuyStat(int index)
    {
        buyStatChannel.RaiseEvent(index);
    }

    public void SellStat(int index)
    {
        sellStatChannel.RaiseEvent(index);
    }
    
}
