using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement m_Movement;

    [SerializeField]
    private MusicManager m_MusicManager;

    void Start()
    {
        // S'abonner à l'événement OnTimeReached du MusicManager
        if (m_MusicManager != null)
        {
            m_MusicManager.OnTimeReached += HandleTimeReached;
        }
    }

    void OnDestroy()
    {
        // Se désabonner de l'événement pour éviter les erreurs si l'objet est détruit
        if (m_MusicManager != null)
        {
            m_MusicManager.OnTimeReached -= HandleTimeReached;
        }
    }

    private void HandleTimeReached(float actionTime)
    {
        // C'est ici que tu peux gérer les actions au moment où l'événement est déclenché
        Debug.Log($"Temps d'action atteint à {actionTime}s !");
    }

    void Update()
    {
        float dir = 1; // Le joueur avance toujours vers la droite.
        m_Movement.Move(dir);

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Debug.Log("Jump");
            m_Movement.Jump();

            // Vérifie si c'est le bon moment pour effectuer l'action en fonction des temps d'action définis
            foreach (var actionTime in m_MusicManager.actionTimes)
            {
                if (m_MusicManager.IsActionTiming(actionTime))
                {
                    Debug.Log("Action réussie !");
                    m_Movement.BoostSpeed();
                    break; // Une seule action par saut
                }
            }
        }
    }
}
