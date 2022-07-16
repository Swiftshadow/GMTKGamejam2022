using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    #region Instance Setting

    /// <summary>
    /// Static instance of the singleton
    /// </summary>
    private static GameAssets instance;

    /// <summary>
    /// Instance of the GameManager
    /// </summary>
    public static GameAssets Instance
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

    public AudioClip[] sounds;
}
