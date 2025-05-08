using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OxygenCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI oxygenLevelText;

    public Slider slider;

    public int oxygenLevel = 100;
    public float RateOfO2Reduction = 1f; 
    private float oxygenTimer;
    public float timeToO2Reduction = 1;

    // For counting O2
    private OxygenCounter counter;

    private void Start()
    {
        UpdateOxygenLevelText(oxygenLevel);

        counter = GetComponent<OxygenCounter>();
        
        // Update the O2 UI
        setMaxOxygen(100);
    }

    private void Update()
    {
        HandleOxygenReduction();
    }
    private void HandleOxygenReduction()
    {
        oxygenTimer += Time.deltaTime;

        if (oxygenTimer >= timeToO2Reduction)
        {
            oxygenLevel = O2LevelReduction(oxygenLevel, Mathf.RoundToInt(RateOfO2Reduction));
            UpdateOxygenLevelText(oxygenLevel);

            oxygenTimer = 0f; 
        }
        if (oxygenLevel < 0)
        {
            oxygenLevel = 0;
            UpdateOxygenLevelText(oxygenLevel);
        }
    }

    private int O2LevelReduction(int currentO2Level, int RateOfO2Reduction)
    {
        // Update O2 UI
        setOxygen(currentO2Level - RateOfO2Reduction);
        
        return currentO2Level - RateOfO2Reduction;
    }
    private void UpdateOxygenLevelText(int currentO2Level)
    {
        oxygenLevelText.text = $"Oxygen: {currentO2Level}";
    }
    

    public void setOxygen(int oxygenLevel) {
        slider.value = oxygenLevel;
    }

    public void setMaxOxygen(int maxOxygen) {
        slider.maxValue = maxOxygen;
        slider.value = oxygenLevel;
    }
}
