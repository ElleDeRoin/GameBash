using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene"); // Replace with your main game scene name
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}