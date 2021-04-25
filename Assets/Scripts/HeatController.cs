using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeatController : MonoBehaviour
{
    public Image heatBar;
    private float depthMultiplier;
    private float heatTime;
    private float heatPumpAmmount;

    public ParticleSystem ventSteam;

    public GameObject deathPanel;
    public TextMeshProUGUI reasonText;

    private AudioManager audioManager;

    private void Start()
    {
        heatBar.fillAmount = 0;
        heatTime = 30;
        heatPumpAmmount = .15f;
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Update()
    {
        depthMultiplier = 1 + (MainMenuManager.depth * .02f); //CHANGE to ratio based on depth of ship. Should get harder the lower we go.

        if (heatBar.fillAmount < 1)
        {
            heatBar.fillAmount += (Time.deltaTime * depthMultiplier) / (heatTime * MainMenuManager.hullEfficency);
        }
        else
        {
            audioManager.Stop("Engine");
            if (!audioManager.isPlaying("Death"))
            {
                audioManager.Stop("Theme");
                audioManager.Play("Death");
            }
            Time.timeScale = 0;
            reasonText.text = "YOU OVERHEATED";
            deathPanel.SetActive(true);
        }
    }

    public void VentHeat()
    {
        audioManager.Play("Venting");
        ventSteam.Play();
        heatBar.fillAmount -= heatPumpAmmount * (MainMenuManager.heatPumpEfficency);
    }
}
