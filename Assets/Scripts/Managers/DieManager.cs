using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DieManager : StateInteractor
{
    [SerializeField] private float diceToSpawn = 2;

    [SerializeField] private GameObject[] dice;

    private Coroutine rollRoutine;

    private Coroutine showRoutine;

    [Tooltip("Delay time between the dice finishing their roll and the showing process starting")]
    [SerializeField] private float delayTime = 0.5f;
    
    [SerializeField] private List<SelfIntChannel> dieChannels = new List<SelfIntChannel>();

    private List<GameObject> spawnedDice = new List<GameObject>();

    private int stat;

    private int mod;

    public int Stat
    {
        get
        {
            return stat;
        }
    }

    private int Mod
    {
        get
        {
            return mod;
        }
    }
    
    protected override void OnStateChange(int intState)
    {
        GameManager.GameState state = (GameManager.GameState)intState;
        if (state != GameManager.GameState.Rolling && state != GameManager.GameState.Modifying)
        {
            if (state != GameManager.GameState.Paused)
            {
                if (rollRoutine != null)
                {
                    StopCoroutine(rollRoutine);
                }

                if (showRoutine != null)
                {
                    StopCoroutine(ShowResults());
                }
            }
            
            return;
        }

        if (state == GameManager.GameState.Rolling)
        {
            // Zero out all mods before running the next dice
            for (int i = 0; i < 3; ++i)
            {
                GameManager.Instance.SetStat(i, 0);
            }

            stat = 0;
            mod = 0;
            rollRoutine = StartCoroutine(RollRoutine());
        }
        else
        {
            showRoutine = StartCoroutine(ShowResults());
        }


    }

    private IEnumerator RollRoutine()
    {
        SpawnDice(diceToSpawn);

        WaitForSeconds wait = new WaitForSeconds(0.1f);
        
        while (dieChannels.Count > 0)
        {
            yield return wait;
        }

        int modDir = Math.Sign(stat);

        stat = Mathf.Abs(stat);
        stat -= 7;
        mod *= modDir;
        
        GameManager.Instance.SetStat(stat, mod);

        if (modDir > 0)
        {
            SoundManager.PlaySound(GameAssets.Instance.sounds[7]);
        }
        else
        {
            SoundManager.PlaySound(GameAssets.Instance.sounds[3]);
        }
        
        requestStateChange.RaiseEvent((int)GameManager.GameState.Modifying);
        
    }

    private void SpawnDice(float f)
    {
        for (int i = 0; i < f; ++i)
        {
            DieScript ds = Instantiate(dice[i], transform.position, Quaternion.identity).GetComponent<DieScript>();

            spawnedDice.Add(ds.gameObject);
            
            SelfIntChannel dieChannel = ScriptableObject.CreateInstance<SelfIntChannel>();
            dieChannels.Add(dieChannel);
            dieChannel.OnEventRaised += ReportResult;
            ds.rollResultChannel = dieChannel;
        }
        SoundManager.PlaySound(GameAssets.Instance.sounds[Random.Range(0, 3)]);
    }

    private void ReportResult(SelfIntChannel channel, int num)
    {
        if (Mathf.Abs(num) >= 7)
        {
            stat = num;
        }
        else
        {
            mod = num;
        }
        Debug.Log($"Adding {num} to result!");
        dieChannels.Remove(channel);
    }

    private IEnumerator ShowResults()
    {
        yield return new WaitForSeconds(delayTime);

        foreach (var die in spawnedDice)
        {
            Destroy(die);
        }
        
        spawnedDice.Clear();
        
        requestStateChange.RaiseEvent((int)GameManager.GameState.SlideOut);
    }
}
