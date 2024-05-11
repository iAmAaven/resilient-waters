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


    // PRIVATES
    private Rigidbody2D rb;
    private Vector2 movementInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");
    }
    void FixedUpdate()
    {
        if (movementInput != Vector2.zero)
        {
            // Debug.Log("Boat speed: " + rb.velocity.magnitude);
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

}