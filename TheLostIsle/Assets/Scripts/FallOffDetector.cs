using UnityEngine;

public class FallOffDetector : MonoBehaviour
{
    private UiManager2 uiManager;
    [SerializeField] private AudioSource deathAudio;
    private void Start()
    {
        uiManager = FindObjectOfType<UiManager2>();

        if (uiManager == null)
        {
            Debug.LogError("UIManager2 script not found in the scene.");
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            PlayDeathSound(collision.gameObject.tag);
            uiManager.ShowGameLostPanel();
        }
    }
    private void PlayDeathSound(string tag)
    {
        if (tag == "Player")
        {
            if (!deathAudio.isPlaying)
            {
                deathAudio.Play();
            }
        }

    }
}
