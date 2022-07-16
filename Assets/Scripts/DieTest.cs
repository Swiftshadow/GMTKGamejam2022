using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieTest : MonoBehaviour
{
    [SerializeField] private Transform[] sides;
    [SerializeField] private float spien;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            CheckSide();
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SPIN();
        }
    }

    private void SPIN()
    {
        Debug.Log("SPIIIIIIEEEEEN");
        GetComponent<Rigidbody>().angularVelocity.Set(Random.Range(-spien, spien), Random.Range(-spien, spien), Random.Range(-spien, spien));
    }

    private void CheckSide()
    {
        float maxHeight = -1;
        Transform top = null;
        foreach(Transform pos in sides)
        {
            if(pos.position.y > maxHeight)
            {
                maxHeight = pos.position.y;
                top = pos;
            }
        }
        Debug.Log("Top side is: " + top.name);
    }
}
