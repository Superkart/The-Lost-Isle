using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    private bool isCollected = false; // Prevents multiple triggers

    [SerializeField] private AudioSource oxygenAudio;
    [SerializeField] private AudioSource fuelAudio;
    private void OnTriggerEnter(Collider other)
    {
        PlaySounds(gameObject.tag);
        if (isCollected) return; // Ignore additional triggers
        if (other.CompareTag("Player"))
        {
            InventoryManager inventory = FindObjectOfType<InventoryManager>();
            if (inventory != null)
            {
                GetComponent<Collider>().enabled = false; // Disable collider to prevent further triggers
                inventory.AddItemByTag(gameObject.tag);
                // Debug.Log("Collected a " + gameObject.tag + "tagged object.");
            }
            isCollected = true; // Mark item as collected
            Destroy(gameObject); // Destroy object after short delay
        }
    }


    private void PlaySounds(string tag)
    {
        if(tag == "OxygenTank")
        {
            if (!oxygenAudio.isPlaying)
            {
                oxygenAudio.Play();
            }
        }
        if(tag == "Fuel")
        {
           fuelAudio.Play();
        }
    }
}
