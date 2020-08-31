using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandCharge : MonoBehaviour
{
    //public float ChargeTime;
    public float SmackTime;

    private float currentTime;
    // uses int instead of bool because there are two hands calling it
    public int charged { get; set; }

    void Update()
    {
        UpdateCharged();
    }

    private void UpdateCharged()
    {
        if (charged > 0)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                currentTime = 0;
                charged = 0;
            }
        }

        else if (handsAreOpen())
        {
            //currentTime += Time.deltaTime;
            //if (currentTime >= ChargeTime)
            //{
                currentTime = SmackTime;
                charged = 2;
            //}
        }
    }

    public void ResetCharge()
    {
        --charged;
        currentTime = 0f;
    }

    private bool handsAreOpen()
    {
        return Input.GetAxisRaw("LeftHorizontal") == -1 && Input.GetAxisRaw("RightHorizontal") == 1;
    }

    private bool handsAreClosed()
    {
        return Input.GetAxisRaw("LeftHorizontal") == 1 && Input.GetAxisRaw("RightHorizontal") == -1;
    }
}
