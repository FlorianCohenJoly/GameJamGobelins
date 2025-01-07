using UnityEngine;
using System;

public class MusicManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource m_AudioSource;

    public event Action OnBeat; // Déclaration de l'événement OnBeat

    private float currentTime;
    private float bpm = 120f;  // BPM fixe (tu peux ajuster si nécessaire)
    private float beatInterval; // Intervalle entre les battements
    private float lastBeatTime = 0f; // Temps du dernier battement

    private void Start()
    {
        if (m_AudioSource == null)
        {
            Debug.LogError("AudioSource est manquant dans MusicManager!");
            return;
        }

        beatInterval = 60f / bpm; // Calcul de l'intervalle entre les battements en secondes
    }

    private void Update()
    {
        if (m_AudioSource.isPlaying)
        {
            currentTime = m_AudioSource.time;

            // Vérifie si un nouveau battement a eu lieu (avec une tolérance pour le battement)
            if (currentTime >= lastBeatTime + beatInterval)
            {
                lastBeatTime = currentTime;
                OnBeat?.Invoke();
                //Debug.Log("Beat détecté !");
            }
        }
    }

    // Calcul du BPM actuel
    public float GetBPM()
    {
        if (m_AudioSource == null) return 120f;
        return bpm; // Retourne le BPM fixé ou calculé
    }

    // Vérifie si le joueur saute au bon moment
    public bool IsInPerfectTiming(float perfectJumpTime)
    {
        float beatInterval = 60f / bpm;
        return Mathf.Abs(currentTime % beatInterval) <= (perfectJumpTime / 2f); // Vérifie si le saut est au bon moment
    }
}
