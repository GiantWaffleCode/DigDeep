using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerController : MonoBehaviour
{
    public GameObject lazerRing;

    public GameObject lazerParts;

    private Vector3 rightPos;
    private Vector3 centerPos;
    private Vector3 leftPos;

    private float targetRot;

    public int currentPos;
    public float rotSpeed;

    private void Start()
    {
        rightPos = new Vector3(90f, -90f, 90f);
        centerPos = new Vector3(0f, -90f, 90f);
        leftPos = new Vector3(-90f, -90f, 90f);

        rotSpeed = 5f;

        currentPos = 0; // -1 = Left    0 = Center   1 = Right
    }

    public void rotateRingLeft()
    {
        Debug.Log("Rotate Left");
        Debug.Log(currentPos);
        if (currentPos > -1)
        {
            currentPos -= 1;
            targetRot = currentPos * 90f;
        }
    }

    public void rotateRingRight()
    {
        //Debug.Log("Rotate Right");
        //Debug.Log(currentPos);
        if (currentPos < 1)
        {
            currentPos += 1;
            targetRot = currentPos * 90f;
        }
    }

    private void Update()
    {
        //Debug.Log(lazerRing.transform.localEulerAngles.x);
        //Debug.Log(targetRot);
        
        lazerRing.transform.rotation = Quaternion.Euler(
            Mathf.LerpAngle(lazerRing.transform.localEulerAngles.x, targetRot, rotSpeed * Time.deltaTime),
            -90f,
            90f);
    }

}
