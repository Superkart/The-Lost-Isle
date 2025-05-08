using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BatteryManager : MonoBehaviour
{
    [Header("Battery UI")]
    [SerializeField] private TextMeshProUGUI batteryLevelText;
    [SerializeField] private Slider batterySlider;

    [Header("Battery Settings")]
    public float batteryLevel = 100f;
    public float maxBattery = 100f;

    private void Start()
    {
        SetMaxBattery(maxBattery);
        UpdateBatteryText(batteryLevel);
    }

    private void UpdateBatteryText(float currentLevel)
    {
        batteryLevelText.text = $"Battery: {Mathf.RoundToInt(currentLevel)}%";
    }

    public void DrainBattery(float amount)
    {
        batteryLevel -= amount;
        batteryLevel = Mathf.Clamp(batteryLevel, 0f, maxBattery);

        SetBatteryLevel(batteryLevel);
        UpdateBatteryText(batteryLevel);
    }

    public void RechargeBattery(float amount)
    {
        batteryLevel += amount;
        batteryLevel = Mathf.Clamp(batteryLevel, 0f, maxBattery);

        SetBatteryLevel(batteryLevel);
        UpdateBatteryText(batteryLevel);
    }

    public void SetBatteryLevel(float level)
    {
        batterySlider.value = level;
    }

    public void SetMaxBattery(float max)
    {
        batterySlider.maxValue = max;
        batterySlider.value = batteryLevel;
    }

    public bool HasBattery()
    {
        return batteryLevel > 0f;
    }
}
