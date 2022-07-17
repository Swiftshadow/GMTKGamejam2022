using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LadyLuckPoseManager : StateInteractor
{
    /// <summary>
    /// Poses for Lady Luck. Default pose is 0
    /// </summary>
    [SerializeField] private Sprite[] poses;
    private SpriteRenderer ladyLuck;
    [SerializeField] private Pose currentPose;

    [SerializeField] private IntChannel poseChannel;
    public enum Pose
    {
        Default,
        Laugh,
        Smug,
        Excited,
        HeadTurn,
        Thinking
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        poseChannel.OnEventRaised += LoadNewReaction;
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        poseChannel.OnEventRaised -= LoadNewReaction;
    }

    protected override void OnStateChange(int arg0)
    {
        Debug.Log("Detected state change");
        if(arg0 == (int)GameManager.GameState.SlideIn || arg0 == (int)GameManager.GameState.Win || arg0 == (int)GameManager.GameState.Lose)
        {
            RevertReaction();
        }
    }

    private void Start()
    {
        ladyLuck = GetComponent<SpriteRenderer>();
        poses[0] = ladyLuck.sprite;
        currentPose = Pose.Default;
    }

    public void LoadNewReaction(int index)
    {
        Debug.Log("[LLPoseManager]: Setting pose to " + (Pose)index);
        ladyLuck.sprite = poses[index];
        currentPose = (Pose)index;
    }

    public void RevertReaction()
    {
        LoadNewReaction(0);
    }
}
