using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject menuButtons;
    public Button jouerButton, creditsButton, quitterButton;

    void Start()
    {
        // Ajouter les listeners
        jouerButton.onClick.AddListener(Jouer);
        creditsButton.onClick.AddListener(AfficherCredits);
        quitterButton.onClick.AddListener(Quitter);
    }

    void Jouer()
    {
        SceneManager.LoadScene("Game");
    }

    void AfficherCredits()
    {
        SceneManager.LoadScene("Credit");
    }


    void Quitter()
    {
        Application.Quit();
        Debug.Log("Quitter (ne fonctionne qu’en build)");
    }
}
