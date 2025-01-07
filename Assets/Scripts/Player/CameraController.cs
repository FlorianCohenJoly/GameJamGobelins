using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float m_Speed = 5f; // Vitesse de la caméra
    [SerializeField]
    private float m_SlowMotionFactor = 0.5f; // Facteur de ralentissement quand un double saut est effectué
    [SerializeField]
    private float m_SlowMotionDuration = 0.5f; // Durée du ralentissement

    [SerializeField]
    private float m_StopAtPositionX = 100f; // Position finale sur l'axe X (si tu veux que la caméra s'arrête quelque part)

    private float m_CurrentSpeed; // Vitesse actuelle de la caméra (sera modifiée quand un double saut est effectué)

    private bool m_IsInSlowMotion = false;

    void Start()
    {
        m_CurrentSpeed = m_Speed; // Initialise la vitesse de la caméra
        
    }

    void Update()
    {
        // Condition fictive pour détecter un double saut réussi (à adapter à ta logique de jeu)
        if (PlayerHasPerfectDoubleJump())
        {
            ApplySlowMotion();
        }

        // Avance la caméra sur l'axe X indépendamment du joueur
        if (transform.position.x < m_StopAtPositionX) // Optionnel : arrêter la caméra après une certaine position
        {
            transform.Translate(Vector3.right * m_CurrentSpeed * Time.deltaTime);
        }
    }

    public bool PlayerHasPerfectDoubleJump()
    {
        // Logique pour détecter un double saut parfait (à remplacer par ta logique)
        return false; // Retourne `true` si un double saut parfait est effectué
    }

    public void ApplySlowMotion()
    {
        if (!m_IsInSlowMotion)
        {
            m_IsInSlowMotion = true;
            m_CurrentSpeed = m_Speed * m_SlowMotionFactor; // Réduit la vitesse de la caméra

            // Retourne la vitesse à la normale après la durée du ralentissement
            DOVirtual.DelayedCall(m_SlowMotionDuration, () =>
            {
                m_CurrentSpeed = m_Speed; // Restaure la vitesse normale
                m_IsInSlowMotion = false;
            });
        }
    }
}
