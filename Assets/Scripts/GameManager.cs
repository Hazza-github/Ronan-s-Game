using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject mainGameCanvas;
    [SerializeField] private GameObject upgradeCanvas;

    [SerializeField] private TextMeshProUGUI moneyCountText;
    [SerializeField] private TextMeshProUGUI moneyPerSecondText;
    [SerializeField] private TextMeshProUGUI resourceCountText;
    [SerializeField] private TextMeshProUGUI resourcePerSecondText;
    [SerializeField] private TextMeshProUGUI npcCountText;

    [SerializeField] private GameObject backgroundObj;

    [Space]
    public UpgradeCreator[] recreationalUpgrades;
    [SerializeField] private GameObject upgradeUIToSpawn;
    [SerializeField] private Transform upgradeUIParent;
    public GameObject moneyPerSecondObjToSpawn;

    [Space]
    public UpgradeCreator[] resourceUpgrades;
    [SerializeField] private GameObject upgradeUIToSpawnResource;
    [SerializeField] private Transform upgradeUIParentResource;
    public GameObject resourcePerSecondObjToSpawn;

    [Space]
    public double npcCount = 1;
    public double currentMoneyCount { get; set; }
    public double currentMoneyPerSecond { get; set; }

    public double currentResourceCount { get; set; }
    public double currentResourcePerSecond { get; set; }

    //upgrades

    public int gameLevel = 1;

    private InitializeUpgrades initializeUpgrades;
    private MoneyDisplay moneyDisplay;
    private MoneyDisplay resourceDisplay;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        currentMoneyCount = 10;
        currentResourceCount = 10;

        moneyDisplay = GetComponent<MoneyDisplay>();
        resourceDisplay = GetComponent<MoneyDisplay>();

        UpdateCurrentUI();
        UpdatePerSecondUI();

        upgradeCanvas.SetActive(false);
        mainGameCanvas.SetActive(true);

        initializeUpgrades = GetComponent<InitializeUpgrades>();
        initializeUpgrades.Initialize(recreationalUpgrades, upgradeUIToSpawn, upgradeUIParent);
        initializeUpgrades.Initialize(resourceUpgrades, upgradeUIToSpawnResource, upgradeUIParentResource);
    }

    #region UI Updates

    private void UpdateCurrentUI()
    {
        moneyDisplay.UpdateMoneyText(currentMoneyCount, moneyCountText);
        resourceDisplay.UpdateMoneyText(currentResourceCount, resourceCountText);
    }

    private void UpdatePerSecondUI()
    {
        moneyDisplay.UpdateMoneyText(currentMoneyPerSecond, moneyPerSecondText, "/s");
        resourceDisplay.UpdateMoneyText(currentResourcePerSecond, resourcePerSecondText, "/s");

    }

    #endregion

    #region Button Presses

    public void OnUpgradeButtonPress()
    {
        mainGameCanvas.SetActive(false);
        upgradeCanvas.SetActive(true);
    }

    public void OnCloseButtonPress()
    {
        mainGameCanvas.SetActive(true);
        upgradeCanvas.SetActive(false);
    }

    #endregion

    #region Simple Increase
    public void SimpleMoneyIncrease(double amount)
    {
        currentMoneyCount += amount;
        UpdateCurrentUI();
    }

    public void SimpleMoneyPerSecondIncrease(double amount)
    {
        currentMoneyPerSecond += amount;
        UpdatePerSecondUI();
    }

    public void SimpleResourceIncrease(double amount)
    {
        currentResourceCount += amount * npcCount;
        UpdateCurrentUI();
    }

    public void SimpleResourcePerSecondIncrease(double amount)
    {
        currentResourcePerSecond += amount;
        UpdatePerSecondUI();
    }


    #endregion

    #region Events
    public void OnUpgradeButtonClick(UpgradeCreator upgrade, UpgradeButtonReferences buttonRef)
    {
        if (currentMoneyCount >= upgrade.currentUpgradeCost && gameLevel >= upgrade.unlockLevel)
        {
            upgrade.ApplyUpgrade();

            currentMoneyCount -= upgrade.currentUpgradeCost;
            UpdateCurrentUI();

            upgrade.currentUpgradeCost = Mathf.Round((float)(upgrade.currentUpgradeCost* (1 + upgrade.costIncreaseMultiplier)));

            buttonRef.upgradeCostText.text = "Cost: " + upgrade.currentUpgradeCost;
        }
    }

    #endregion
}