using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Assurez-vous d'inclure TextMeshPro pour afficher le score

public class WinManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUI; // UI Game Over
    [SerializeField] private GameObject winUI; // UI de victoire
    [SerializeField] private GameObject mainUi; // UI principale
    private bool isGameOver = false; // Pour éviter les déclenchements multiples
    private bool isWin = false; // Pour éviter plusieurs victoires

    private TimerManager timerManager;

    private void Awake()
    {
        // Récupérer la référence au TimerManager
        timerManager = FindObjectOfType<TimerManager>(); 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isGameOver && other.CompareTag("Player")) // Vérifie si c'est le joueur pour le Game Over
        {
            isGameOver = true; // Déclenchement unique
            TriggerGameOver();
        }

        if (!isWin && other.CompareTag("WinCollider")) // Vérifie si le joueur atteint la zone de victoire
        {
            isWin = true; // Déclenchement unique pour la victoire
            TriggerWin();
        }
    }

    private void TriggerGameOver()
    {
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true); // Affiche l'UI de Game Over
            mainUi.SetActive(false); // Désactive l'UI principale
        }

        Time.timeScale = 0f; // Arrête le jeu
        Debug.Log("Game Over : La caméra a rattrapé le joueur !");
    }

    private void TriggerWin()
    {
        if (winUI != null)
        {
            winUI.SetActive(true); // Affiche l'UI de victoire
            mainUi.SetActive(false); // Désactive l'UI principale
        }

        // Récupérer le temps écoulé du TimerManager
        if (timerManager != null)
        {
            float finalScore = timerManager.GetElapsedTime();
            // Sauvegarder le score dans PlayerPrefs
            PlayerPrefs.SetFloat("PlayerScore", finalScore); 
            PlayerPrefs.Save(); // Assurez-vous de sauvegarder dans PlayerPrefs

            // Afficher un message de score dans la console
            Debug.Log("Score Sauvegardé : " + finalScore);
        }

        Time.timeScale = 0f; // Arrête le jeu
        Debug.Log("Victoire : Le joueur a atteint la fin du niveau !");
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
