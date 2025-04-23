using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]   
public class MovementEvent
{
    public float timer_max;
    private float timer;
    public int eventQuantity = -1;
    public bool runMethods;
    [SerializeField]
    public List<MovementActionType> methodsToRun = new List<MovementActionType>();
    public void ProcessEvent()
    {
        UpdateTimer();
        if (IsTimerEnded()) 
        {
            if (!DecreaseQuantity())
                return;
            if (eventQuantity != 0)
            {
                ResetTimer();
                runMethods = true;
            }
        }
    }

    void UpdateTimer()
    {
        timer -= Time.fixedDeltaTime;
    }

    bool IsTimerEnded()
    {
        if (timer < 0)
            return true;
        return false;
    }

    bool DecreaseQuantity()
    {
        if (eventQuantity != 0)
        {
            eventQuantity--;
            return true;
        }
        return false;   
    }

    bool ResetTimer()
    {
        timer = timer_max;
        return true;
    }
}
