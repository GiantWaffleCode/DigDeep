using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyController : MonoBehaviour
{
    public TextMeshProUGUI moneyValue;

    private AudioManager audioManager;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        moneyValue.text = Mathf.Round(MainMenuManager.money).ToString();
    }

    public void AddBlockValue(GameObject brokenBlock)
    {
        switch (brokenBlock.name)
        {
            case "Dark Cube(Clone)":
                MainMenuManager.money += 2f * (MainMenuManager.oreRefineryEfficency);
                break;
            case "Light Cube(Clone)":
                MainMenuManager.money += 1f * (MainMenuManager.oreRefineryEfficency);
                break;
            case "Iron Ore(Clone)":
                audioManager.Play("OreBreak");
                MainMenuManager.money += 10f * (MainMenuManager.oreRefineryEfficency);
                break;
            case "Gold Ore(Clone)":
                audioManager.Play("OreBreak");
                MainMenuManager.money += 50f * (MainMenuManager.oreRefineryEfficency);
                break;
            default:
                break;
        }
    }
}
