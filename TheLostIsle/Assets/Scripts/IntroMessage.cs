using System.Collections; // Use this for regular UI Text
using System.Collections.Generic; // Use this for regular UI Text
using UnityEngine;
using TMPro; // Use this if you're using TextMeshPro
using UnityEngine.UI;


public class IntroMessage : MonoBehaviour
{
    public TextMeshProUGUI introText; // Drag your Text object here in the Inspector
    public float displayTime = 10f;
    public float fadeDuration = 2f;

    void Start()
    {
        StartCoroutine(ShowIntroText());
    }

    private IEnumerator ShowIntroText()
    {
        // Set message and make it fully visible
        introText.text = "You find yourself in this new area, time to explore!";
        introText.alpha = 1f;

        // Wait for the display time
        yield return new WaitForSeconds(displayTime);

        // Fade out over time
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            introText.alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            yield return null;
        }

        // Optional: disable the text object after fade
        introText.gameObject.SetActive(false);
    }
}
