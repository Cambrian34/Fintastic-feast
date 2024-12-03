using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Target for the camera to follow
    public Transform target;

    // Offset for the camera position
    public Vector3 offset = new Vector3(0, 1, -10);

    // Smoothing speed for the camera movement
    public float smoothSpeed = 0.125f;

    // Optional boundaries for camera movement
    public bool useBoundaries = false;
    public Vector2 minBoundary;
    public Vector2 maxBoundary;

    private void LateUpdate()
    {
        if (target == null)
            return;

        // Target position with offset
        Vector3 desiredPosition = target.position + offset;

        // If boundaries are enabled, clamp the desired position
        if (useBoundaries)
        {
            desiredPosition.x = Mathf.Clamp(desiredPosition.x, minBoundary.x, maxBoundary.x);
            desiredPosition.y = Mathf.Clamp(desiredPosition.y, minBoundary.y, maxBoundary.y);
        }

        // Smooth transition between current position and target position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
