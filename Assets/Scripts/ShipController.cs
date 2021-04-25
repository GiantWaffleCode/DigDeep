using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{

    public GameObject shipObject;
    public GameObject worldObject;
    public GameObject lazerObject;
    public GameObject moneyObject;
    public DepthController depthController;
    public GameController gameController;
    private MoneyController moneyController;
    public PowerController powerController;

    public GameObject winPanel;

    public ParticleSystem lazerParts;

    private AudioManager audioManager;

    public float moveSpeed;

    public float blockSize;

    private int currentCol;

    private Vector3 targetLocation;

    private LayerMask shipMask;

    private void Awake()
    {
        worldObject.transform.position = new Vector3(0, 0, 0);
    }
    void Start()
    {
        blockSize = 5.65f;
        worldObject.transform.position = new Vector3(.66f, -.8f + 10f, .306f);
        blockSize = 5.65f;
        targetLocation = worldObject.transform.position;
        moveSpeed = 5f;
        shipMask = LayerMask.GetMask("Buttons");
        moneyController = moneyObject.GetComponent<MoneyController>();
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.Play("Engine");

    }

    void Update()
    {
        //Debug.Log(currentCol);
    }

    public void MoveShip()
    {
        int lazerState = lazerObject.GetComponent<Animator>().GetInteger("lazerState");
        if (lazerState == 1) //Right
        {
            
            //Debug.Log("Right");
            RaycastHit hit;
            if (Physics.Raycast(lazerObject.transform.position, new Vector3(1, 0, 0), out hit, 5f))
            {
                if (!hit.collider.gameObject.CompareTag("Unbreakable"))
                {
                    if (!hit.collider.gameObject.CompareTag("Diamond"))
                    {
                        audioManager.Play("LazerShoot");
                        lazerParts.Play();
                        Destroy(hit.collider.gameObject);
                        moneyController.AddBlockValue(hit.collider.gameObject);
                        powerController.shootPower();
                    }
                    else
                    {
                        Time.timeScale = 0;
                        audioManager.Stop("Engine");
                        winPanel.SetActive(true);
                    }
                }
            }
            if (currentCol < 6)
            {
                if (hit.collider == null || !hit.collider.gameObject.CompareTag("Unbreakable"))
                {
                    audioManager.Play("Move");
                    currentCol += 1;
                    gameController.ShiftLayer(-1);
                    powerController.movePower();
                }
            }
        }
        else if (lazerState == -1) //Left
        {
            //Debug.Log("Left");
            RaycastHit hit;
            if (Physics.Raycast(lazerObject.transform.position, new Vector3(-1, 0, 0), out hit, 5f))
            {
                if (!hit.collider.gameObject.CompareTag("Unbreakable"))
                {
                    if (!hit.collider.gameObject.CompareTag("Diamond"))
                    {
                        audioManager.Play("LazerShoot");
                        lazerParts.Play();
                        Destroy(hit.collider.gameObject);
                        moneyController.AddBlockValue(hit.collider.gameObject);
                        powerController.shootPower();
                    }
                    else
                    {
                        Time.timeScale = 0;
                        audioManager.Stop("Engine");
                        winPanel.SetActive(true);
                    }
                }
            }
            if (currentCol > -6)
            {
                if (hit.collider == null || !hit.collider.gameObject.CompareTag("Unbreakable"))
                {
                    audioManager.Play("Move");
                    currentCol -= 1;
                    gameController.ShiftLayer(1);
                    powerController.movePower();
                }
            }
        }
        else if (lazerState == 0) //Down
        {
            //Debug.Log("Down");
            RaycastHit hit;
            if (Physics.Raycast(lazerObject.transform.position, new Vector3(0, -1, 0), out hit, 5f))
            {
                if (!hit.collider.gameObject.CompareTag("Unbreakable"))
                {
                    if (!hit.collider.gameObject.CompareTag("Diamond"))
                    {
                        audioManager.Play("Move");
                        audioManager.Play("LazerShoot");
                        lazerParts.Play();
                        Destroy(hit.collider.gameObject);
                        gameController.AddLayer(currentCol);
                        moneyController.AddBlockValue(hit.collider.gameObject);
                        powerController.shootPower();
                        depthController.AddDepth(1);
                    }
                    else
                    {
                        Time.timeScale = 0;
                        audioManager.Stop("Engine");
                        winPanel.SetActive(true);
                    }
                }
            }
        }
    }

}
