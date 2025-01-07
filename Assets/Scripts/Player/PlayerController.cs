using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement m_Movement;

    [SerializeField]
    private MusicManager m_MusicManager;

    [SerializeField]
    private CameraController m_CameraController; // Contrôle de la caméra

    private float perfectJumpTime = 60f / 120f; // Le temps entre chaque battement pour un BPM de 120

    void Update()
    {
        float dir = 1; // Le joueur avance toujours vers la droite
        m_Movement.Move(dir);

        if (Input.GetKeyDown(KeyCode.Space)) // Le joueur appuie pour sauter
        {
            //Debug.Log("Saut tenté !");
            m_Movement.Jump(); // Force de saut de base

            // Vérifie si le joueur saute à un moment précis
            if (m_MusicManager != null && m_MusicManager.IsInPerfectTiming(perfectJumpTime))
            {
                //Debug.Log("Saut en rythme parfait !");
                m_CameraController.ApplySlowMotion(); // Ralentit la caméra pour chaque saut parfait
            }
            else
            {
                //Debug.Log("Saut hors timing.");
            }
        }
    }
}
