// Dans ton script "Collectible.cs"
using UnityEngine;

public class Collectible : MonoBehaviour
{
    // Tu peux garder ton code pour la rotation et le flottement ici...
    [Header("Visual Effects")]
    public float rotationSpeed = 50f;
    public float floatSpeed = 2f;
    public float floatAmplitude = 0.5f;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Rotation
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        // Flottement
        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
    // FIN de ton code visuel

    // C'EST LA PARTIE À CHANGER
    void OnTriggerEnter(Collider other)
    {
        // On vérifie toujours si c'est le joueur
        if (other.CompareTag("Player"))
        {
            // Au lieu de se détruire, on appelle le GameManager !
            // GameManager.Instance permet d'accéder à ton script depuis n'importe où.
            if (GameManager.Instance != null)
            {
                // On demande au GameManager de gérer la collecte de cet objet.
                GameManager.Instance.CollectItem(this.gameObject);
            }
        }
    }
}