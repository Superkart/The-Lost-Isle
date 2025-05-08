using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections; // Only needed since showing text
using System.Collections.Generic;


public class IntroFadeInText : MonoBehaviour
{
    public TextMeshProUGUI introText;
    public float fadeInDuration = 2f;
    public float displayDuration = 6f;
    public float fadeOutDuration = 2f;
    // public string nextSceneName = "GameScene";

    private void Start()
    {
        // Start fully transparent
        Color c = introText.color;
        introText.color = new Color(c.r, c.g, c.b, 0f);

        StartCoroutine(PlayIntroSequence());
    }

    private IEnumerator PlayIntroSequence()
    {
        // Set your intro text
        introText.text =
            "You find yourself stranded in this strange place. In the distance, you see a strange figure.\n" +
            "It doesn't seem very friendly... you should leave as soon as you can.\n" +
            "Find the fuel you need, while keeping up your O2 levels, and escape this place.";

        // Fade in
        yield return StartCoroutine(FadeText(0f, 1f, fadeInDuration));

        // Hold
        yield return new WaitForSeconds(displayDuration);

        // Fade out
        yield return StartCoroutine(FadeText(1f, 0f, fadeOutDuration));

        // Load next scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Verifying scene queue is needed
    }

    private IEnumerator FadeText(float startAlpha, float endAlpha, float duration)
    {
        float elapsed = 0f;
        Color c = introText.color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            introText.color = new Color(c.r, c.g, c.b, alpha);
            yield return null;
        }

        introText.color = new Color(c.r, c.g, c.b, endAlpha);
    }
}
