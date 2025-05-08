using UnityEngine;

public class OxygenTank : MonoBehaviour
{
    public OxygenSpawner spawner;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Death"))
        {
            Debug.Log("Oxygen tank fell into death zone, respawning...");
            spawner?.RespawnOneTank();  // Ask spawner to retry
            Destroy(gameObject);
        }
    }
}
