using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    public static float hullMulti;
    public static float heatTime;
    public static float heatPump;

    public static float depth;
    public static float money;

    public static float battMulti;
    public static float rechargeMulti;


    public static float hullEfficency;
    public static float engineEfficency;
    public static float lazerEfficency;
    public static float oreRefineryEfficency;
    public static float heatPumpEfficency;
    public static float batteryCap;

    public GameObject storePanel;
    public GameObject howToPanel;

    private static int hullLevel;
    private static int batteryLevel;
    private static int heatPumpLevel;
    private static int laserLevel;
    private static int oreRefineryLevel;

    public TextMeshProUGUI hullLevelText;
    public TextMeshProUGUI batteryLevelText;
    public TextMeshProUGUI heatPumpLevelText;
    public TextMeshProUGUI laserLevelText;
    public TextMeshProUGUI oreRefineryLevelText;

    public TextMeshProUGUI hullLevelMoney;
    public TextMeshProUGUI batteryLevelMoney;
    public TextMeshProUGUI heatPumpLevelMoney;
    public TextMeshProUGUI laserLevelMoney;
    public TextMeshProUGUI oreRefineryLevelMoney;

    public GameObject hullLevelButton;
    public GameObject batteryLevelButton;
    public GameObject heatPumpLevelButton;
    public GameObject laserLevelButton;
    public GameObject oreRefineryLevelButton;

    public TextMeshProUGUI totalMoney;

    void Awake()
    {
        //DontDestroyOnLoad(this.gameObject);
        //money = 10000;
        Time.timeScale = 1;

        hullLevelText.text = (hullLevel.ToString() + "/5");
        batteryLevelText.text = (batteryLevel.ToString() + "/5");
        heatPumpLevelText.text = (heatPumpLevel.ToString() + "/5");
        laserLevelText.text = (laserLevel.ToString() + "/5");
        oreRefineryLevelText.text = (oreRefineryLevel.ToString() + "/5");

        SetCost(hullLevel, hullLevelMoney, hullLevelButton);
        SetCost(batteryLevel, batteryLevelMoney, batteryLevelButton);
        SetCost(heatPumpLevel, heatPumpLevelMoney, heatPumpLevelButton);
        SetCost(laserLevel, laserLevelMoney, laserLevelButton);
        SetCost(oreRefineryLevel, oreRefineryLevelMoney, oreRefineryLevelButton);
    }

    private void Update()
    {

        if(totalMoney != null)
        {
            totalMoney.text = "MONEY: $" + Mathf.Floor(money).ToString();
        }

        hullEfficency = (1f + (hullLevel * .2f));
        lazerEfficency = (1f + (laserLevel * .2f));
        batteryCap = (1f + (batteryLevel * .2f));
        heatPumpEfficency = (1f + (heatPumpLevel * .2f));
        oreRefineryEfficency = (1f + (oreRefineryLevel * .2f));
        engineEfficency = 1f;

    }

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }

    public void toggleStoreShown()
    {
        storePanel.SetActive(!storePanel.activeSelf);
    }

    public void toggleHowToShown()
    {
        howToPanel.SetActive(!howToPanel.activeSelf);
    }

    public void UpgradeHull()
    {
        if(hullLevel < 5 && tryToBuy(hullLevel))
        {
            hullLevel++;
            hullLevelText.text = (hullLevel.ToString() + "/5");
            SetCost(hullLevel, hullLevelMoney, hullLevelButton);
        }
    }
    public void UpgradeBattery()
    {
        if(batteryLevel < 5 && tryToBuy(batteryLevel))
        {
            batteryLevel++;
            batteryLevelText.text = (batteryLevel.ToString() + "/5");
            SetCost(batteryLevel, batteryLevelMoney, batteryLevelButton);
        }
    }

    public void UpgradeHeatPump()
    {
        if (heatPumpLevel < 5 && tryToBuy(heatPumpLevel))
        {
            heatPumpLevel++;
            heatPumpLevelText.text = (heatPumpLevel.ToString() + "/5");
            SetCost(heatPumpLevel, heatPumpLevelMoney, heatPumpLevelButton);
        }
    }

    public void UpgradeLaser()
    {
        if (laserLevel < 5 && tryToBuy(laserLevel))
        {
            laserLevel++;
            laserLevelText.text = (laserLevel.ToString() + "/5");
            SetCost(laserLevel, laserLevelMoney, laserLevelButton);
        }
    }

    public void UpgradeOreRefinery()
    {
        if (oreRefineryLevel < 5 && tryToBuy(oreRefineryLevel))
        {
            oreRefineryLevel++;
            oreRefineryLevelText.text = (oreRefineryLevel.ToString() + "/5");
            SetCost(oreRefineryLevel, oreRefineryLevelMoney, oreRefineryLevelButton);
        }
    }
    private bool tryToBuy(int level)
    {
        int cost = (20 + (level * 40) + (level * level * 20));
        if (money >= cost)
        {
            money -= cost;
            return true;
        }
        else
        {
            return false;
        }
    }

    private void SetCost(int level, TextMeshProUGUI levelCostText, GameObject button)
    {
        if (level != 5)
        {
            levelCostText.text = "$" + (20 + (level * 40) + (level * level * 20));
        }
        else
        {
            levelCostText.text = "MAX";
            button.SetActive(false);
        }
    }

    public void Exit()
    {
        Application.Quit();
    }
}
