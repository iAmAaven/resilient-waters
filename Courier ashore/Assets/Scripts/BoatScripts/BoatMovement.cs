using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    [Header("Movement stats")]
    [SerializeField] float acceleration = 5f;
    [SerializeField] float maxSpeed = 5f;
    [SerializeField] float rotationSpeed = 3f;
    [SerializeField] float maxTorque = 0.5f;


    // HIDDEN
    [HideInInspector] public bool canPlayerMove = true;
    [HideInInspector] public bool isPlayerHarvesting = false;
    [HideInInspector] public bool playerPassedOut = false;

    // PRIVATES
    private Animator boatAnim;
    private Rigidbody2D rb;
    private Vector2 movementInput;

    void Start()
    {
        acceleration = PlayerPrefs.GetFloat("BoatSpeed", 5);
        maxSpeed = acceleration;
        boatAnim = GetComponentInChildren<Animator>();

        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");
    }
    void FixedUpdate()
    {
        if (canPlayerMove == true && playerPassedOut == false)
        {
            if (movementInput != Vector2.zero)
            {
                boatAnim.SetBool("IsMoving", true);
                rb.AddForce(transform.up * acceleration * movementInput.y);

                float torque = -movementInput.x * rotationSpeed;
                float clampedTorque = Mathf.Clamp(torque, -maxTorque, maxTorque);
                rb.angularVelocity = clampedTorque;
            }
            else
            {
                boatAnim.SetBool("IsMoving", false);
                rb.angularVelocity *= 0.9f;
            }

            if (movementInput.y == 0f)
            {
                rb.velocity *= 0.98f;
            }

            if (rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }
        }
        else
        {
            rb.velocity *= 0.98f;
            rb.angularVelocity *= 0.9f;
            boatAnim.SetBool("IsMoving", false);
        }
    }
}