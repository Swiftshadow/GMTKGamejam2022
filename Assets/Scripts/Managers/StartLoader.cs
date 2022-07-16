using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLoader : MonoBehaviour
{
    [SerializeField] private string[] toLoad;
    
    // Start is called before the first frame update
    void Awake()
    {
        LoadManager.Instance.LoadAdditive(toLoad);
        Destroy(gameObject);
    }

}
