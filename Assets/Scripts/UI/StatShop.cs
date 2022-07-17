using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatShop : StateInteractor
{

    [SerializeField] private IntChannel buyStatChannel;

    [SerializeField] private IntChannel sellStatChannel;

    [SerializeField] private VoidChannel confirmStatChannel;

    [SerializeField] private Button confirmButton;

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
            confirmStatChannel.RaiseEvent();
            requestStateChange.RaiseEvent((int)GameManager.GameState.Transition);
            string[] temp = { "StatShop" };
            LoadManager.Instance.UnloadAddative(temp);
        }
    }

    private void Update()
    {
        if (GameManager.Instance.GetStat(3) == 0 && confirmButton.interactable == false)
        {
            confirmButton.interactable = true;
        }
        else if(GameManager.Instance.GetStat(3) != 0 && confirmButton.interactable == true)
        {
            confirmButton.interactable = false;
        }
    }

    protected override void OnStateChange(int arg0)
    {
        
    }
}
