/**********************************************
 * Based on Pneuma's code in Disaster Golf
 **********************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : StateInteractor
{
    private GameplayControls controls;

    private InputAction rollDice;

    private InputAction pause;
    
    // Start is called before the first frame update
    private void Awake()
    {
        controls = new GameplayControls();
        rollDice = controls.Player.RollDice;
        pause = controls.Player.Pause;
        controls.Player.Enable();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        rollDice.performed += SubscribeRolling;

        pause.performed += SubscribePausing;
    }

    protected override void OnStateChange(int arg0)
    {
        
    }

    protected override void OnDisable()
    {
        controls.Player.Disable();
        rollDice.performed -= SubscribeRolling;

        pause.performed -= SubscribePausing;
    }
    
    private void SubscribeRolling(InputAction.CallbackContext obj)
    {
        requestStateChange.RaiseEvent((int)GameManager.GameState.Rolling);
    }
    
    private void SubscribePausing(InputAction.CallbackContext obj)
    {
        requestStateChange.RaiseEvent((int)GameManager.GameState.Paused);
    }
}
