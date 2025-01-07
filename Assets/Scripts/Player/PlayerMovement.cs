using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigidBody;

    [SerializeField]
    private float m_MoveSpeed, m_Deceleration, m_JumpForce;

    private float _dir = 0;
    private bool _isJumping = false;

    private bool _isGrounded = false;
    private bool _canDoubleJump = false;
    private bool _isTouchingWall = false;
    private bool _isPerfectDoubleJump = false;

    [SerializeField]
    private LayerMask m_GroundMask; // Masque pour détecter le sol
    [SerializeField]
    private LayerMask m_WallMask; // Masque pour détecter les murs

    [SerializeField]
    private float m_GravityScale = 3f; // Gravité par défaut
    [SerializeField]
    private float m_RiseGravityScale = 1.5f; // Gravité pendant l'ascension
    [SerializeField]
    private float m_WallCheckDistance = 0.1f; // Distance de détection des murs
    [SerializeField]
    private Vector2 m_BoxSize = new Vector2(0.5f, 1f); // Taille de la boîte pour le BoxCast

    private JumpCounter m_JumpCounter;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _rigidBody.gravityScale = m_GravityScale;
        m_JumpCounter = FindObjectOfType<JumpCounter>(); // Référence au gestionnaire de sauts
    }

    public void Move(float dir)
    {
        _dir = dir;
    }

    public void Jump()
    {
        if (_isGrounded) // Saut simple
        {
            _isJumping = true;
            _canDoubleJump = true; // Permet un double saut après le saut initial
        }
        else if (_canDoubleJump) // Double saut
        {
            _isJumping = true;
            _canDoubleJump = false; // Désactive le double saut après son utilisation

            // Vérification si le double saut est "parfait"
            _isPerfectDoubleJump = IsPerfectDoubleJump();
        }
    }

    private bool IsPerfectDoubleJump()
    {
        // Détermine si le double saut est "parfait" (basé sur la vitesse verticale)
        return _rigidBody.velocity.y > 0.5f && _rigidBody.velocity.y < 3f;
    }

    private void FixedUpdate()
    {
        // Vérifie si le joueur touche un mur
        _isTouchingWall = IsTouchingWall();

        // Si le joueur touche un mur, bloque le mouvement horizontal
        if (_isTouchingWall)
        {
            _rigidBody.velocity = new Vector2(0, _rigidBody.velocity.y);
        }
        else
        {
            // Applique le mouvement horizontal
            if (Mathf.Abs(_dir) > 0.01f)
            {
                _rigidBody.velocity = new Vector2(_dir * m_MoveSpeed, _rigidBody.velocity.y);
            }
            else
            {
                _rigidBody.velocity = new Vector2(_rigidBody.velocity.x * m_Deceleration, _rigidBody.velocity.y);
            }
        }

        // Applique la force de saut
        if (_isJumping)
        {
            _isJumping = false;
            _rigidBody.AddForce(Vector2.up * m_JumpForce, ForceMode2D.Impulse);
        }

        // Gravité ajustée pendant l'ascension ou la descente
        _rigidBody.gravityScale = _rigidBody.velocity.y > 0 ? m_RiseGravityScale : m_GravityScale;
    }

    private void Update()
    {
        // Vérifie si le joueur est au sol
        _isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, m_GroundMask);

        // Si le joueur est au sol, réinitialiser le double saut
        if (_isGrounded)
        {
            _canDoubleJump = true;
            _isPerfectDoubleJump = false; // Réinitialisation
        }

        // Détection du saut par espace
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        // Si le double saut est parfait, on informe le gestionnaire de sauts
        if (_isPerfectDoubleJump)
        {
            m_JumpCounter.IncrementGoodJump(); // Incrémente le compteur de sauts parfaits
            _isPerfectDoubleJump = false; // Réinitialisation
        }
    }

    private bool IsTouchingWall()
    {
        // Centre de la BoxCast
        Vector2 origin = transform.position;
        // Direction de la BoxCast (droite ou gauche)
        Vector2 direction = Vector2.right * Mathf.Sign(_dir);

        // Effectue le BoxCast
        RaycastHit2D hit = Physics2D.BoxCast(origin, m_BoxSize, 0f, direction, m_WallCheckDistance, m_WallMask);

        // Dessine la BoxCast dans la scène pour visualisation
        Debug.DrawRay(origin + new Vector2(-m_BoxSize.x / 2, -m_BoxSize.y / 2), direction * m_WallCheckDistance, Color.green); // Bas gauche
        Debug.DrawRay(origin + new Vector2(-m_BoxSize.x / 2, m_BoxSize.y / 2), direction * m_WallCheckDistance, Color.green); // Haut gauche
        Debug.DrawRay(origin + new Vector2(m_BoxSize.x / 2, -m_BoxSize.y / 2), direction * m_WallCheckDistance, Color.green); // Bas droite
        Debug.DrawRay(origin + new Vector2(m_BoxSize.x / 2, m_BoxSize.y / 2), direction * m_WallCheckDistance, Color.green); // Haut droite

        return hit.collider != null;
    }
}
