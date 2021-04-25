using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    public GameObject cubeLight;
    public GameObject cubeDark;
    public GameObject background;
    public GameObject ironOre;
    public GameObject goldOre;
    public GameObject diamondOre;
    public GameObject worldParent;

    public GameObject worldLayerClone;
    private Vector3 row3Cords;
    private Vector3 row1Cords;

    public int worldWidth;
    public int worldHeight;

    public int layerNumber;
    private GameObject layerClone;

    private float frontZ;
    private float backZ;

    public float blockSize;

    private void Awake()
    {
        layerNumber = 1;
        worldHeight = 7;
        worldWidth = 15;

        blockSize = 5.65f;

        frontZ = .306f;
        backZ = .306f + blockSize;

        row3Cords = new Vector3(.66f, -.8f, .306f);
        row1Cords = new Vector3(row3Cords.x - (blockSize * ((worldWidth - 1) / 2)), row3Cords.y - (3 * blockSize), frontZ);
        //GenerateLayer();
    }

    public GameObject GenerateLayer()
    {
        GameObject layerClone = (GameObject)Instantiate(worldLayerClone, row1Cords, Quaternion.identity);
        for (int x = 0; x < worldWidth; x++) //Create Front Blocks
        {
            if (x == 0 || x == (worldWidth - 1))
            {
                spawnUnbreakable(x, layerClone);
            }
            if (x > 0 && x < worldWidth - 1)
            {
                int randomGen = Random.Range(0, 100);
                if (MainMenuManager.depth >= 0 && MainMenuManager.depth <= 50)
                {
                    if (randomGen < 90) spawnDirt(x, layerClone);
                    else if (randomGen < 95) spawnUnbreakable(x, layerClone);
                    else spawnIronOre(x, layerClone);
                }
                else if (MainMenuManager.depth > 50 && MainMenuManager.depth <= 150)
                {
                    if (randomGen < 80) spawnDirt(x, layerClone);
                    else if (randomGen < 90) spawnUnbreakable(x, layerClone);
                    else if (randomGen < 97) spawnIronOre(x, layerClone);
                    else spawnGoldOre(x, layerClone);
                }
                else if (MainMenuManager.depth > 150 && MainMenuManager.depth <= 350)
                {
                    if (randomGen < 70) spawnDirt(x, layerClone);
                    else if (randomGen < 85) spawnUnbreakable(x, layerClone);
                    else if (randomGen < 95) spawnIronOre(x, layerClone);
                    else spawnGoldOre(x, layerClone);
                }
                else if (MainMenuManager.depth > 350)
                {
                    if (randomGen < 10) spawnDirt(x, layerClone);
                    else if (randomGen < 15) spawnUnbreakable(x, layerClone);
                    else if (randomGen < 20) spawnIronOre(x, layerClone);
                    else if (randomGen < 30) spawnGoldOre(x, layerClone);
                    else spawnDiamondOre(x, layerClone);
                }
            }
        }
        for (int x = 0; x < worldWidth; x++) //Create Back Blocks
        {
            spawnBG(x, layerClone);
        }
        //layerClone.name = "World Layer: " + layerNumber.ToString();
        return layerClone;
    }

    private void spawnDarkDirt(int x, GameObject layerClone)
    {
        Instantiate(cubeDark, new Vector3(
                        layerClone.transform.localPosition.x + (x * blockSize),
                        row1Cords.y,
                        frontZ), Quaternion.identity, layerClone.transform);
    }

    private void spawnLightDirt(int x, GameObject layerClone)
    {
        Instantiate(cubeLight, new Vector3(
                       layerClone.transform.localPosition.x + (x * blockSize),
                       row1Cords.y,
                       frontZ), Quaternion.identity, layerClone.transform);
    }
    private void spawnUnbreakable(int x, GameObject layerClone)
    {
        Instantiate(background, new Vector3(
                   layerClone.transform.localPosition.x + (x * blockSize),
                   row1Cords.y,
                   frontZ), Quaternion.identity, layerClone.transform);
    }
    private void spawnIronOre(int x, GameObject layerClone)
    {
        Instantiate(ironOre, new Vector3(
                   layerClone.transform.localPosition.x + (x * blockSize),
                   row1Cords.y,
                   frontZ), Quaternion.identity, layerClone.transform);
    }
    private void spawnGoldOre(int x, GameObject layerClone)
    {
        Instantiate(goldOre, new Vector3(
                   layerClone.transform.localPosition.x + (x * blockSize),
                   row1Cords.y,
                   frontZ), Quaternion.identity, layerClone.transform);
    }
    private void spawnDiamondOre(int x, GameObject layerClone)
    {
        Instantiate(diamondOre, new Vector3(
                   layerClone.transform.localPosition.x + (x * blockSize),
                   row1Cords.y,
                   frontZ), Quaternion.identity, layerClone.transform);
    }
    private void spawnBG(int x, GameObject layerClone)
    {
        Instantiate(background, new Vector3(
                layerClone.transform.localPosition.x + (x * blockSize),
                row1Cords.y,
                backZ), Quaternion.identity, layerClone.transform);
    }
    private void spawnDirt(int x, GameObject layerClone)
    {
        if (Random.Range(0, 2) == 0)
        {
            spawnDarkDirt(x, layerClone);
        }
        else
        {
            spawnLightDirt(x, layerClone);
        }
    }
}