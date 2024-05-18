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
        acceleration = PlayerPrefs.GetFloat("BoatSpeed", acceleration);
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
                rb.AddForce(transform.up * acceleration * movementInput.y);

                float torque = -movementInput.x * rotationSpeed;
                float clampedTorque = Mathf.Clamp(torque, -maxTorque, maxTorque);
                rb.angularVelocity = clampedTorque;
            }
            else
            {
                rb.angularVelocity *= 0.9f;
            }

            if (movementInput.y == 0f)
            {
                rb.velocity *= 0.98f;
                boatAnim.SetBool("IsMoving", false);
                boatAnim.SetBool("IsGoingBackwards", false);
            }
            else if (movementInput.y > 0f)
            {
                boatAnim.SetBool("IsMoving", true);
                boatAnim.SetBool("IsGoingBackwards", false);
            }
            else
            {
                boatAnim.SetBool("IsMoving", false);
                boatAnim.SetBool("IsGoingBackwards", true);
            }

            if (rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }

            if (movementInput.x > 0.01f)
            {
                boatAnim.SetBool("IsTurningRight", true);
                boatAnim.SetBool("IsTurningLeft", false);
            }
            else if (movementInput.x < -0.01f)
            {
                boatAnim.SetBool("IsTurningRight", false);
                boatAnim.SetBool("IsTurningLeft", true);
            }
            else
            {
                boatAnim.SetBool("IsTurningRight", false);
                boatAnim.SetBool("IsTurningLeft", false);
            }
        }
        else
        {
            rb.velocity *= 0.98f;
            rb.angularVelocity *= 0.9f;
            boatAnim.SetBool("IsMoving", false);
            boatAnim.SetBool("IsTurningLeft", false);
            boatAnim.SetBool("IsTurningRight", false);
            boatAnim.SetBool("IsGoingBackwards", false);
        }
    }
}