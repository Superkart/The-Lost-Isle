using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame() {
        Debug.Log("Starting Game...");
        SceneManager.LoadScene(1);
    }

    public void quitGame() {
        Debug.Log("Quitting...");
        Application.Quit();
    }

    public void getCredits() {
        Debug.Log("Displaying Credits...");
    }
}
