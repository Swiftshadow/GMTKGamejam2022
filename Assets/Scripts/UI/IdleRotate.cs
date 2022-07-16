using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleRotate : MonoBehaviour
{

    private RectTransform rt;

    [SerializeField] private float maxAngle;

    [SerializeField] private float rotSpeed;
    
    private bool dir = false;
    
    // Start is called before the first frame update
    void Start()
    {
        rt = gameObject.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        float currAngle = rt.eulerAngles.z;

        float newRot = rt.eulerAngles.z;
        
        if (Mathf.Abs(currAngle) > maxAngle)
        {
            dir = !dir;
        }
        
        if (dir)
        {
            newRot += rotSpeed * Time.deltaTime;
        }
        else
        {
            newRot -= rotSpeed * Time.deltaTime;
        }
        
        rt.rotation = Quaternion.Euler(rt.eulerAngles.x, rt.eulerAngles.y, newRot);
    }
}
