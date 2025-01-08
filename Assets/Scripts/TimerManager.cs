using UnityEngine;
using TMPro;  // Assurez-vous d'inclure le namespace TextMeshPro

public class TimerManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timerText;  // Texte affichant le temps en cours
    [SerializeField]
    private TextMeshProUGUI finalTimeText;  // Texte affichant le temps final
    [SerializeField]
    private GameObject endPanel;  // Panel de fin à afficher lors de la victoire/défaite

    private float timeElapsed = 0f;  // Temps écoulé depuis le début de la scène
    private bool isRunning = true;  // Contrôle si le timer doit continuer à fonctionner

    private void Start()
    {
        if (timerText == null)
        {
            Debug.LogError("Le texte du timer n'est pas assigné !");
        }

        if (endPanel != null)
        {
            endPanel.SetActive(false); // Désactive le panel de fin au début
        }
    }

    private void Update()
    {
        if (isRunning)
        {
            // Incrémente le temps écoulé en fonction du deltaTime (temps écoulé entre chaque frame)
            timeElapsed += Time.deltaTime;

            // Met à jour l'affichage du timer
            UpdateTimerDisplay();
        }
    }

    // Méthode pour mettre à jour l'affichage du timer en temps réel
    private void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            // Affiche le temps écoulé avec 2 chiffres après la virgule
            timerText.text = "Time : " + timeElapsed.ToString("F2");
        }
    }

    // Méthode pour arrêter le timer et afficher le temps final
    public void StopTimer()
    {
        isRunning = false; // Arrête le timer

        if (endPanel != null && finalTimeText != null)
        {
            endPanel.SetActive(true); // Affiche le panel de fin
            finalTimeText.text = "Final Time : " + timeElapsed.ToString("F2");
        }
    }

    // Méthode pour obtenir le temps écoulé (optionnel)
    public float GetElapsedTime()
    {
        return timeElapsed;
    }
}
