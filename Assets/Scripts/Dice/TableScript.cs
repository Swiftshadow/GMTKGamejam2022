using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableScript : StateInteractor
{
    private Animator anim;

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }
    
    protected override void OnStateChange(int arg0)
    {
        GameManager.GameState state = (GameManager.GameState)arg0;
        if (state != GameManager.GameState.SlideIn && state != GameManager.GameState.SlideOut)
        {
            return;
        }

        if (state == GameManager.GameState.SlideIn)
        {
            anim.SetTrigger("SlideIn");
        }
        else
        {
            anim.SetTrigger("SlideOut");
        }
    }

    public void SetToRollable()
    {
        requestStateChange.RaiseEvent((int)GameManager.GameState.Rollable);
    }

    public void SetToTalking()
    {
        requestStateChange.RaiseEvent((int)GameManager.GameState.Talking);
    }
}
