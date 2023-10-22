using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController characterController;
    public Transform groundCheck;
    public LayerMask groundMask;
    [SerializeField]
    private float speed = 10f;
    private float speedDefault;
    [SerializeField]
    private float sneakSpeed = 5f;
    [SerializeField]
    private float runSpeed = 20f;
    [SerializeField]
    private float gravity = -9.8f;
    [SerializeField]
    private float groundDistance = 0.4f;
    [SerializeField]
    private float jumpHeight = 3f;
    private Vector3 velocity;
    private bool isGrounded;

    private void Awake()
    {
        speedDefault = speed;
    }
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        Gravity();
        Moving();
        isRunning();
        isJumping();
        isSneaking();
     
    }

    void isRunning()
    {
        if (Input.GetKeyDown("left shift"))
        {
            speed = runSpeed;
        }
        if (Input.GetKeyUp("left shift"))
        {
            speed = speedDefault;
        }
    }

    void isSneaking()
    {
        if (Input.GetKeyDown("c"))
        {
            characterController.height = 1f;
            speed = sneakSpeed;
        }
        if (Input.GetKeyUp("c"))
        {
            characterController.height = 2f;
            speed = speedDefault;
        }
    }

    void isJumping()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    void Gravity()
    {
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    void Moving()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * y;
        characterController.Move(move * speed * Time.deltaTime);
    }
}
