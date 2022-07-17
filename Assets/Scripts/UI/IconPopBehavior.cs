using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconPopBehavior : MonoBehaviour
{
    [SerializeField] private Sprite[] icons;
    private Animator anim;
    private int targetSprite;
    private Image image;
    private Vector3 originalPos;

    private void Start()
    {
        anim = GetComponent<Animator>();
        image = GetComponent<Image>();
        originalPos = GetComponent<RectTransform>().position;
    }

    public void EmotionPop(DialogueOption data)
    {
        targetSprite = (int)data.statID;
        image.sprite = icons[targetSprite];
        anim.SetTrigger("Pop");
    }

    public void IconPopReset()
    {
        image.color = new Color(1, 1, 1, 0);
        GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        GetComponent<RectTransform>().position = originalPos;
    }
}
