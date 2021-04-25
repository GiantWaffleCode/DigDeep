using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject shipObject;
    public GameObject heatObject;
    public GameObject powerObject;
    public GameObject cameraObject;
    public GameObject lazerObject;
    public GameObject playerObject;
    private ShipController shipController;
    private HeatController heatController;
    private PowerController powerController;

    public float cameraZoomOffset;
    public float cameraVerticalOffset;
    public float cameraSpeed;

    public GameObject buttonPower;

    private AudioManager audioManager;

    public Camera cam;

    const string LAZER_FIRE_ANIMATION = "ButtonLazerPress";
    const string HEAT_ANIMATION = "HeatHandleAnimation";
    const string LEVER_LEFT_ANIMATION = "LeverRotationLeft";
    const string LEVER_RIGHT_ANIMATION = "LeverRotationRight";

    private void Start()
    {
        shipController = shipObject.GetComponent<ShipController>();
        heatController = heatObject.GetComponent<HeatController>();
        powerController = powerObject.GetComponent<PowerController>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Update()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hit))
        {
            GameObject objectHit = hit.collider.gameObject;

            if (objectHit.GetComponent<Animator>() != null)
            {
                if ((hit.collider.name == "Lazer Button" ) && (objectHit.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle")) && (Vector3.Distance(hit.collider.transform.position, playerObject.transform.position) < 1f))
                {
                        objectHit.GetComponent<Animator>().Play(LAZER_FIRE_ANIMATION); //Run Lazer Button Animation
                        //Debug.Log("Hit: " + hit.collider.name);
                        shipController.MoveShip(); //Move ship in direction of lazer
                }
                else if (hit.collider.name == "Lever Pivot" && (Vector3.Distance(hit.collider.transform.position, playerObject.transform.position) < 1f))
                {
                    if (hit.point.x > objectHit.transform.position.x) //Right Side
                    {
                        hit.collider.gameObject.GetComponent<Animator>().Play(LEVER_RIGHT_ANIMATION);
                        int currentLaserState = lazerObject.GetComponent<Animator>().GetInteger("lazerState");
                        if (currentLaserState < 1)
                        {
                            audioManager.Play("LazerMove");
                            lazerObject.GetComponent<Animator>().SetInteger("lazerState", (currentLaserState + 1));
                        }
                    }
                    else if (hit.point.x < objectHit.transform.position.x)  //Left Side
                    {
                        hit.collider.gameObject.GetComponent<Animator>().Play(LEVER_LEFT_ANIMATION);
                        int currentLaserState = lazerObject.GetComponent<Animator>().GetInteger("lazerState");
                        if (currentLaserState > -1)
                        {
                            audioManager.Play("LazerMove");
                            lazerObject.GetComponent<Animator>().SetInteger("lazerState", (currentLaserState - 1));
                        }
                    }
                }
                else if (hit.collider.name == "HeatPumpParent" && (Vector3.Distance(hit.collider.transform.position, playerObject.transform.position) < 1f)) //Vent Heat
                {
                    if (objectHit.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                    {
                        objectHit.GetComponent<Animator>().Play(HEAT_ANIMATION);
                        heatController.VentHeat();
                    }
                }
                else if (hit.collider.name == "Power Handle" && (Vector3.Distance(hit.collider.transform.position, playerObject.transform.position) < 1f)) //Charge Reactor
                {
                    //Debug.Log(Vector3.Distance(hit.collider.transform.position, playerObject.transform.position));
                    if (objectHit.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                    {

                        objectHit.GetComponent<Animator>().SetBool("isPressed", true);
                        powerController.isCharging = true;
                    }
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            buttonPower.GetComponent<Animator>().SetBool("isPressed", false);
            powerController.isCharging = false;
        }

    }
}
