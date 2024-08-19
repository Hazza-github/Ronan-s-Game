using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePerSecondTimer : MonoBehaviour
{
    public float timerDuration = 1f;

    public double resourcePerSecond { get; set; }

    private float counter;

    private void Update()
    {
        counter += Time.deltaTime;

        if (counter >= timerDuration)
        {
            GameManager.instance.SimpleResourceIncrease(resourcePerSecond);

            counter = 0;
        }
    }
}
