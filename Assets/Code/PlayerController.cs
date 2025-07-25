using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Références")]
    public CharacterController controller;
    public Animator animator;
    public Transform cameraTransform; // Référence à la transform de la caméra

    [Header("Paramètres de Mouvement")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 720f; // Vitesse de rotation en degrés par seconde

    void Start()
    {
        // Assigne automatiquement les composants s'ils ne sont pas définis dans l'inspecteur
        if (controller == null)
            controller = GetComponent<CharacterController>();
        if (animator == null)
            animator = GetComponent<Animator>();
        // Trouve la caméra principale si elle n'est pas assignée
        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        // 1. Récupérer les entrées du joueur
        float moveX = Input.GetAxisRaw("Horizontal"); // ZQSD ou flèches (gauche/droite)
        float moveZ = Input.GetAxisRaw("Vertical");   // ZQSD ou flèches (avant/arrière)

        // 2. Calculer la direction du mouvement par rapport à la caméra
        // On récupère la direction "avant" et "droite" de la caméra
        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        // On annule la composante Y pour ne pas se déplacer vers le haut ou le bas
        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();

        // On combine les directions de la caméra avec les entrées du joueur
        Vector3 moveDirection = (camForward * moveZ + camRight * moveX).normalized;

        // 3. Gérer le mouvement et l'animation
        if (moveDirection.magnitude >= 0.1f)
        {
            // --- Mouvement ---
            controller.Move(moveDirection * moveSpeed * Time.deltaTime);

            // --- Rotation ---
            // Fait tourner le personnage pour qu'il regarde dans la direction du mouvement
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // --- Animation ---
            animator.SetBool("isRunning", true);
        }
        else
        {
            // Si le personnage ne bouge pas, on arrête l'animation de course
            animator.SetBool("isRunning", false);
        }
    }
}