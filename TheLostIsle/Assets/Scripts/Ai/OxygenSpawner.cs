using UnityEngine;

public class OxygenSpawner : MonoBehaviour
{
    [SerializeField] private OxygenCounter oxygenCounter;
    public GameObject oxygenTankPrefab;
    public Transform player;
    public PathFinder pathfinder;
    public UIArrowIndicator uiArrowIndicator;

    public float spawnRadius = 30f;
    public float criticalOxygenLevel = 30f;
    public float minSpawnDistance = 10f;
    public float maxSpawnDistance = 25f;

    private bool oxygenSpawned = false;

    private void Update()
    {
        if (oxygenCounter == null || pathfinder == null || uiArrowIndicator == null)
        {
            Debug.LogError("One or more required references are missing in the OxygenSpawner. Check Inspector settings.");
            return;
        }

        int playerOxygen = oxygenCounter.oxygenLevel;

        if (playerOxygen <= criticalOxygenLevel && !oxygenSpawned)
        {
            SpawnOxygenTank(2);
            oxygenSpawned = true;
        }

        if (playerOxygen > criticalOxygenLevel)
        {
            oxygenSpawned = false;
            uiArrowIndicator.ClearTarget(); // Hide arrow when oxygen level is safe
        }
    }

    public void RespawnOneTank()
    {
        SpawnOxygenTank(1);
    }

    private void SpawnOxygenTank(int count = 1)
    {
        if (pathfinder == null || uiArrowIndicator == null)
        {
            Debug.LogError("PathFinder or UIArrowIndicator reference is missing. Check Inspector settings.");
            return;
        }
        for (int i = 0; i < count; i++)
    {


            Vector3 spawnPosition = GetRandomSpawnPosition();
            GameObject spawnedTank = Instantiate(oxygenTankPrefab, spawnPosition, Quaternion.identity);
            OxygenTank ot = spawnedTank.GetComponent<OxygenTank>();
            if (ot != null)
            {
                ot.spawner = this;
            }
            if (i == 0)
        {

                pathfinder.SetCurrentTarget(spawnedTank.transform);
                pathfinder.FindPath(player.position, spawnedTank.transform.position);

                uiArrowIndicator.SetTarget(spawnedTank.transform); // Only show arrow when a tank is spawned
        }
        Debug.Log("Oxygen Tank Spawned at: " + spawnPosition);
    }
    }

    // private Vector3 GetRandomSpawnPosition()
    // {
    //     Vector3 randomDirection = Random.insideUnitSphere * maxSpawnDistance;
    //     randomDirection += player.position;
    //     randomDirection.y = player.position.y + 4f;

    //     float distanceFromPlayer = Vector3.Distance(player.position, randomDirection);

    //     while (distanceFromPlayer < minSpawnDistance)
    //     {
    //         randomDirection = Random.insideUnitSphere * maxSpawnDistance + player.position;
    //         randomDirection.y = player.position.y;
    //         distanceFromPlayer = Vector3.Distance(player.position, randomDirection);
    //     }

    //     return randomDirection;
    // }


    private Vector3 GetRandomSpawnPosition()
{
    int maxAttempts = 20;
    for (int i = 0; i < maxAttempts; i++)
    {
        Vector3 randomDirection = Random.insideUnitSphere * maxSpawnDistance + player.position;
        randomDirection.y = player.position.y + 5f;

        float distanceFromPlayer = Vector3.Distance(player.position, randomDirection);
        if (distanceFromPlayer < minSpawnDistance)
            continue;

        // Check if space is free using a sphere overlap (adjust radius as needed)
        float checkRadius = 1f; // Size of your oxygen tank
        Collider[] hits = Physics.OverlapSphere(randomDirection, checkRadius);
        bool hasBlockingObject = false;

        foreach (var hit in hits)
        {
            if (hit.attachedRigidbody != null && hit.gameObject != player.gameObject)
            {
                hasBlockingObject = true;
                break;
            }
        }

        if (!hasBlockingObject)
            return randomDirection;
    }

    // Fallback: just use max distance forward if all attempts fail
    Debug.LogWarning("Could not find unoccupied space to spawn oxygen tank. Spawning fallback.");
    return player.position + player.forward * maxSpawnDistance;
}

}
