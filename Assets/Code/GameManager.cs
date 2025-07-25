using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro; // ÉTAPE 1 : On importe TextMeshPro
using UnityEngine.UI; // On garde ça pour les boutons (Button)

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game Settings")]
    public float gameTime = 45f;
    private int totalCollectibles;

    [Header("UI References (TextMeshPro)")]
    // ÉTAPE 2 : On change Text en TextMeshProUGUI
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText; // Ajout pour afficher le score
    public GameObject gameOverPanel;
    public GameObject victoryPanel;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI victoryText;
    public Button retryButton;
    public Button mainMenuButton;

    [Header("Game State")]
    private float currentTime;
    private int collectedItems = 0;
    private bool isGameActive = false;

    // ... (le reste du script ne change pas beaucoup)

    void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }

    void Start()
    {
        SetupUI();
        StartGame();
    }

    void Update()
    {
        if (isGameActive)
        {
            UpdateTimer();
        }
    }

    void SetupUI()
    {
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (victoryPanel != null) victoryPanel.SetActive(false);

        if (retryButton != null)
        {
            retryButton.onClick.RemoveAllListeners();
            retryButton.onClick.AddListener(RestartGame);
        }

        if (mainMenuButton != null)
        {
            mainMenuButton.onClick.RemoveAllListeners();
            mainMenuButton.onClick.AddListener(GoToMainMenu);
        }
    }

    public void StartGame()
    {
        currentTime = gameTime;
        collectedItems = 0;
        isGameActive = true;

        GameObject[] collectibleObjects = GameObject.FindGameObjectsWithTag("Collectible");
        totalCollectibles = collectibleObjects.Length;

        UpdateTimerDisplay();
        UpdateScoreDisplay(); // On affiche le score de départ (0 / total)
    }

    void UpdateTimer()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
        }
        else
        {
            currentTime = 0;
            GameOver();
        }
        UpdateTimerDisplay();
    }

    void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(currentTime / 60);
            int seconds = Mathf.FloorToInt(currentTime % 60);
            timerText.text = string.Format("Temps: {0:00}:{1:00}", minutes, seconds);
        }
    }

    // NOUVELLE FONCTION pour afficher le score
    void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = "Objets : " + collectedItems + " / " + totalCollectibles;
        }
    }

    public void CollectItem(GameObject item)
    {
        if (!isGameActive) return; // Sécurité pour ne pas collecter après la fin du jeu

        item.SetActive(false);
        collectedItems++;

        UpdateScoreDisplay(); // On met à jour l'affichage du score

        if (collectedItems >= totalCollectibles)
        {
            Victory();
        }
    }

    void GameOver()
    {
        isGameActive = false;
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            if (gameOverText != null)
            {
                gameOverText.text = "Temps écoulé !\nVous avez collecté " + collectedItems + "/" + totalCollectibles + " objets";
            }
        }
    }

    void Victory()
    {
        isGameActive = false;
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(true);
            if (victoryText != null)
            {
                victoryText.text = "Félicitations !\nVous avez collecté tous les objets !";
            }
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Menu"); // Assure-toi d'avoir une scène nommée "Menu"
    }
}