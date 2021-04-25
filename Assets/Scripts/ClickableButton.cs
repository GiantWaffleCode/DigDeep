using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableButton : MonoBehaviour
{
    public Camera cam;
    public GameObject shipObject;
    private ShipController shipController;

    const string BUTTON_ANIMATION = "Button press anim";

    private void Start()
    {
        shipController = shipObject.GetComponent<ShipController>();
    }

    private void Update()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hit))
        {
            GameObject objectHit = hit.collider.gameObject;
            objectHit.GetComponent<Animator>().Play(BUTTON_ANIMATION);
            Debug.Log("Move the Ship!");
            //shipController.MoveShip();
        }
    }
}
