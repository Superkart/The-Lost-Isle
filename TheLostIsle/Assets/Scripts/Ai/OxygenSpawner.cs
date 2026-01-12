using UnityEngine;
using UnityEngine.AI;

public class OxygenSpawner : MonoBehaviour
{
    [SerializeField] private OxygenCounter oxygenCounter;
    public GameObject oxygenTankPrefab;
    public Transform player;
    public PathFinder pathfinder;
    public UIArrowIndicator uiArrowIndicator;
    public LayerMask walkableLayerMask; // Optional: Layers to raycast against for spawning
    [SerializeField] private bool useLayerMask = false; // If false, raycasts hit all layers (no editor changes required)
    [SerializeField] private float maxSlopeAngle = 45f; // Reject steep surfaces
    [SerializeField] private bool snapToNavMesh = true; // Try to snap to NavMesh if available

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
        // Spawn in a random direction around the player (not dependent on facing direction)
        Vector3 randomDirection = Random.insideUnitCircle * Random.Range(minSpawnDistance, maxSpawnDistance);
        Vector3 spawnPos = player.position + new Vector3(randomDirection.x, 10f, randomDirection.y); // Start above to raycast down

        // Raycast down to find ground. If useLayerMask is false or mask is 0, hit everything.
        RaycastHit hit;
        bool didHit = useLayerMask && walkableLayerMask.value != 0
            ? Physics.Raycast(spawnPos, Vector3.down, out hit, 50f, walkableLayerMask)
            : Physics.Raycast(spawnPos, Vector3.down, out hit, 50f);

        if (didHit)
        {
            // Skip invisible kill plane tagged as "Death"
            if (hit.collider.CompareTag("Death"))
                continue;

            // Reject overly steep surfaces
            if (Vector3.Angle(hit.normal, Vector3.up) > maxSlopeAngle)
                continue;

            // Spawn on the ground surface
            Vector3 groundSpawnPos = hit.point + Vector3.up * 1f;

            // Optionally snap to closest NavMesh position (if a NavMesh exists)
            if (snapToNavMesh)
            {
                NavMeshHit navHit;
                if (NavMesh.SamplePosition(groundSpawnPos, out navHit, 2f, NavMesh.AllAreas))
                {
                    groundSpawnPos = navHit.position + Vector3.up * 0.5f;
                }
            }

            // Check if space is free
            float checkRadius = 1f;
            Collider[] hits = Physics.OverlapSphere(groundSpawnPos, checkRadius);
            bool hasBlockingObject = false;

            foreach (var collider in hits)
            {
                if (collider.attachedRigidbody != null && collider.gameObject != player.gameObject)
                {
                    hasBlockingObject = true;
                    break;
                }
            }

            if (!hasBlockingObject)
                return groundSpawnPos;
        }
    }

    // Fallback: spawn directly above player and raycast down
    Debug.LogWarning("Could not find suitable spawn location. Using fallback.");
    Vector3 fallbackPos = player.position + Vector3.up * 10f;
    RaycastHit fallbackHit;
    bool fallbackDidHit = useLayerMask && walkableLayerMask.value != 0
        ? Physics.Raycast(fallbackPos, Vector3.down, out fallbackHit, 50f, walkableLayerMask)
        : Physics.Raycast(fallbackPos, Vector3.down, out fallbackHit, 50f);

    if (fallbackDidHit && !fallbackHit.collider.CompareTag("Death") && Vector3.Angle(fallbackHit.normal, Vector3.up) <= maxSlopeAngle)
    {
        Vector3 pos = fallbackHit.point + Vector3.up * 1f;
        if (snapToNavMesh)
        {
            NavMeshHit navHit;
            if (NavMesh.SamplePosition(pos, out navHit, 2f, NavMesh.AllAreas))
                pos = navHit.position + Vector3.up * 0.5f;
        }
        return pos;
    }
    
    return player.position + Vector3.up * 1f;
}

}
