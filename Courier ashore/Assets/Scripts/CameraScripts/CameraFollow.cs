using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Camera Limits")]
    [SerializeField] Vector3 minValue;
    [SerializeField] Vector3 maxValue;
    [SerializeField] Vector3 offset;

    [Header("Smoothness")]
    [Range(1, 10)]
    [SerializeField] float smoothFactor;

    // PRIVATES
    private Transform target;
    void Start()
    {
        FindTarget();
    }
    private void FixedUpdate()
    {
        if (target)
        {
            Follow();
        }
    }

    void Follow()
    {
        Vector3 targetPosition = target.position + offset;

        Vector3 boundPosition = new Vector3(
            Mathf.Clamp(targetPosition.x, minValue.x, maxValue.x),
            Mathf.Clamp(targetPosition.y, minValue.y, maxValue.y),
            Mathf.Clamp(targetPosition.z, minValue.z, maxValue.z));

        Vector3 smoothPosition = Vector3.Lerp(transform.position, boundPosition, smoothFactor * Time.fixedDeltaTime);
        transform.position = smoothPosition;
    }

    public void FindTarget()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
}
