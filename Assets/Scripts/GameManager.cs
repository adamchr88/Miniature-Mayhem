using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject winPanel;

    public FloodTimerSystem floodTimerSystem;
    public TapEffectsController tapEffectsController;

    public TextMeshProUGUI finalGameOverTimeText;
    public TextMeshProUGUI finalWinTimeText;

    private bool gameEnded = false;

    public AudioManager audioManager;
    void Start()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        if (winPanel != null)
        {
            winPanel.SetActive(false);
        }

        Time.timeScale = 1f;
    }

    void Update()
    {
        if (gameEnded && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    public void GameOver()
    {
        if (gameEnded) return;

        gameEnded = true;

        if (floodTimerSystem != null)
        {
            floodTimerSystem.StopSystem();
        }

        if (tapEffectsController != null)
        {
            tapEffectsController.StopEffects();
        }

        if (finalGameOverTimeText != null && floodTimerSystem != null)
        {
            finalGameOverTimeText.text = "You lasted: " + floodTimerSystem.GetFormattedFloodTime();
        }

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        Time.timeScale = 0f;

        if (audioManager != null)
        {
            audioManager.StopFloodSound();
        }
    }

    public void WinGame()
    {
        if (gameEnded) return;

        gameEnded = true;

        if (floodTimerSystem != null)
        {
            floodTimerSystem.StopSystem();
        }

        if (tapEffectsController != null)
        {
            tapEffectsController.StopEffects();
        }

        if (finalWinTimeText != null && floodTimerSystem != null)
        {
            finalWinTimeText.text = "Flood active for: " + floodTimerSystem.GetFormattedFloodTime();
        }

        if (winPanel != null)
        {
            winPanel.SetActive(true);
        }

        Time.timeScale = 0f;

        if (audioManager != null)
        {
            audioManager.StopFloodSound();
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}