using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderController : MonoBehaviour
{
    

    public float horizontalMove;
    public float verticalMove;

    public Vector3 playerInput;

    public CharacterController player;
    public float speed;
    public float gravity;
    public float fallVelocity;
    public float jumpForce;

    public Camera mainCamera;
    public Vector3 camForward;
    public Vector3 camRight;
    public Vector3 movePlayer;



    void Start()
    {
        player = GetComponent<CharacterController>();
        //gravity = 9.81f;
        jumpForce = 7;

    }


    void Update()
    {
        IsGrounded();

        

        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        playerInput = new Vector3(horizontalMove, 0, verticalMove);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);

        camDirection();

        movePlayer = playerInput.x * camRight + playerInput.z * camForward;

        movePlayer = movePlayer * speed;

        player.transform.LookAt(player.transform.position + movePlayer);

        SetGravity();

        PlayerSkills();

        player.Move(movePlayer * Time.deltaTime);
        Debug.Log(player.velocity.magnitude);
    }

    void IsGrounded()
    {
        if (player.isGrounded)
        {
            Debug.Log("El jugador está en el suelo");
            // Hacer algo si el jugador está en el suelo
        }
        else
        {
            Debug.Log("El jugador está en el aire");
            // Hacer algo si el jugador está en el aire
        }
    }

    //Funcion de direccion de camara
    void camDirection()
    {
        camForward = mainCamera.transform.forward;
        camRight = mainCamera.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward = camForward.normalized;
        camRight = camRight.normalized;

    }



    // Funcion de habilidades

    public void PlayerSkills()
    {

        if (Input.GetButtonDown("Jump"))
        {
            fallVelocity = jumpForce;
            movePlayer.y = fallVelocity;
        }

    }


    //Funcion de gravedad
    void SetGravity()
    {
        if (player.isGrounded)
        {
            gravity = 0f;
            fallVelocity = gravity;
            movePlayer.y = fallVelocity;
        }

        else
        {
            gravity = 9.81f;
            fallVelocity -= gravity * Time.deltaTime;
            movePlayer.y = fallVelocity;
        }

    }

}
