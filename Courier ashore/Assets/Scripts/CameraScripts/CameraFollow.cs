using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [Header("Smoothness")]
    [SerializeField] float smoothSpeed = 0.125f;

    [Header("Offset")]
    [SerializeField] Vector3 offset;

    [Header("Camera Limits")]
    [SerializeField] Vector3 minValue;
    [SerializeField] Vector3 maxValue;

    private Vector3 velocity = Vector3.zero;
    private Transform target;

    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 targetPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothSpeed);
            Vector3 clampedPosition = new Vector3(
                Mathf.Clamp(smoothedPosition.x, minValue.x, maxValue.x),
                Mathf.Clamp(smoothedPosition.y, minValue.y, maxValue.y),
                Mathf.Clamp(smoothedPosition.z, minValue.z, maxValue.z)
            );
            transform.position = clampedPosition;
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube((maxValue + minValue) / 2, maxValue - minValue);
    }
}