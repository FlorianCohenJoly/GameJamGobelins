using UnityEngine;
using System.Collections.Generic;

public class MusicManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource m_AudioSource;

    [SerializeField]
    public List<float> actionTimes; // Liste des temps où les actions doivent être effectuées

    [SerializeField]
    private float cueBeforeActionTime = 2f; // Le nombre de secondes avant l'action pour afficher l'indicateur visuel

    // Définition de l'événement pour notifier les autres scripts qu'il est temps d'effectuer l'action
    public delegate void TimeReachedHandler(float actionTime);
    public event TimeReachedHandler OnTimeReached;

    private void Update()
    {

        // Debug le temp
        Debug.Log(m_AudioSource.time);
        
        if (m_AudioSource == null) return;

        float currentTime = m_AudioSource.time;

        // Vérifier chaque actionTime dans la liste
        foreach (var actionTime in actionTimes)
        {
            // Afficher l'indicateur avant l'action
            if (currentTime >= actionTime - cueBeforeActionTime && currentTime <= actionTime)
            {
                // Déclencher l'événement si l'heure cible est atteinte
                OnTimeReached?.Invoke(actionTime);
                break; // Stopper la boucle après avoir trouvé la première action à effectuer
            }
        }
    }

    // Cette fonction est utilisée pour vérifier si c'est le moment de l'action
    public bool IsActionTiming(float actionTime)
    {
        if (m_AudioSource == null) return false;

        float currentTime = m_AudioSource.time;
        return currentTime >= actionTime - cueBeforeActionTime && currentTime <= actionTime;
    }
}
