using UnityEngine;
using UnityEngine.UI;

public class VisualCueManager : MonoBehaviour
{
    [SerializeField]
    private MusicManager m_MusicManager;

    [SerializeField]
    private Image visualCue; // L'indicateur visuel à afficher
    private bool cueActive = false;

    private void Start()
{
    // Désactive l'image au départ
    if (visualCue != null)
    {
        visualCue.enabled = false;
    }

    // Abonne-toi à l'événement du MusicManager pour détecter le moment où afficher l'indicateur
    if (m_MusicManager != null)
    {
        m_MusicManager.OnTimeReached += ShowVisualCue;
    }
}


    private void ShowVisualCue(float actionTime)
{
    // Vérifie si l'indicateur visuel existe et si l'image est bien définie
    if (visualCue != null)
    {
        visualCue.enabled = true; // Affiche l'indicateur
        Debug.Log($"Aide visuelle affichée pour l'action à {actionTime}s !");
        Invoke("HideVisualCue", 1f); // Cache l'indicateur après 1 seconde
    }
}


    private void HideVisualCue()
    {
        // Cache l'indicateur
        if (visualCue != null)
        {
            visualCue.enabled = false;
            cueActive = false;
            Debug.Log("Aide visuelle cachée !");
        }
    }
}
