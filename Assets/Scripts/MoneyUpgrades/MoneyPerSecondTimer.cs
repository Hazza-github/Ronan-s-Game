using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyPerSecondTimer : MonoBehaviour
{
    public float timerDuration = 1f;

    public double moneyPerSecond { get; set; }

    private float counter;

    private void Update()
    {
        counter += Time.deltaTime;

        if (counter >= timerDuration)
        {
            GameManager.instance.SimpleMoneyIncrease(moneyPerSecond);

            counter = 0;
        }
    }
}
