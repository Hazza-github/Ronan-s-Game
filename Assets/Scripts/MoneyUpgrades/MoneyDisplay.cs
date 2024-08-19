using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyDisplay : MonoBehaviour
{
    public void UpdateMoneyText(double moneyCount, TextMeshProUGUI textToChange, string optionalEndText = null)
    {
        string[] suffixes = { "", "k", "M", "B", "T", "Q" };
        int index = 0;

        while (moneyCount >= 1000 && index < suffixes.Length - 1)
        {
            moneyCount /= 1000;
            index++;

            if(index >= suffixes.Length - 1 && moneyCount >= 1000)
            {
                break;
            }
        }

        string formattedText;

        if (index == 0)
        {
            formattedText = moneyCount.ToString();
        }
        else
        {
            formattedText = moneyCount.ToString("F1") + suffixes[index];
        }

        textToChange.text = formattedText + optionalEndText;
    }
}
