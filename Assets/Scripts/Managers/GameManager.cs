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

    private enum GameState
    {
        Menu,
        Gameplay,
        Paused
    }

    private void Start()
    {
    }

}
