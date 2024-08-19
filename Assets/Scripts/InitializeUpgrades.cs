using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeUpgrades : MonoBehaviour
{
    public void Initialize(UpgradeCreator[] upgrades, GameObject uiToSpawn, Transform spawnParent)
    {
        for (int i = 0; i < upgrades.Length; i++)
        {
            int currentIndex = i;

            GameObject go = Instantiate(uiToSpawn, spawnParent);

            //reset cost
            upgrades[currentIndex].currentUpgradeCost = upgrades[currentIndex].originalUpgradeCost;

            //set text
            UpgradeButtonReferences buttonRef = go.GetComponent<UpgradeButtonReferences>();
            buttonRef.upgradeButtonText.text = upgrades[currentIndex].upgradeButtonText;
            buttonRef.upgradeDescriptionText.SetText(upgrades[currentIndex].upgradeButtonDescription, upgrades[currentIndex].upgradeAmount);
            buttonRef.upgradeCostText.text = "Cost: " + upgrades[currentIndex].currentUpgradeCost;

            //set onclick
            buttonRef.upgradeButton.onClick.AddListener(delegate { GameManager.instance.OnUpgradeButtonClick(upgrades[currentIndex], buttonRef); });
        }
    }
}
