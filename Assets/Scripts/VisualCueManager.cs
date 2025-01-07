using UnityEngine;
using UnityEngine.UI;

public class VisualCueManager : MonoBehaviour
{
    [SerializeField]
    private MusicManager m_MusicManager;

    [SerializeField]
    private Image visualCue;

    private void Start()
    {
        if (m_MusicManager == null)
        {
            Debug.LogError("MusicManager manquant dans VisualCueManager !");
            return;
        }

        if (visualCue == null)
        {
            Debug.LogError("Aucune image assignée pour l'aide visuelle !");
            return;
        }

        // Initialisation : désactiver l'aide visuelle au départ
        visualCue.enabled = false;

        // Abonne-toi à l'événement OnBeat du MusicManager
        m_MusicManager.OnBeat += ShowVisualCue;
    }

    private void OnDestroy()
    {
        // Se désabonner de l'événement lors de la destruction
        if (m_MusicManager != null)
        {
            m_MusicManager.OnBeat -= ShowVisualCue;
        }
    }

    private void ShowVisualCue()
    {
        // Affiche l'indicateur visuel
        visualCue.enabled = true;
        Debug.Log("Aide visuelle affichée !");

        // Cache l'indicateur après un délai
        Invoke("HideVisualCue", 0.2f);  // Ajuste la durée d'affichage de l'indicateur
    }

    private void HideVisualCue()
    {
        // Cache l'indicateur visuel
        visualCue.enabled = false;
    }
}
