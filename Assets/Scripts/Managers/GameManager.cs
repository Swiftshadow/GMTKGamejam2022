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
            maxPoints = points;
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
        previousState = currentState;
        currentState = (GameState)state;
        stateChangedChannel.RaiseEvent(state);
    }
    
    #endregion

    #region Stats

    [SerializeField] private IntChannel buyStatChannel;

    [SerializeField] private IntChannel sellStatChannel;
    
    [Tooltip("Base stats. First is Charm, second is Int, third is Guilt")]
    [SerializeField] private int[] baseStats;
    
    /// <summary>
    /// Stat mods. First is Charm, second is Int, third is Guilt
    /// </summary>
    [SerializeField] private int[] modStats;

    [SerializeField] private int points = 3;

    private int maxPoints;
    
    public int Points
    {
        get
        {
            return points;
        }

        set
        {
            points = value;
        }
        
    }

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
        if (points > 0)
        {
            --points;
            SetBaseStat(index, baseStats[index] + 1);
        }
    }

    public void SellStat(int index)
    {
        if (points < maxPoints && baseStats[index] > 0)
        {
            ++points;
            SetBaseStat(index, baseStats[index] - 1);
        }
    }
    
    #endregion
}
