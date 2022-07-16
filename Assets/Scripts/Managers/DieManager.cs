using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieManager : StateInteractor
{
    [SerializeField] private float diceToSpawn = 2;

    [SerializeField] private GameObject die;

    public GameObject Die
    {
        get
        {
            return die;
        }

        set
        {
            die = value;
        }
    }

    private Coroutine rollRoutine;

    [SerializeField] private List<SelfIntChannel> dieChannels = new List<SelfIntChannel>();

    private List<GameObject> spawnedDice = new List<GameObject>();

    private int result;

    public int Result
    {
        get
        {
            return result;
        }
    }
    protected override void OnStateChange(int intState)
    {
        GameManager.GameState state = (GameManager.GameState)intState;
        if (state != GameManager.GameState.Rolling)
        {
            if (rollRoutine != null)
            {
                StopCoroutine(rollRoutine);
            }
            
            return;
        }

        result = 0;
        rollRoutine = StartCoroutine(RollRoutine());
        

    }

    private IEnumerator RollRoutine()
    {
        SpawnDice(diceToSpawn);

        WaitForSeconds wait = new WaitForSeconds(0.1f);
        
        while (dieChannels.Count > 0)
        {
            yield return wait;
        }

        foreach (var die in spawnedDice)
        {
            Destroy(die);
        }
        
        spawnedDice.Clear();
        
        requestStateChange.RaiseEvent((int)GameManager.GameState.Modifying);
        
    }

    private void SpawnDice(float f)
    {
        for (int i = 0; i < f; ++i)
        {
            DieScript ds = Instantiate(die).GetComponent<DieScript>();

            spawnedDice.Add(ds.gameObject);
            
            SelfIntChannel dieChannel = ScriptableObject.CreateInstance<SelfIntChannel>();
            dieChannels.Add(dieChannel);
            dieChannel.OnEventRaised += ReportResult;
            ds.rollResultChannel = dieChannel;
        }
    }

    private void ReportResult(SelfIntChannel channel, int num)
    {
        result += num;
        Debug.Log($"Adding {num} to result!");
        dieChannels.Remove(channel);
    }
}
