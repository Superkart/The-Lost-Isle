using UnityEngine;

public class OxygenAudioTrigger : MonoBehaviour
{
    public OxygenCounter oxygenCounter;
    public AudioSource breathingAudio;
    public int oxygenThreshold = 30;

    void Update()
    {
        if (oxygenCounter.oxygenLevel < oxygenThreshold)
        {
            if (!breathingAudio.isPlaying)
                breathingAudio.Play();
        }
        else
        {
            if (breathingAudio.isPlaying)
                breathingAudio.Stop();
        }
    }
}
