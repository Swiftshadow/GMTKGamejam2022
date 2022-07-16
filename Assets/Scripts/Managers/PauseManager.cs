using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : StateInteractor
{

    private bool isPaused = false;

    protected override void OnStateChange(int arg0)
    {
        if ((GameManager.GameState)arg0 != GameManager.GameState.Paused)
        {
            return;
        }

        string[] scene = { "PauseMenu" };
        
        isPaused = !isPaused;
        if (isPaused)
        {
            Time.timeScale = 0;
            LoadManager.Instance.LoadAdditive(scene);
        }
        else
        {
            Time.timeScale = 1;
            LoadManager.Instance.UnloadAddative(scene);
        }
        
    }
}
