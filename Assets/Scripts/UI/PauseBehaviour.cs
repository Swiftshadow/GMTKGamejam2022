using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseBehaviour : StateInteractor
{

    private AudioSource bgm;

    private void Start()
    {
        bgm = GameObject.FindWithTag("GameAssets").GetComponent<AudioSource>();
    }
    
    public void Close()
    {
        requestStateChange.RaiseEvent((int)GameManager.GameState.Paused);
    }

    public void MainMenu()
    {
        requestStateChange.RaiseEvent((int)GameManager.GameState.Menu);
        LoadManager.Instance.LoadSingle("Title");
    }

    public void SetVolume(float value)
    {
        bgm.volume = value * 0.2f;
        SoundManager.volume = value;
    }
    
    protected override void OnStateChange(int arg0)
    {
        
    }
}
