using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class playerMovement : MonoBehaviour
{

    public CharacterController characterController;

    public float speed = 10f;
    public float rotationSpeed = 150f;
    Vector3 direction;

    [SerializeField] private float gravity = -10f;
    private float gravMult = 10f;
    private float velocity;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    } 

    // Update is called once per frame
    void Update()
    {
        Gravity();
        Movement();
    }

    private void Movement()
    {
        //move left right
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        //normalized so player doesn't move faster diagonally
        direction = new Vector3(horizontal, 0f, vertical).normalized;
        direction *= speed * Time.deltaTime;

        //character rotate
        if(horizontal != 0)
        {
            float angle = transform.eulerAngles.y + (horizontal * rotationSpeed * Time.deltaTime);
            transform.eulerAngles = new Vector3(0, angle, 0);
        }

        characterController.Move(direction + Vector3.up * velocity * Time.deltaTime);
    }

    private void Gravity()
    {
        if(!characterController.isGrounded)
        {
            velocity = -1f;
        }
        else
        {
            velocity += gravity * gravMult * Time.deltaTime;
        }

        direction.y = velocity;
    }   
}
