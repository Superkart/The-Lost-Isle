using System.Collections;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator animator;
    //public OxygenCounter oxygenCounter;
    public AudioSource flapAudio;
    public Transform player;
    //public int triggerThreshold = 98;
    public float flapAudioDuration = 8f;
    public int detectionDistance = 5;
    private bool hasFlapped = false;

    void Update()
    {
        //int o2 = oxygenCounter.oxygenLevel;

        float distance = Vector3.Distance(transform.position, player.position);
        if (!hasFlapped && distance <= detectionDistance)
        {
            PlayFlapSequence();
            hasFlapped = true;
        }

        if (hasFlapped && distance > detectionDistance)
        {
            hasFlapped = false;
        }
    }

    void PlayFlapSequence()
    {
        animator.SetTrigger("Flap");     
        flapAudio.Play();                
        StartCoroutine(StopSoundAfter(flapAudioDuration)); 
    }

    IEnumerator StopSoundAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        flapAudio.Stop();
    }
}
