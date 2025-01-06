using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigidBody;

    [SerializeField, Range(0f, 1f)]
    private float m_Deceleration;

    [SerializeField]
    private float m_MoveSpeed, m_JumpForce;

    private float _dir = 0;

    private bool _isGrounded = false;

    private bool _isJumping = false;

    [SerializeField]
    private LayerMask m_GroundMask;

    private float _originalSpeed;
    private float _boostTimer = 0f;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _originalSpeed = m_MoveSpeed;
    }

    public void Move(float dir)
    {
        _dir = dir;
    }

    public void Jump()
    {
        if (_isGrounded)
            _isJumping = true;
    }

    public void BoostSpeed()
    {
        m_MoveSpeed += 2f; // Augmente la vitesse.
        _boostTimer = 2f;  // Durée du boost.
    }

    private void FixedUpdate()
    {
        if (_boostTimer > 0)
        {
            _boostTimer -= Time.fixedDeltaTime;
            if (_boostTimer <= 0)
            {
                m_MoveSpeed = _originalSpeed; // Réinitialise la vitesse.
            }
        }

        // Déplacement automatique
        _rigidBody.velocity = new Vector2(_dir * m_MoveSpeed, _rigidBody.velocity.y);

        if (_isJumping)
        {
            _isJumping = false;
            _rigidBody.AddForce(Vector2.up * m_JumpForce, ForceMode2D.Impulse);
        }
    }

    private void Update()
    {
        _isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.7f, m_GroundMask);
    }
}
