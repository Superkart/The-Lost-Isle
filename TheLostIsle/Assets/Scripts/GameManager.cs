using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    public OxygenCounter oxygenCounter;
    public InventoryManager inventoryManager;
    public UiManager2 uiManager;

    public AudioSource deathAudio;

    [Header("Settings")]
    public int fuelNeededToWin = 3;

    private bool gameEnded = false;

    void Update()
    {
        if (gameEnded) return;

        CheckOxygenLevel();
        CheckFuelCollected();
    }

    private void CheckOxygenLevel()
    {
        if (oxygenCounter != null && oxygenCounter.oxygenLevel <= 0)
        {
            Debug.Log("Player Died: Oxygen depleted.");
            deathAudio.Play();
            gameEnded = true;
            uiManager.ShowGameLostPanel();
        }
    }

    private void CheckFuelCollected()
    {
        if (inventoryManager != null && inventoryManager.GetItemCount("Fuel") >= fuelNeededToWin)
        {
            Debug.Log("Player Won: Enough fuel collected!");
            gameEnded = true;
            uiManager.ShowGameWonPanel();
        }
    }
}
