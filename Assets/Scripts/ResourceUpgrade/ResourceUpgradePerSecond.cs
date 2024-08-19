using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Resource Upgrade/ Resource Per Second", fileName = "Resource per Second")]

public class ResourceUpgradePerSecond : UpgradeCreator
{
    public override void ApplyUpgrade()
    {
        GameObject go = Instantiate(GameManager.instance.resourcePerSecondObjToSpawn, Vector3.zero, Quaternion.identity);
        go.GetComponent<ResourcePerSecondTimer>().resourcePerSecond = upgradeAmount;

        GameManager.instance.SimpleResourcePerSecondIncrease(upgradeAmount);
    }
}
