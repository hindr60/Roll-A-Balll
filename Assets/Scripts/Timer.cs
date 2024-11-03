using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float currentTime;
    public bool isTiming;
 
    void Update()
    {
       if(isTiming == true)
        {
            currentTime += Time.deltaTime;
        }
       
    }

    public float GetTime()
    {
        return currentTime;
    }

    public void StartTimer()
    {
        isTiming = true;
    }
    public void StopTimer()
    { 
        isTiming = false;
    }
}
