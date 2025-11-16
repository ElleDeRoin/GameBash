using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public TMP_Text finalScoreText; // assign TMP text in Inspector

    private void Start()
    {
        if (ScoreManager.instance != null)
            finalScoreText.text = "Final Score: " + ScoreManager.instance.GetScore();
        else
            finalScoreText.text = "Final Score: 0";
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene"); // your main scene
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
