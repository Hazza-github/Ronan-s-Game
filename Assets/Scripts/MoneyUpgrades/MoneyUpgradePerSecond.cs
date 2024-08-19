using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Recreational Upgrade/ Money Per Second", fileName = "Money per Second")]

public class MoneyUpgradePerSecond : UpgradeCreator
{
    public override void ApplyUpgrade()
    {
        GameObject go = Instantiate(GameManager.instance.moneyPerSecondObjToSpawn, Vector3.zero, Quaternion.identity);
        go.GetComponent<MoneyPerSecondTimer>().moneyPerSecond = upgradeAmount;

        GameManager.instance.SimpleMoneyPerSecondIncrease(upgradeAmount);
    }
}
