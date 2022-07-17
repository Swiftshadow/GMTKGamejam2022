using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaiterManager : MonoBehaviour
{
    private Animator a;
    [Tooltip("Min and max time between waitor walking by. In seconds")]
    [SerializeField] private Vector2 timeRangeBounds;

    // Start is called before the first frame update
    void Start()
    {
        a = GetComponent<Animator>();
        GetNextCycleData();
    }

    private void GetNextCycleData()
    {
        float time = Random.Range(timeRangeBounds.x, timeRangeBounds.y);
        float temp = Random.Range(0, 2);
        bool fromRight = temp == 0;

        StartCoroutine(WaitForNextCycle(time, fromRight));
    }

    public void DisableWaiter()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }
    public void EnableWaiter()
    {
        GetComponent<SpriteRenderer>().enabled = true;
    }


    private IEnumerator WaitForNextCycle(float time, bool direction)
    {
        yield return new WaitForSecondsRealtime(time);
        if (direction)
            a.SetTrigger("WalkFromRight");
        else
            a.SetTrigger("WalkFromLeft");
        GetNextCycleData();
    }
}
