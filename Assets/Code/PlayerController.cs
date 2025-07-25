using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("R�f�rences")]
    public CharacterController controller;
    public Animator animator;
    public Transform cameraTransform; // R�f�rence � la transform de la cam�ra

    [Header("Param�tres de Mouvement")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 720f; // Vitesse de rotation en degr�s par seconde

    void Start()
    {
        // Assigne automatiquement les composants s'ils ne sont pas d�finis dans l'inspecteur
        if (controller == null)
            controller = GetComponent<CharacterController>();
        if (animator == null)
            animator = GetComponent<Animator>();
        // Trouve la cam�ra principale si elle n'est pas assign�e
        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        // 1. R�cup�rer les entr�es du joueur
        float moveX = Input.GetAxisRaw("Horizontal"); // ZQSD ou fl�ches (gauche/droite)
        float moveZ = Input.GetAxisRaw("Vertical");   // ZQSD ou fl�ches (avant/arri�re)

        // 2. Calculer la direction du mouvement par rapport � la cam�ra
        // On r�cup�re la direction "avant" et "droite" de la cam�ra
        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        // On annule la composante Y pour ne pas se d�placer vers le haut ou le bas
        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();

        // On combine les directions de la cam�ra avec les entr�es du joueur
        Vector3 moveDirection = (camForward * moveZ + camRight * moveX).normalized;

        // 3. G�rer le mouvement et l'animation
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
            // Si le personnage ne bouge pas, on arr�te l'animation de course
            animator.SetBool("isRunning", false);
        }
    }
}