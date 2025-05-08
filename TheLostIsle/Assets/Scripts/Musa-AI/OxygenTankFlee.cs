using UnityEngine;
using UnityEngine.AI;

public class OxygenTankFlee : MonoBehaviour
{
    public Transform player;
    public float detectionRadius = 5f;
    public float fleeDistance = 5f;
    public float fleeSpeed = 3f;

    private NavMeshAgent agent;
    private bool isFleeing = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = fleeSpeed;

        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
            else
            {
                Debug.LogWarning("No player object found with tag 'Player'. Please assign the player.");
            }
        }
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < detectionRadius)
        {
            isFleeing = true;
            Vector3 fleeDirection = (transform.position - player.position).normalized;
            Vector3 targetPosition = transform.position + fleeDirection * fleeDistance;

            // Clamp destination to NavMesh
            NavMeshHit hit;
            if (NavMesh.SamplePosition(targetPosition, out hit, fleeDistance, NavMesh.AllAreas))
            {
                agent.SetDestination(hit.position);
            }
        }
        else if (isFleeing && distance > detectionRadius * 2)
        {
            // Stop fleeing when far enough
            isFleeing = false;
            agent.ResetPath();
        }
    }
}
