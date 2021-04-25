using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject worldGeneratorObject;
    private WorldGenerator worldGenerator;

    private List<GameObject> layerList;

    private List<Vector3> targetLocList;

    private AudioManager audioManager;

    private Vector3 targetLoc;

    private float blockSize;
    private float direction;
    private float lerpTime;

    private void Start()
    {
        blockSize = 5.65f;
        lerpTime = 5f;
        worldGenerator = worldGeneratorObject.GetComponent<WorldGenerator>();
        layerList = new List<GameObject>();
        targetLocList = new List<Vector3>();
        AddLayer(0);
        AddLayer(0);
        AddLayer(0);
        MainMenuManager.depth = 0;
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < layerList.Count; i++)
        {
            layerList[i].transform.position = Vector3.Lerp(layerList[i].transform.position, targetLocList[i], lerpTime * Time.deltaTime);
        }
    }

    public void AddLayer(int currentCol)
    {
        layerList.Insert(0, worldGenerator.GenerateLayer());

        targetLocList.Insert(0, new Vector3(
            layerList[0].transform.position.x - (currentCol * blockSize),
            layerList[0].transform.position.y,
            layerList[0].transform.position.z));
        

        for (int i = 0; i < layerList.Count; i++)
        {
            targetLocList[i] = new Vector3(
                targetLocList[i].x,
                targetLocList[i].y + blockSize,
                targetLocList[i].z);
        }

        if (layerList.Count > 6)
        {
            Destroy(layerList[6].gameObject);
            layerList.RemoveAt(6);
        }
    }

    public void ShiftLayer(int direction)
    {
        for (int i = 0; i < layerList.Count; i++)
        {
            targetLocList[i] = new Vector3(
                targetLocList[i].x + (direction * blockSize),
                targetLocList[i].y,
                targetLocList[i].z);
            //layerList[i].transform.position = Vector3.MoveTowards(layerList[i].transform.position, targetloc, lerpTime * Time.deltaTime);
            //layerList[i].transform.position = targetloc;
        }
    }

    public void LoadMenu()
    {
        //if (audioManager.isPlaying("Death"))
        //{
        //    audioManager.Stop("Death");
        //}
        audioManager.Play("Theme");
        SceneManager.LoadScene(0);
    }
}
