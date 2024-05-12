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

    [Header("References")]
    [SerializeField] Animator boatAnim;


    // HIDDEN
    private ControlUI controlUI;

    // PRIVATES
    private Rigidbody2D rb;
    private Vector2 movementInput;

    void Start()
    {
        controlUI = FindObjectOfType<ControlUI>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");
    }
    void FixedUpdate()
    {
        if (controlUI.canPlayerMove == false)
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
            boatAnim.SetBool("IsMoving", false);
        }
    }
}