using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBehaviour : StateInteractor
{
    public void OnClick()
    {
        Debug.Log("Changing State!");
        requestStateChange.RaiseEvent((int)GameManager.GameState.StatSelection);
    }

    protected override void OnStateChange(int arg0)
    {
        return;
    }
}
