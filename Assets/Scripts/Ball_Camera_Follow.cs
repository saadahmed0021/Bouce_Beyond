using UnityEngine;

public class Ball_Camera_Follow : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform target;

    [Header("Horizontal Thresholds")]
    [SerializeField] private float leftThreshold = -4f;
    [SerializeField] private float rightThreshold = 4f;
    [SerializeField] private float horizontalSmoothTime = 0.08f;

    [Header("Horizontal Bounds")]
    [SerializeField] private float minimumX = 5f;
    [SerializeField] private float maximumX = 600f;

    [Header("Vertical Thresholds")]
    [SerializeField] private float bottomThreshold = -3f;
    [SerializeField] private float topThreshold = 3f;
    [SerializeField] private float verticalSmoothTime = 0.12f;

    [Header("Vertical Bounds")]
    [SerializeField] private float minimumY = 5f;
    [SerializeField] private float maximumY = 600f;

    [Header("Follow Axes")]
    [SerializeField] private bool followX = true;
    [SerializeField] private bool followY = true;

    private float targetX;
    private float targetY;
    private float horizontalVelocity;
    private float verticalVelocity;

    private void Awake()
    {
        Vector3 position = transform.position;
        targetX = position.x;
        targetY = position.y;
    }

    private void LateUpdate()
    {
        if (target == null)
            return;

        Vector3 cameraPosition = transform.position;

        float relativeX = target.position.x - cameraPosition.x;
        float relativeY = target.position.y - cameraPosition.y;

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

        float newX = followX
            ? Mathf.SmoothDamp(
                cameraPosition.x,
                targetX,
                ref horizontalVelocity,
                horizontalSmoothTime,
                Mathf.Infinity,
                Time.unscaledDeltaTime)
            : cameraPosition.x;

        float newY = followY
            ? Mathf.SmoothDamp(
                cameraPosition.y,
                targetY,
                ref verticalVelocity,
                verticalSmoothTime,
                Mathf.Infinity,
                Time.unscaledDeltaTime)
            : cameraPosition.y;

        newX = Mathf.Clamp(newX, minimumX, maximumX);
        newY = Mathf.Clamp(newY, minimumY, maximumY);

        transform.position = new Vector3(
            newX,
            newY,
            cameraPosition.z
        );
    }
}