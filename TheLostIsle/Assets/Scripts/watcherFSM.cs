using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;



public enum WatcherState { Idle, Alert, Chase, Return }

public class WatcherFSM : MonoBehaviour
{
    public WatcherState state = WatcherState.Idle;
    public Transform[] waypoints;
    private int currentWaypoint = 0;
    public Transform player;
    public float alertDistance = 50f;
    public float chaseDistance = 25f;
    public float returnDistance = 15f;
    public NavMeshAgent agent;
    public Image darknessOverlay; // UI image for darkening
    public float maxDarkness = 0.6f;
    public Rigidbody playerRb;  // drag your player's Rigidbody here
public float baseMass = 1f;
public float maxMassMultiplier = 10f;


    void Update()
{
    float distanceToPlayer = Vector3.Distance(transform.position, player.position);

    switch (state)
    {
        case WatcherState.Idle:
            Debug.Log("State: Idle");
            Patrol();
            if (distanceToPlayer < alertDistance)
                state = WatcherState.Alert;
            break;

        case WatcherState.Alert:
            Debug.Log($"State: Alert | Distance to player = {distanceToPlayer}");

            LookAtPlayer();

            if (distanceToPlayer < chaseDistance -1f)
             {
                 Debug.Log("Distance < chaseDistance — switching to CHASE!");
                 state = WatcherState.Chase;
     }
            else if (distanceToPlayer > alertDistance+ 1f)
     {
                  Debug.Log("Distance > returnDistance — switching to RETURN.");
                 state = WatcherState.Return;
    }
    break;

            

        case WatcherState.Chase:
            Debug.Log("State: Chase");
            agent.SetDestination(player.position);
            UpdateDarkness(distanceToPlayer);
            if (distanceToPlayer > chaseDistance +1f)
                state = WatcherState.Return;
            break;

        case WatcherState.Return:
            Debug.Log("State: Return");
            ReturnToPatrol();
            ResetDarkness();
            if (distanceToPlayer < alertDistance)
                state = WatcherState.Alert;
            break;
    }
}


    void Patrol()
{
    if (!agent.isOnNavMesh)
    {
        Debug.LogWarning("Watcher is not on the NavMesh!");
        return;
    }

    if (waypoints.Length == 0)
    {
        Debug.LogError("No waypoints assigned to Watcher!");
        return;
    }

    if (agent.remainingDistance < 0.5f)
    {
        currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        agent.SetDestination(waypoints[currentWaypoint].position);
    }
}


    void LookAtPlayer()
    {
        transform.LookAt(player);
    }

    void ReturnToPatrol()
    {
        agent.SetDestination(waypoints[currentWaypoint].position);
    }

    void UpdateDarkness(float distance)
{
    float t = Mathf.Clamp01(1 - (distance / chaseDistance));
    float alpha = Mathf.Lerp(0, maxDarkness, t);
    darknessOverlay.color = new Color(0, 0, 0, alpha);
    {
    float newMass = Mathf.Lerp(baseMass, baseMass * maxMassMultiplier, t);
    playerRb.mass = newMass;
}
    Debug.Log($"Distance to player: {distance}, Alpha: {alpha}");
}


    void ResetDarkness()
    {
        darknessOverlay.color = new Color(0, 0, 0, 0);
    }
}  