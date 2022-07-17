using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAnimator : StateInteractor
{
    public GameManager.GameState validState;

    public string triggerName;
    
    private Animator anim;

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }
    
    protected override void OnStateChange(int arg0)
    {
        if ((GameManager.GameState)arg0 != validState)
        {
            return;
        }
        
        anim.SetTrigger(triggerName);
    }
}
