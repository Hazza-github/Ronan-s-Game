using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

    [Space]
    public UpgradeCreator[] recreationalUpgrades;
    [SerializeField] private GameObject upgradeUIToSpawn;
    [SerializeField] private Transform upgradeUIParent;
    public GameObject moneyPerSecondObjToSpawn;

    [Space]
    public UpgradeCreator[] npcUpgrades;
    [SerializeField] private GameObject upgradeUIToSpawnNPC;
    [SerializeField] private Transform upgradeUIParentNPC;

    [Space]
    public UpgradeCreator[] resourceUpgrades;
    [SerializeField] private GameObject upgradeUIToSpawnResource;
    [SerializeField] private Transform upgradeUIParentResource;
    public GameObject resourcePerSecondObjToSpawn;

    [Space]
    public UpgradeCreator[] energyUpgrades;
    [SerializeField] private GameObject upgradeUIToSpawnEnergy;
    [SerializeField] private Transform upgradeUIParentEnergy;
    public GameObject energyPerSecondObjToSpawn;

    [Space]
    public double npcCount = 1;
    public double currentMoneyCount { get; set; }
    public double currentMoneyPerSecond { get; set; }

    public double currentResourceCount { get; set; }
    public double currentResourcePerSecond { get; set; }

    public double resourceBoost {  get; set; }
    //upgrades

    private InitializeUpgrades initializeUpgrades;
    private MoneyDisplay moneyDisplay;
    private MoneyDisplay resourceDisplay;

    [Space]
    //win stuff
    public int gameLevel = 1;
    [SerializeField] private Image backgroundObj;
    [SerializeField] private Sprite backgroundImage2;
    [SerializeField] private TextMeshProUGUI lvlText;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject lvlButton;
    private int lvlMultiplier = 100;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        currentMoneyCount = 100;
        resourceBoost = 1;
        lvlText.text = "Level 1: Office";

        moneyDisplay = GetComponent<MoneyDisplay>();
        resourceDisplay = GetComponent<MoneyDisplay>();

        upgradeCanvas.SetActive(false);
        mainGameCanvas.SetActive(true);

        initializeUpgrades = GetComponent<InitializeUpgrades>();
        initializeUpgrades.Initialize(recreationalUpgrades, upgradeUIToSpawn, upgradeUIParent);
        initializeUpgrades.Initialize(npcUpgrades, upgradeUIToSpawnNPC, upgradeUIParentNPC);
        initializeUpgrades.Initialize(resourceUpgrades, upgradeUIToSpawnResource, upgradeUIParentResource);
        initializeUpgrades.Initialize(energyUpgrades, upgradeUIToSpawnEnergy, upgradeUIParentEnergy);

        GameObject go = Instantiate(moneyPerSecondObjToSpawn, Vector3.zero, Quaternion.identity);
        go.GetComponent<MoneyPerSecondTimer>().moneyPerSecond = npcCount;
        SimpleMoneyPerSecondIncrease(npcCount);

        UpdateCurrentUI();
        UpdatePerSecondUI();
    }


    private void Update()
    {
        Debug.Log(Mathf.Pow(lvlMultiplier, gameLevel));

        if (currentResourceCount >= Mathf.Pow(lvlMultiplier, gameLevel))
        {
            lvlButton.SetActive(true);
        }
        if (currentResourceCount < Mathf.Pow(lvlMultiplier, gameLevel))
        {
            lvlButton.SetActive(false);
        }
    }

    #region UI Updates

    private void UpdateCurrentUI()
    {
        moneyDisplay.UpdateMoneyText(Mathf.RoundToInt((float)currentMoneyCount), moneyCountText, " £");
        resourceDisplay.UpdateMoneyText(Mathf.RoundToInt((float)currentResourceCount), resourceCountText, " r");
        npcCountText.text = npcCount.ToString() + " Workers";
    }

    private void UpdatePerSecondUI()
    {
        moneyDisplay.UpdateMoneyText(Mathf.RoundToInt((float)(currentMoneyPerSecond * npcCount)), moneyPerSecondText, " £/s");
        resourceDisplay.UpdateMoneyText(Mathf.RoundToInt((float)(currentResourcePerSecond * resourceBoost)), resourcePerSecondText, " r/s");
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
        currentMoneyCount += amount * npcCount;
        UpdateCurrentUI();
    }

    public void SimpleMoneyPerSecondIncrease(double amount)
    {
        currentMoneyPerSecond += amount;
        UpdatePerSecondUI();
    }

    public void SimpleResourceIncrease(double amount)
    {
        currentResourceCount += amount * resourceBoost;
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
            UpdatePerSecondUI();

            upgrade.currentUpgradeCost = Mathf.Round((float)(upgrade.currentUpgradeCost* (1 + upgrade.costIncreaseMultiplier)));

            buttonRef.upgradeCostText.text = "Cost: " + upgrade.currentUpgradeCost;
        }
    }

    public void OnLevelUpButtonClick()
    {
        currentResourceCount -= Mathf.Pow(lvlMultiplier, gameLevel);

        gameLevel++;

        switch(gameLevel)
        {
            case 1:
                lvlText.text = "Level 1: Office";
                break;

            case 2:
                lvlText.text = "Level 2: Building";
                break;
            case 3:
                lvlText.text = "Level 3: Town";
                break;
            case 4:
                lvlText.text = "Level 4: Country";
                break;
            case 5:
                lvlText.text = "Level 5: Planet";
                break;
            case 6:
                lvlText.text = "Level 6: Solar System";
                backgroundObj.sprite = backgroundImage2;
                break;
            case 7:
                lvlText.text = "Level 7: InterGalactic";
                break;
        }

        if (gameLevel == 8)
        {
            winScreen.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    #endregion
}