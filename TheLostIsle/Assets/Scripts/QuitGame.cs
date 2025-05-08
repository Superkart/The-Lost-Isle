using UnityEngine;

public class QuitGame : MonoBehaviour
{
    private void Update()
    {
        if(Application.isPlaying && Input.GetKeyDown(KeyCode.Escape)){
            Application.Quit();
        }
    }
}
