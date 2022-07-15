using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLoader : MonoBehaviour
{
    [Tooltip("Name of scenes to interact with")]
    [SerializeField] private string[] sceneNames;
    [Tooltip("Whether to load addatively (UI) or single (scenes)")]
    [SerializeField] private bool addativeLoad;

    /// <summary>
    /// Send scenes to load to load manager
    /// </summary>
    public void Load()
    {
        if(sceneNames.Length == 0)
        {
            Debug.Log("Error! " + gameObject.name + " has no scene names to load!");
            return;
        }

        if(addativeLoad)
        {
            LoadManager.Instance.LoadAdditive(sceneNames);
        }
        else
        {
            LoadManager.Instance.LoadSingle(sceneNames[0]);
        }
    }

    /// <summary>
    /// Send scenes to unload to load manager
    /// </summary>
    public void Unload()
    {
        if (sceneNames.Length == 0)
        {
            Debug.Log("Error! " + gameObject.name + " has no scene names to load!");
            return;
        }

        LoadManager.Instance.UnloadAddative(sceneNames);
    }
}
