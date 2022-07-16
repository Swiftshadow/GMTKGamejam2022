using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Side-agnostic die script. Rolls the die and broadcasts the result via a channel
/// </summary>
[RequireComponent(typeof(Rigidbody))] [RequireComponent(typeof(Collider))]
public class DieScript : MonoBehaviour
{

    public SelfIntChannel rollResultChannel;

    private Rigidbody rb;

    [SerializeField] private float speedZ;

    [SerializeField] private float speedX;

    [SerializeField] private Transform[] sides;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        Roll();
    }

    private void Roll()
    {
        int dirX = (Random.Range(0, 2) * 2) - 1;
        int dirZ = (Random.Range(0, 2) * 2) - 1;
        rb.velocity = new Vector3(speedX * dirX, 0, speedZ * dirZ);
        StartCoroutine(CheckSide());
    }

    private IEnumerator CheckSide()
    {
        WaitForSeconds wait = new WaitForSeconds(0.1f);
        while (rb.velocity.sqrMagnitude > 0)
        {
            yield return wait;
        }

        int result = 0;

        float maxHeight = -Mathf.Infinity;
        Transform top = null;

        foreach (Transform pos in sides)
        {
            if (pos.position.y > maxHeight)
            {
                maxHeight = pos.position.y;
                top = pos;
            }
        }

        result = Int32.Parse(top.name);
        
        Debug.Log($"Rolled {result}", gameObject);
        
        rollResultChannel.RaiseEvent(rollResultChannel, result);
    }
}
