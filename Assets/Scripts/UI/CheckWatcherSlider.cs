using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckWatcherSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider = gameObject.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = (float)GameManager.Instance.currScore / GameManager.Instance.maxScore;
    }
}
