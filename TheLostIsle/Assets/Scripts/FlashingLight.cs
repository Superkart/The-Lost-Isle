using UnityEngine;

public class FlashingLight : MonoBehaviour
{
    public Light flashingLight;
    public float flashSpeed = 2f; // How fast it flashes

    private void Update()
    {
        if (flashingLight != null && flashingLight.enabled) // Only flash if the light is ON
        {
            flashingLight.intensity = Mathf.Abs(Mathf.Sin(Time.time * flashSpeed)) * 3f;
        }
        else if (flashingLight != null)
        {
            flashingLight.intensity = 0f; // Force intensity to 0 if light is OFF
        }
    }
}
