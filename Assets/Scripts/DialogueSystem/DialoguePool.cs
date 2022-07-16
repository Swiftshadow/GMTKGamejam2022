using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue Pool", menuName = "ScriptableObjects/Dialogue Pool")]
public class DialoguePool : ScriptableObject
{
    [SerializeField] private List<DialogueOption> pool;
    [SerializeField] private List<DialogueOption> corePool;

    private DialogueOption lastUsed = null;
    private bool justRefreshed = false;

    private void Start()
    {
        corePool = new List<DialogueOption>(pool);
    }

    /// <summary>
    /// Get a dialogue from this pool
    /// </summary>
    /// <returns>Last dialogue option used</returns>
    public DialogueOption GetFromPool()
    {
        DialogueOption option;
        int r;
        // Select once. If it was recently refreshed, then make sure the last post isnt selected again
        do
        {
            r = Random.Range(0, pool.Count);
            Debug.Log("Pool : " + r);
            option = pool[r];
        } while (justRefreshed && option == lastUsed);
        justRefreshed = false;
        pool.Remove(option);

        // Check if pool is empty. If empty, refresh
        if (pool.Count == 0)
        {
            Debug.Log("Out of text, refreshing " + name + " pool");
            pool = new List<DialogueOption>(corePool);
            justRefreshed = true;
            lastUsed = option;
        }
        return option;
    }
}
