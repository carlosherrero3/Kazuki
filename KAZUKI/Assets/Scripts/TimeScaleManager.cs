using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaleManager : MonoBehaviour
{
    void Start()
    {
        Time.timeScale = 1.0f;
        Debug.Log("El tiempo esta en 1f");
    }

    void Update()
    {
        
    }
}
