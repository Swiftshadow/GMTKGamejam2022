using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CheckWatcherText : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = gameObject.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = $"${GameManager.Instance.currScore}";
    }
}
