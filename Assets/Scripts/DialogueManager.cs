using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Manages uploading the dialogue to the UI elements
/// </summary>
public class DialogueManager : StateInteractor
{
    [Header("Dialogue Data")]
    [SerializeField] private List<DialoguePool> pools;
    [SerializeField] private DialoguePool successPool;
    [SerializeField] private DialoguePool failPool;
    private DialogueOption option1Data;
    private DialogueOption option2Data;
    private DialogueOption reactData;

    [Header("Channels")]
    [SerializeField] private IntIntChannel choiceStatChannel;
    [SerializeField] private BoolChannel dialogueSuccessChannel;
    
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI option1;
    [SerializeField] private TextMeshProUGUI option2;
    [SerializeField] private TextMeshProUGUI dialogueBox;

    [Tooltip("Max amount of characters allowed in a box")]
    [SerializeField] private int textboxLimit;

    private void Start()
    {
        dialogueSuccessChannel.OnEventRaised += ChooseReactDialogue;
    }

    // When state changed to talking, start dialogue system 
    protected override void OnStateChange(int arg0)
    {
        // If transitioned to talking state, load dialogue
        if(GameManager.Instance.CurrentState == GameManager.GameState.Talking)
        {
            GetDialogueOptions();
        }
    }

    private void GetDialogueOptions()
    {
        int opt1 = Random.Range(0, pools.Count);
        int opt2;
        do
        {
            opt2 = Random.Range(0, pools.Count);
        } while (opt1 == opt2);

        option1Data = pools[opt1].GetFromPool();
        option2Data = pools[opt2].GetFromPool();

        LoadDialogue(option1, option1Data);
        LoadDialogue(option2, option2Data);
    }

    /// <summary>
    /// Export the choice option and its stat to game manager
    /// </summary>
    /// <param name="option">The option selected. [1,2]</param>
    public void ExportChoice(int option)
    {
        if(option == 1)
        {
            choiceStatChannel.RaiseEvent((int)option1Data.statID, option1Data.statVal);
        }
        else if (option == 2)
        {
            choiceStatChannel.RaiseEvent((int)option2Data.statID, option2Data.statVal);
        }
    }

    /// <summary>
    /// Choose the react dialogue based on passed in info. 
    /// </summary>
    /// <param name="successful">Whether or not the calculation passed</param>
    private void ChooseReactDialogue(bool successful)
    {
        if(successful)
        {
            reactData = successPool.GetFromPool();
        }
        else
        {
            reactData = failPool.GetFromPool();
        }

        LoadDialogue(dialogueBox, reactData);
    }

    /// <summary>
    /// Load the dialogue into a target destination
    /// </summary>
    /// <param name="target">target UI element to load the text</param>
    /// <param name="data">text to load</param>
    private void LoadDialogue(TextMeshProUGUI target, DialogueOption data)
    {
        // TODO - Add in rolling characters and broken characters by \n and character limits
        target.text = data.dialogueFull;
    }
}
