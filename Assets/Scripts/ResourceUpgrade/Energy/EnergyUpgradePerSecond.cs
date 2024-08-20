using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Energy Upgrade/ Energy Per Second", fileName = "Energy per Second")]

public class EnergyUpgradePerSecond : UpgradeCreator
{
    public override void ApplyUpgrade()
    {
        GameObject go = Instantiate(GameManager.instance.energyPerSecondObjToSpawn, Vector3.zero, Quaternion.identity);
        go.GetComponent<EnergyPerSecondTimer>().boostAmount += upgradeAmount;

        GameManager.instance.resourceBoost += upgradeAmount;
    }
}
