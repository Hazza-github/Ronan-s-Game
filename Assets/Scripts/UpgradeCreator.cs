using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UpgradeCreator : ScriptableObject
{
    public float upgradeAmount = 1f;

    public double originalUpgradeCost = 100;
    public double currentUpgradeCost = 100;
    public double costIncreaseMultiplier = 0.05f;
    public int unlockLevel;

    public string upgradeButtonText;
    [TextArea (3,10)]
    public string upgradeButtonDescription;

    public abstract void ApplyUpgrade();

    private void OnValidate()
    {
        currentUpgradeCost = originalUpgradeCost;
    }
}
