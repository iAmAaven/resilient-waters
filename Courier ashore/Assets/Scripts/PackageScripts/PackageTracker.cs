using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageTracker : MonoBehaviour
{
    public Transform target;
    public float rotationSpeed = 2;
    private Transform playerPos;

    void Start()
    {
        playerPos = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (target != null)
        {
            Vector3 direction = target.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        if (playerPos != null)
        {
            transform.position = playerPos.position;
        }
    }
}