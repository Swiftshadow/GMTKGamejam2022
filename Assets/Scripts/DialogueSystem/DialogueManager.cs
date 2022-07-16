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

    private List<string> currentLines;
    [SerializeField] private float loadDelay;
    private int lineIndex;
    private bool loadingText;
    private Coroutine coroutineBuffer;

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

    /// <summary>
    /// Get two dialogue options to choose from, load into dialogue
    /// </summary>
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

        StartCoroutine(LoadText(option1, option1Data.dialogueFull, 0));
        StartCoroutine(LoadText(option2, option2Data.dialogueFull, 0));
    }

    /// <summary>
    /// Export the choice option and its stat to game manager
    /// </summary>
    /// <param name="option">The option selected. [1,2]</param>
    public void ExportChoice(int option)
    {
        Debug.Log("[DialogueManager] Exporting out option " + option);
        option1.transform.parent.gameObject.SetActive(false);
        option2.transform.parent.gameObject.SetActive(false);

        if (option == 1)
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
        if (successful)
        {
            reactData = successPool.GetFromPool();
        }
        else
        {
            reactData = failPool.GetFromPool();
        }

        PrepDialogue(reactData);
    }

    /// <summary>
    /// Divide data to match the character limit. Submits data to currentLines
    /// </summary>
    /// <param name="data">Dialogue data holding the text</param>
    private void PrepDialogue(DialogueOption data)
    {
        string fulldata = data.dialogueFull;

        List<string> dividedLines = new List<string>();
        string word = "";
        string line = "";
        for (int i = 0; i < fulldata.Length; i++)
        {
            // If a space, finish word, try adding to line
            if (fulldata[i] == ' ' || fulldata[i] == '\n')
            {
                // If it fits, then add word to line
                if (word.Length + line.Length + 1 <= textboxLimit && fulldata[i] == ' ')
                {
                    line += ' ' + word;
                    word = "";
                }
                // If overflowing, then submit the line and start a new one
                else if (word.Length + line.Length + 1 > textboxLimit)
                {
                    dividedLines.Add(line);
                    line = word;
                    word = "";
                }
            }
            // If an enter space and not overflowing, then add current word and start new line
            else if (fulldata[i] == '\n')
            {
                line += ' ' + word;
                dividedLines.Add(line);
                line = "";
                word = "";
            }
            else
            {
                word += fulldata[i];
            }
        }
        line += ' ' + word;
        dividedLines.Add(line);
        currentLines = dividedLines;
        lineIndex = 0;
        NextLine();
    }

    public void NextLine()
    {
        // If already loading the text, don't progress but instead instantly load it
        if(loadingText)
        {
            if(coroutineBuffer != null)
                StopCoroutine(coroutineBuffer);
            loadingText = false;
            dialogueBox.text = currentLines[lineIndex];
            lineIndex++;
        }
        // If reached final line, then send out a signal
        else if(lineIndex == currentLines.Count)
        {
            Debug.Log("Finished current dialogue!");
            dialogueBox.text = "";
        }
        // Progress to the next line
        else
        {
            coroutineBuffer = StartCoroutine(LoadText(dialogueBox, currentLines[lineIndex], loadDelay));
        }
    }

    /// <summary>
    /// Submit some dialogue to play independent of the choice/roll system
    /// </summary>
    /// <param name="dialogue"></param>
    public void EventDialogue(DialogueOption dialogue)
    {
        PrepDialogue(dialogue);
        NextLine();
        // TODO - add some form of channel notification
    }

    /// <summary>
    /// Load the dialogue into a target destination staggered
    /// </summary>
    /// <param name="target">target UI element to load the text</param>
    /// <param name="data">text to load</param>
    private IEnumerator LoadText(TextMeshProUGUI target, string data, float delay)
    {
        loadingText = true;
        string fulldata = data;
        string tempString = "";
        for(int i = 0; i < fulldata.Length; i++)
        {
            tempString += fulldata[i];
            target.text = tempString;
            yield return new WaitForSecondsRealtime(delay);
            yield return null;
        }
        target.text = fulldata;
        loadingText = false;
        lineIndex++;
    }
}
