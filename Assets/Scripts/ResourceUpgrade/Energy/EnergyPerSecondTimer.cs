using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPerSecondTimer : MonoBehaviour
{
    public float timerDuration = 1f;

    public double boostAmount { get; set; }

    private float counter;

    private void Update()
    {
        counter += Time.deltaTime;

        if (counter >= timerDuration)
        {
            //GameManager.instance.resourceBoost = boostAmount;
            counter = 0;
        }
    }
}
