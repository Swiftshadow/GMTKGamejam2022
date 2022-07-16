using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue Data", menuName = "ScriptableObjects/Dialogue Data")]
public class DialogueOption : ScriptableObject
{
    public enum Stats
    {
        Charm,
        Int,
        Guilt
    }
    
    /// <summary>
    /// Full dialogue option
    /// </summary>
    public string dialogueFull;

    /// <summary>
    /// Shorthanded dialogue displayed on the selection prompt
    /// </summary>
    public string dialogueShorthand;

    /// <summary>
    /// ID for the stat this dialogue represents
    /// </summary>
    public Stats statID;

    /// <summary>
    /// 
    /// </summary>
    public int statVal;
}
