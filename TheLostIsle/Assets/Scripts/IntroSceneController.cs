using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections; // Only needed since showing text
using System.Collections.Generic;

public class IntroSceneController : MonoBehaviour
{
    public TextMeshProUGUI introText;
    public float displayTime = 10f;

    void Start()
    {

        // Update the on-screen text
        introText.text = 
        "You find yourself stranded in this strange place. In the distance, you see a strange figure.\n" +
        "It doesn't seem very friendly... you should leave as soon as you can.\n" +
        "Find the fuel hidden nearby, while keeping up your O2 levels, and escape this place.";

        StartCoroutine(ContinueToGame());
    }

    private IEnumerator ContinueToGame()
    {
        yield return new WaitForSeconds(displayTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Verifying scene queue is needed
    }
}
