using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;  // Vitesse de base
    public float boostMultiplier = 1.05f;  // Multiplicateur de vitesse léger (accélération)
    public float slowMultiplier = 0.95f;  // Multiplicateur de ralentissement léger

    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Vérifie si le joueur est au sol pour pouvoir sauter
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.7f, LayerMask.GetMask("Ground"));

        // Contrôle du saut
        if (isGrounded && Input.GetKeyDown(KeyCode.Space)) 
        {
            Jump();
        }

        // Déplacement horizontal
        float moveInput = Input.GetAxis("Horizontal");
        Move(moveInput);
    }

    public void Move(float direction)
    {
        // Applique le mouvement horizontal sans toucher la vitesse verticale
        rb.velocity = new Vector2(direction * moveSpeed, rb.velocity.y);
    }

    public void Jump()
    {
        // Applique une force verticale pour le saut
        rb.AddForce(Vector2.up * 3f, ForceMode2D.Impulse);
    }

    public void BoostSpeed()
    {
        moveSpeed *= boostMultiplier;  // Accélération légère
        Debug.Log($"Vitesse augmentée à {moveSpeed} !");  // Log pour afficher la nouvelle vitesse
    }

    
}
