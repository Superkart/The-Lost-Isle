using UnityEngine;

public class FlashLightToggle : MonoBehaviour
{
    [Header("Flashlight Settings")]
    public Light flashlight;
    public KeyCode toggleKey = KeyCode.F;
    public float drainRate = 10f; // battery % per second while on

    [Header("Battery Reference")]
    [SerializeField] private BatteryManager batteryManager;

    private bool isOn = false;

    void Start()
    {
        if (flashlight != null)
        {
            flashlight.enabled = isOn;
        }
        else
        {
            Debug.LogWarning("Flashlight not assigned in the Inspector.");
        }

        if (batteryManager == null)
        {
            Debug.LogWarning("BatteryManager not assigned in the Inspector.");
        }
    }

    void Update()
    {
        if (flashlight == null || batteryManager == null) return;

        // Toggle flashlight ON/OFF
        if (Input.GetKeyDown(toggleKey) && batteryManager.HasBattery())
        {
            isOn = !isOn;
            flashlight.enabled = isOn;
        }

        // Drain battery if flashlight is ON
        if (isOn && batteryManager.HasBattery())
        {
            batteryManager.DrainBattery(Time.deltaTime * drainRate);

            // Auto shut off if battery is gone
            if (!batteryManager.HasBattery())
            {
                flashlight.enabled = false;
                isOn = false;
            }
        }


    }
}
