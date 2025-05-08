using UnityEngine;
using UnityEngine.AI;

public class WatcherFSM1 : MonoBehaviour
{
    public enum WatcherState { Idle, CloseToPlayer, KillPlayer }
    public WatcherState currentState = WatcherState.Idle;

    public AudioSource watcherSound;

    [Header("References")]
    public Transform player;
    public GameManager gameManager;

    [Header("Parameters")]
    public float detectionDistance = 30f;
    public float killDistance = 5f;
    public float timeToKill = 3f;

    private NavMeshAgent agent;
    private float closeTimer = 0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        watcherSound = GetComponent<AudioSource>();

        if (player == null)
            Debug.LogError("Player reference is missing on WatcherFSM1.");
    }

    void Update()
    {
        if (player == null || agent == null) return;

        switch (currentState)
        {
            case WatcherState.Idle:
                IdleBehavior();
                break;
            case WatcherState.CloseToPlayer:
                CloseToPlayerBehavior();
                break;
            case WatcherState.KillPlayer:
                KillPlayerBehavior();
                break;
        }

        Debug.DrawLine(transform.position, GetProjectedPlayerPosition(), Color.red); // Visual debug
    }

    // IDLE STATE
    private void IdleBehavior()
    {
        MoveToProjectedPlayer();

        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= detectionDistance)
        {
            if (!watcherSound.isPlaying)
                watcherSound.Play();

            currentState = WatcherState.CloseToPlayer;
        }
        if (distance > detectionDistance && watcherSound.isPlaying)
        {
            watcherSound.Stop(); // Cleanly stop the loop
        }
    }

    // CLOSE STATE
    private void CloseToPlayerBehavior()
    {
        MoveToProjectedPlayer();

        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= killDistance)
        {
            closeTimer += Time.deltaTime;
            if (closeTimer >= timeToKill)
            {
                currentState = WatcherState.KillPlayer;
            }
        }
        else
        {
            closeTimer = 0f;

            if (distance > detectionDistance)
                currentState = WatcherState.Idle;
        }
    }

    // KILL STATE
    private void KillPlayerBehavior()
    {
        if (gameManager != null)
        {
            Debug.Log("Player Killed by Watcher");
            gameManager.deathAudio.Play();
            FindObjectOfType<UiManager2>().ShowGameLostPanel();
            Destroy(this.gameObject); // Watcher disappears after kill

        }
        else
        {
            Debug.LogWarning("GameManager reference not set in WatcherFSM1.");
        }

        enabled = false; // Stop FSM after kill
    }

    // Move agent to player projected on plane height
    private void MoveToProjectedPlayer()
    {
        Vector3 projectedPlayerPos = GetProjectedPlayerPosition();
        agent.SetDestination(projectedPlayerPos);
    }

    // Get player position on the same Y height as Watcher
    private Vector3 GetProjectedPlayerPosition()
    {
        return new Vector3(player.position.x, transform.position.y, player.position.z);
    }
}
