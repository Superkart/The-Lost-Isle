using UnityEngine;
using System.Collections; // Only needed if you're showing text
using System.Collections.Generic;

using UnityEngine.SceneManagement;
using TMPro;


public class IntroSceneLoader : MonoBehaviour
{
    public float waitTime = 10f;
    public string sceneToLoad = "ForestScene"; // Replace with your actual scene name
    public TextMeshProUGUI introText; // Optional, only if showing a message

    void Start()
    {
        if (introText != null)
            introText.text = "You find yourself in this new area, time to explore!";
        
        StartCoroutine(LoadNextSceneAfterDelay());
    }

    private IEnumerator LoadNextSceneAfterDelay()
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(sceneToLoad);
    }
}
