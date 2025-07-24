using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float timeLeft = 45f;
    public Text timerText;
    public GameObject victoryPanel;
    public GameObject gameOverPanel;
    public int totalItems;
    private int collectedItems;

    private bool gameEnded = false;

    void Update()
    {
        if (gameEnded) return;

        timeLeft -= Time.deltaTime;
        timerText.text = "Temps restant : " + Mathf.Ceil(timeLeft);

        if (timeLeft <= 0)
        {
            EndGame(false);
        }
    }

    public void CollectItem()
    {
        collectedItems++;
        if (collectedItems >= totalItems)
        {
            EndGame(true);
        }
    }

    void EndGame(bool won)
    {
        gameEnded = true;
        if (won)
        {
            victoryPanel.SetActive(true);
        }
        else
        {
            gameOverPanel.SetActive(true);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Jeu");
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
