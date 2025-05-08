using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager2 : MonoBehaviour
{
    [SerializeField] private GameObject GameLostPanel;
    [SerializeField] private GameObject GameWonPanel;


    private void Start()
    {
        GameLostPanel.SetActive(false);
        GameWonPanel.SetActive(false);
    }


    public void ShowGameLostPanel()
    {
        ShowMouseOnCanvas();
        GameLostPanel.SetActive(true);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    private void ShowMouseOnCanvas()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
    }

    public void ShowGameWonPanel() ///////////////////// CODE HERE TO MAKE WINNING CONDTION /////////////////////////////////////////
    {
        Debug.Log("Game Won!");
        ShowMouseOnCanvas();
        GameWonPanel.SetActive(true);
    }
}