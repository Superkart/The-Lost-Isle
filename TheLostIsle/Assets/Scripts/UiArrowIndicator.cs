using UnityEngine;
using UnityEngine.UI;

public class UIArrowIndicator : MonoBehaviour
{
    public Transform player;
    public Image arrowImage;
    private Transform target;

    private void Start()
    {
        // Hide the arrow initially since there's no target
        arrowImage.enabled = false;
    }

    private void Update()
    {
        if (target == null)
        {
            arrowImage.enabled = false;
            return;
        }

        Vector3 targetDirection = (target.position - player.position).normalized;

        // Project the direction to the player's forward vector plane
        Vector3 forward = player.forward;
        forward.y = 0; // Make sure we are working in a flat plane (ignore Y-axis)
        targetDirection.y = 0; // Ignore vertical difference

        float angle = Vector3.SignedAngle(forward, targetDirection, Vector3.up);

        // Rotate the UI arrow
        arrowImage.rectTransform.rotation = Quaternion.Euler(0, 0, -angle);

        // Show or hide the arrow based on if the target is within view
        arrowImage.enabled = !IsTargetVisible();
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        arrowImage.enabled = true; // Enable the arrow when a target is set
    }

    private bool IsTargetVisible()
    {
        if (target == null) return false;

        Vector3 screenPoint = Camera.main.WorldToViewportPoint(target.position);
        return screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
    }

    public void ClearTarget()
    {
        target = null;
        arrowImage.enabled = false; // Hide the arrow when the target is cleared
    }
}
