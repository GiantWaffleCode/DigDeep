using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DepthController : MonoBehaviour
{
    public GameObject world;
    public TextMeshProUGUI depthValue;


    void Update()
    {
        depthValue.text = MainMenuManager.depth.ToString();
    }

    public void AddDepth(int addToDepth)
    {
        MainMenuManager.depth += addToDepth;
    }
}
