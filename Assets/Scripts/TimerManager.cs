using UnityEngine;
using TMPro;  // Assurez-vous d'inclure le namespace TextMeshPro

public class TimerManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timerText;  // Référence à l'objet TextMeshPro où le timer sera affiché

    private float timeElapsed = 0f;  // Temps écoulé depuis le début de la scène

    private void Start()
    {
        if (timerText == null)
        {
            Debug.LogError("Le texte du timer n'est pas assigné !");
            return;
        }
    }

    private void Update()
    {
        // Incrémente le temps écoulé en fonction du deltaTime (temps écoulé entre chaque frame)
        timeElapsed += Time.deltaTime;

        // Met à jour l'affichage du timer
        UpdateTimerDisplay();
    }

    // Méthode pour mettre à jour l'affichage du timer
    private void UpdateTimerDisplay()
    {
        // Affiche le temps écoulé avec 2 chiffres après la virgule
        timerText.text = "Time : " + timeElapsed.ToString("F2");
    }
}
