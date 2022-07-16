using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatShop : StateInteractor
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

    public void Confirm()
    {
        if (GameManager.Instance.GetStat(3) == 0)
        {
            requestStateChange.RaiseEvent((int)GameManager.GameState.Transition);
            string[] temp = { "StatShop" };
            LoadManager.Instance.UnloadAddative(temp);
        }
    }

    protected override void OnStateChange(int arg0)
    {
        
    }
}
