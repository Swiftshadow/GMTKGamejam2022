using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{
    #region Instance Setting

    /// <summary>
    /// Static instance of the singleton
    /// </summary>
    private static LoadManager instance;

    /// <summary>
    /// Instance of the GameManager
    /// </summary>
    public static LoadManager Instance
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

    /// <summary>
    /// Name of the current active scene
    /// </summary>
    private string currentScene;
    public string CurrentScene
    {
        get
        {
            return currentScene;
        }
    }

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
    }

    /// <summary>
    /// Load to a single scene
    /// </summary>
    /// <param name="name">Name of the scene to load</param>
    public void LoadSingle(string name)
    {
        SceneManager.LoadSceneAsync(name, LoadSceneMode.Single);
        currentScene = name;
    }

    /// <summary>
    /// Additively load in scenes for UI in bulk
    /// </summary>
    /// <param name="sceneNames">Names of scenes to load</param>
    public void LoadAdditive(string[] sceneNames)
    {
        foreach (string scene in sceneNames)
        {
            SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
        }
    }

    /// <summary>
    /// Unload all specified scenes
    /// </summary>
    /// <param name="sceneNames">Array of scene names to unload</param>
    public void UnloadAddative(string[] sceneNames)
    {
        foreach (string name in sceneNames)
        {
            SceneManager.UnloadSceneAsync(name);
        }
    }
}
