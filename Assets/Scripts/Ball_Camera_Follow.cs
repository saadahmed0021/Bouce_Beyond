using UnityEngine;

public class Ball_Camera_Follow : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform target;

    [Header("Horizontal Follow")]
    [SerializeField] private float leftThreshold = -4f;
    [SerializeField] private float rightThreshold = 4f;
    [SerializeField] private float horizontalSmoothTime = 0.15f;

    [Header("Vertical Follow")]
    [SerializeField] private float bottomThreshold = -3f;
    [SerializeField] private float topThreshold = 3f;
    [SerializeField] private float verticalSmoothTime = 0.15f;

    [Header("Camera Bounds")]
    [SerializeField] private bool followX = true;
    [SerializeField] private bool followY = true;

    private float targetX;
    private float targetY;

    private float horizontalVelocity;
    private float verticalVelocity;

    private void Start()
    {
        if (target == null)
            return;

        targetX = transform.position.x;
        targetY = transform.position.y;
    }

    private void LateUpdate()
    {
        if (target == null)
            return;

        Vector3 cameraPosition = transform.position;

        float relativeX = target.position.x - transform.position.x;
        float relativeY = target.position.y - transform.position.y;

        if (followX)
        {
            if (relativeX > rightThreshold)
                targetX = target.position.x - rightThreshold;

            else if (relativeX < leftThreshold)
                targetX = target.position.x - leftThreshold;
        }

        if (followY)
        {
            if (relativeY > topThreshold)
                targetY = target.position.y - topThreshold;

            else if (relativeY < bottomThreshold)
                targetY = target.position.y - bottomThreshold;
        }

        float newX = Mathf.SmoothDamp(
            cameraPosition.x,
            targetX,
            ref horizontalVelocity,
            horizontalSmoothTime
        );

        float newY = Mathf.SmoothDamp(
            cameraPosition.y,
            targetY,
            ref verticalVelocity,
            verticalSmoothTime
        );

        transform.position = new Vector3(
            newX,
            newY,
            cameraPosition.z
        );
    }
}