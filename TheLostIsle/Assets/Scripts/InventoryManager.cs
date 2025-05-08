using System.Collections.Generic;
using UnityEngine;
using TMPro; // Import TMP namespace

public class InventoryManager : MonoBehaviour
{
    [Header("UI References (Drag & Drop in Inspector)")]
    public TextMeshProUGUI fuelText;
    public TextMeshProUGUI oxygenText;

    private Dictionary<string, int> inventory = new Dictionary<string, int>();
    private Dictionary<string, TextMeshProUGUI> itemTextObjects = new Dictionary<string, TextMeshProUGUI>();

    private void Start()
    {
        // Manually register UI elements (Ensure they are assigned in Inspector)
        RegisterTagUI("Fuel", fuelText);
        RegisterTagUI("Oxygen", oxygenText);
    }

    public void RegisterTagUI(string tag, TextMeshProUGUI uiText)
    {
        if (uiText == null)
        {
            Debug.LogError($"UI Text for tag '{tag}' is missing! Assign it in the Inspector.");
            return;
        }

        if (!itemTextObjects.ContainsKey(tag))
        {
            itemTextObjects[tag] = uiText;
            inventory[tag] = 0;
            //UpdateInventoryUI(tag);
        }
    }

    public void AddItemByTag(string tag)
    {
        if (inventory.ContainsKey(tag))
        {
            Debug.Log("Item Collected");
            inventory[tag]++;
        }
        else
        {
            Debug.LogError($"Tag '{tag}' does not have an assigned UI Text!");
            return;
        }

        UpdateInventoryUI(tag);
    }

    private void UpdateInventoryUI(string tag)
    {
        if (itemTextObjects.ContainsKey(tag) && itemTextObjects[tag] != null)
        {
            itemTextObjects[tag].text = $"{tag}: {inventory[tag]}";
        }
        else
        {
            Debug.LogError($"UI Text for '{tag}' is missing in itemTextObjects!");
        }
    }
    public int GetItemCount(string tag)
    {
        if (inventory.ContainsKey(tag))
            return inventory[tag];
        return 0;
    }

}
