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
            for (int i = 0; i < maxStats.Length; ++i)
            {
                maxStats[i] = baseStats[i];
            }
            DontDestroyOnLoad(this.gameObject.transform.parent);
        }
        else
        {
            Debug.LogError("Warning! Second instance of " + name + " trying to be created!");
            Destroy(this.gameObject);
        }
    }
    #endregion

    #region State Control
    [SerializeField] private IntChannel stateChangedChannel;

    [SerializeField] private IntChannel requestStateChange;

    private GameState currentState = GameState.Menu;

    private GameState previousState = GameState.Menu;
    

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
        Transition,
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
        buyStatChannel.OnEventRaised += BuyStat;
        sellStatChannel.OnEventRaised += SellStat;
    }

    /// <summary>
    /// Changes the state of the game
    /// TODO: Add checks to make sure the state change is legal
    /// </summary>
    /// <param name="state"></param>
    private void ChangeState(int state)
    {
        GameState newState = (GameState)state;
        Debug.Log($"[GameManager] Request to change state to {newState}");
        // Prevents jumping states, unless going to a menu, win, or lose
        switch (newState)
        {
            case GameState.Rolling:
                if (currentState >= GameState.Win)
                {
                    break;
                }
                if ((currentState != GameState.Talking && currentState != GameState.Transition))
                {
                    return;
                }
                break;
            case GameState.Modifying:
                if (currentState >= GameState.Win)
                {
                    break;
                }
                if ((currentState != GameState.Rolling))
                {
                    return;
                }
                break;
            case GameState.Talking:
                if (currentState >= GameState.Win)
                {
                    break;
                }
                if ((currentState != GameState.Modifying))
                {
                    return;
                }
                break;
            case GameState.Paused:
                if (currentState == GameState.Paused)
                {
                    newState = previousState;
                }

                if (currentState == GameState.Menu || currentState == GameState.Lose || currentState == GameState.Win)
                {
                    return;
                }

                break;
            case GameState.Menu:
                for (int i = 0; i < maxStats.Length; ++i)
                {
                    baseStats[i] = maxStats[i];
                }

                currentState = GameState.Menu;
                
                break;
            default:
                Debug.Log("[GameManager] New state has no entry conditions");
                break;
        }
        
        Debug.Log($"[GameManager] Changing to {newState}");
        
        previousState = currentState;
        currentState = newState;
        stateChangedChannel.RaiseEvent(state);
    }
    
    #endregion

    #region Stats

    [SerializeField] private IntChannel buyStatChannel;

    [SerializeField] private IntChannel sellStatChannel;
    
    [Tooltip("Base stats. First is Charm, second is Int, third is Guilt, fourth is points")]
    [SerializeField] private int[] baseStats;
    
    /// <summary>
    /// Stat mods. First is Charm, second is Int, third is Guilt
    /// </summary>
    [SerializeField] private int[] modStats;

    private int[] maxStats = new int[4];

    /// <summary>
    /// Gets the specific stat.
    /// 0 is Charm
    /// 1 is Int
    /// 2 is Guilt
    /// </summary>
    /// <param name="index">Which stat to get</param>
    /// <returns></returns>
    public int GetStat(int index)
    {
        return baseStats[index] - modStats[index];
    }

    /// <summary>
    /// Sets the specific stat.
    /// 0 is Charm
    /// 1 is Int
    /// 2 is Guilt
    /// </summary>
    /// <param name="index">Which stat to set</param>
    /// <param name="value">Value to set it to</param>
    /// <returns></returns>
    public void SetStat(int index, int value)
    {
        modStats[index] = value;
    }

    /// <summary>
    /// Sets the specific base stat.
    /// 0 is Charm
    /// 1 is Int
    /// 2 is Guilt
    /// </summary>
    /// <param name="index">Which stat to set</param>
    /// <param name="value">Value to set it to</param>
    /// <returns></returns>
    private void SetBaseStat(int index, int value)
    {
        baseStats[index] = value;
    }

    public void BuyStat(int index)
    {
        if (baseStats[3] > 0)
        {
            --baseStats[3];
            SetBaseStat(index, baseStats[index] + 1);
        }
    }

    public void SellStat(int index)
    {
        if (baseStats[3] < maxStats[3] && baseStats[index] > 0)
        {
            ++baseStats[3];
            SetBaseStat(index, baseStats[index] - 1);
        }
    }
    
    #endregion
}
