using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverManager : MonoBehaviour
{
    private bool[] leverStatus;
    private bool correctOrder;
    public bool isTriggered;
    [SerializeField] private int leverCount;

    // Start is called before the first frame update
    void Start()
    {
        leverStatus = new bool[leverCount];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void leverTurn(int n)
    {
        leverStatus[n] = true;
        if (checkOrder(n))
        {
            correctOrder = true;
        }
        else
        {
            correctOrder = false;
        }

        if (correctOrder && allTurned())
        {
            triggerEvent();
        }
        else if(!correctOrder && allTurned())
        {
            leverStatus = new bool[leverCount];
        }
    }

    public bool allTurned()
    {
        for(int i = 0;i < leverCount; i++)
        {
            if (!leverStatus[i])
            {
                return false;
            }
        }

        return true;
    }

    private void triggerEvent() 
    {
        // Insert event when all lever is triggered
        isTriggered = true;
        Debug.Log("All Lever Triggered Correctly");
    }

    private bool checkOrder(int n)
    {
        for(int i = 0;i < n; i++)
        {
            if(leverStatus[i] == false)
            {
                return false;
            }
        }
        return true;
    }
}
