using System.Collections;
using System.Collections.Generic;
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

    [SerializeField]
    private LayerMask m_GroundMask;  // Masque pour détecter le sol

    [SerializeField]
    private float m_GravityScale = 3f; // Gravité par défaut pendant la descente

    [SerializeField]
    private float m_RiseGravityScale = 1.5f; // Gravité pendant l'ascension (plus faible)

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _rigidBody.gravityScale = m_GravityScale; // Assure une gravité initiale par défaut
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
        }
    }
    
    private void FixedUpdate()
    {
        // Applique le mouvement horizontal tout le temps, même pendant le saut
        if (Mathf.Abs(_dir) > 0.01f)
        {
            _rigidBody.velocity = new Vector2(_dir * m_MoveSpeed, _rigidBody.velocity.y);
        }
        else
        {
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x * m_Deceleration, _rigidBody.velocity.y);
        }

        // Applique la force de saut seulement si un saut est demandé
        if (_isJumping)
        {
            _isJumping = false;
            _rigidBody.AddForce(Vector2.up * m_JumpForce, ForceMode2D.Impulse);
        }

        // Si le joueur est en l'air (en train de monter), on réduit la gravité
        if (_rigidBody.velocity.y > 0)
        {
            _rigidBody.gravityScale = m_RiseGravityScale;
        }
        else if (_rigidBody.velocity.y < 0)
        {
            // Si le joueur descend, on augmente la gravité pour une descente plus rapide
            _rigidBody.gravityScale = m_GravityScale;
        }
    }

    private void Update()
    {
        // Vérifie si le joueur touche le sol (Raycast utilisant le LayerMask pour détecter les sols)
        _isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, m_GroundMask);

        // Si le joueur est au sol, réinitialiser les capacités de double saut
        if (_isGrounded)
        {
            _canDoubleJump = true;
        }

        // Détection du saut par appui sur la touche espace
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }
}
