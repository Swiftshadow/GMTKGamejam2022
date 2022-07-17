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

    [SerializeField] private Button[] addButtons;

    [SerializeField] private Button[] subtractButtons;

    private int baseStat;

    private void Start()
    {
        baseStat = GameManager.Instance.GetBaseStats();
    }

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
            foreach (Button butt in addButtons)
                butt.interactable = false;
        }
        else if(GameManager.Instance.GetStat(3) != 0 && confirmButton.interactable == true)
        {
            confirmButton.interactable = false;
            foreach (Button butt in addButtons)
                butt.interactable = true;
        }
        CheckStatMins();
    }

    // Disable and enable the subtract buttons based on if they are already maxed out
    private void CheckStatMins()
    {
        
        if(GameManager.Instance.GetStat(0) <= baseStat && subtractButtons[0].interactable == true)
        {
            subtractButtons[0].interactable = false;
        }
        else if(GameManager.Instance.GetStat(0) > baseStat && subtractButtons[0].interactable == false)
        {
            subtractButtons[0].interactable = true;
        }

        if (GameManager.Instance.GetStat(1) <= baseStat && subtractButtons[1].interactable == true)
        {
            subtractButtons[1].interactable = false;
        }
        else if (GameManager.Instance.GetStat(1) > baseStat && subtractButtons[1].interactable == false)
        {
            subtractButtons[1].interactable = true;
        }

        if (GameManager.Instance.GetStat(2) <= baseStat && subtractButtons[2].interactable == true)
        {
            subtractButtons[2].interactable = false;
        }
        else if (GameManager.Instance.GetStat(2) > baseStat && subtractButtons[2].interactable == false)
        {
            subtractButtons[2].interactable = true;
        }
    }

    protected override void OnStateChange(int arg0)
    {
        
    }
}
