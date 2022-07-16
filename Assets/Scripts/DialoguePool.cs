using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialoguePool : MonoBehaviour
{
    [SerializeField] private List<DialogueOption> pool;
    private static List<DialogueOption> corePool;

    private DialogueOption lastUsed = null;
    private bool justRefreshed = false;

    private void Start()
    {
        corePool = pool;
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
            r = Random.Range(0, pool.Count - 1);
            option = pool[r];
        } while (justRefreshed && option == lastUsed);
        justRefreshed = false;
        pool.RemoveAt(r);

        // Check if pool is empty. If empty, refresh
        if (pool.Count == 0)
        {
            pool = corePool;
            justRefreshed = true;
            lastUsed = option;
        }
        return option;
    }
}
