using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Instance Setting

    /// <summary>
    /// Static instance of the singleton
    /// </summary>
    private static GameManager instance;

    /// <summary>
    /// Instance of the GameManager
    /// </summary>
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    /// <summary>
    /// Prepare the instance
    /// </summary>
    void Awake()
    {
        // Set singleton
        if (instance is null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject.transform.parent);
        }
        else
        {
            Debug.LogError("Warning! Second instance of " + name + " trying to be created!");
            Destroy(this.gameObject);
        }
    }
    #endregion

    [SerializeField] private IntChannel stateChangedChannel;

    [SerializeField] private IntChannel requestStateChange;

    private GameState currentState;

    private GameState previousState;
    
    public GameState CurrentState
    {
        get
        {
            return currentState;
        }
    }
    
    public enum GameState
    {
        Menu,
        StatSelection,
        Rolling,
        Modifying,
        Talking,
        Win,
        Lose,
        Paused
    }

    private void OnEnable()
    {
        requestStateChange.OnEventRaised += ChangeState;
    }

    /// <summary>
    /// Changes the state of the game
    /// TODO: Add checks to make sure the state change is legal
    /// </summary>
    /// <param name="state"></param>
    private void ChangeState(int state)
    {
        previousState = currentState;
        currentState = (GameState)state;
        stateChangedChannel.RaiseEvent(state);
    }
}
