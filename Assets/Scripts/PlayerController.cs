using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController characterController;
    private Vector3 playerVelocity;
    private float horizontalMovement;
    public float speed;
    private float jumpInput;
    public float jumpHeight;
    public float gravityValue;
    private bool groundedPlayer;

    private void Start()
    {
        speed = 5f;
        jumpHeight = 5f;
        gravityValue = -9.8f;
    }

    private void Update()
    {
        groundedPlayer = characterController.isGrounded;

        if (groundedPlayer)
        {
            //Debug.Log("Im Grounded");
        }

        if (groundedPlayer && characterController.velocity.y < .0001)
        {
            playerVelocity.y = 0f;
            //Debug.Log("Setting Velocity to 0");
        }

        horizontalMovement = Input.GetAxis("Horizontal") * speed;
        jumpInput = Input.GetAxis("Jump");
        //Debug.Log(jumpInput);
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        characterController.Move(move * Time.deltaTime * speed);

        if ((jumpInput > 0) && groundedPlayer)
        {
            playerVelocity.y = jumpHeight;
            //Debug.Log("JUMP");
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -0.089f);
    }
}
