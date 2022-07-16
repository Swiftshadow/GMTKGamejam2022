using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatWatcher : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    
    [SerializeField] private int stat;
    
    // Start is called before the first frame update
    void Start()
    {
        text = gameObject.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = GameManager.Instance.GetStat(stat).ToString();
    }
}
