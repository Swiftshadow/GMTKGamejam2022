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

    // Start is called before the first frame update
    private void Awake()
    {
        controls = new GameplayControls();
        rollDice = controls.Player.RollDice;
        controls.Player.Enable();
    }

    private void OnEnable()
    {
        rollDice.performed += ctx => requestStateChange.RaiseEvent((int)GameManager.GameState.Rolling);
    }
}
