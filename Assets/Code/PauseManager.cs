using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public Button pauseButton, reprendreButton, quitterButton;

    void Start()
    {
        pauseMenu.SetActive(false);
        pauseButton.onClick.AddListener(Pause);
        reprendreButton.onClick.AddListener(Reprendre);
        quitterButton.onClick.AddListener(QuitterVersMenu);
    }

    void Pause()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }

    void Reprendre()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    void QuitterVersMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}
