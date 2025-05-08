using UnityEngine;

public class CrosshairFollowMouse : MonoBehaviour
{
    RectTransform crosshair;

    void Start()
    {
        // Get the RectTransform of the UI element
        crosshair = GetComponent<RectTransform>();
    }

    void Update()
    {
        // Move the UI element to match the mouse position
        crosshair.position = Input.mousePosition;
    }
}
