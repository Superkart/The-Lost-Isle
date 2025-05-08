using UnityEngine;

public class GroundPulseSpawner : MonoBehaviour
{
    public GameObject pulsePrefab;
    public Transform groundSpawnPoint;
    public float pulseInterval = 0.25f;

    private float pulseTimer = 0f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        pulseTimer += Time.deltaTime;

        // Only spawn pulse while player is grounded and moving
        if (pulseTimer >= pulseInterval && IsGrounded() && rb.linearVelocity.magnitude > 0.1f)
        {
            Instantiate(pulsePrefab, groundSpawnPoint.position, Quaternion.identity);
            pulseTimer = 0f;
        }
    }

    bool IsGrounded()
    {
        // Adjust ray length and layer mask if needed
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }
}
