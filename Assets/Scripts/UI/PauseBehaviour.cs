using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseBehaviour : StateInteractor
{
    public void Close()
    {
        requestStateChange.RaiseEvent((int)GameManager.GameState.Paused);
    }

    public void MainMenu()
    {
        requestStateChange.RaiseEvent((int)GameManager.GameState.Menu);
        LoadManager.Instance.LoadSingle("Title");
    }
    
    protected override void OnStateChange(int arg0)
    {
        
    }
}
