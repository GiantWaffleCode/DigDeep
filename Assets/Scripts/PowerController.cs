using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PowerController : MonoBehaviour
{
    public Image powerBar;
    private float powerTime;
    private float rechargeAmmount;

    public bool isCharging;

    public GameObject deathPanel;
    public TextMeshProUGUI reasonText;

    private AudioManager audioManager;

    public Light reactorLight;

    private void Start()
    {
        powerBar.fillAmount = 1;
        powerTime = 20f;
        rechargeAmmount = .3f;
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Update()
    {
        if (powerBar.fillAmount > 0)
        {
            powerBar.fillAmount -= (Time.deltaTime) / (powerTime * MainMenuManager.batteryCap);
        }
        else
        {
            Time.timeScale = 0;
            audioManager.Stop("Engine");
            if (!audioManager.isPlaying("Death"))
            {
                audioManager.Stop("Theme");
                audioManager.Play("Death");
            }
            reasonText.text = "YOU RAN OUT OF POWER";
            deathPanel.SetActive(true);
        }

        if (isCharging)
        {
            if(!audioManager.isPlaying("Charging"))
            {
                audioManager.Play("Charging");
            }
            powerBar.fillAmount += rechargeAmmount * Time.deltaTime;
        }
        else
        {
            audioManager.Stop("Charging");
        }

        reactorLight.intensity = powerBar.fillAmount * 2f;
    }

    public void movePower()
    {
        powerBar.fillAmount -= (.02f / MainMenuManager.engineEfficency);
    }

    public void shootPower()
    {
        powerBar.fillAmount -= (.1f / MainMenuManager.lazerEfficency);
    }
}
