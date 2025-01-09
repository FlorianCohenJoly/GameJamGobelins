using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class WinManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject winUI;
    [SerializeField] private TMP_InputField nameInputField; // InputField pour entrer le nom
    private bool isGameOver = false;
    private bool isWin = false;

    private TimerManager timerManager;

    private void Awake()
    {
        timerManager = FindObjectOfType<TimerManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isGameOver && other.CompareTag("Player"))
        {
            isGameOver = true;
            TriggerGameOver();
        }

        if (!isWin && other.CompareTag("WinCollider"))
        {
            isWin = true;
            TriggerWin();
        }
    }

    private void TriggerGameOver()
    {
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }
        Time.timeScale = 0f;
    }

    private void TriggerWin()
    {
        if (timerManager != null)
        {
            timerManager.StopTimer(); // Arrête le timer
        }

        
        Time.timeScale = 0f; // Arrête le jeu
    }

    public void OnNameEntered()
    {
        // Récupérer le nom du joueur
        string playerName = nameInputField.text;

        if (string.IsNullOrEmpty(playerName))
        {
            Debug.LogWarning("Le nom du joueur est vide !");
            return;
        }

        // Sauvegarder le nom et le score dans PlayerPrefs
        float finalScore = timerManager.GetElapsedTime();
        SavePlayerScore(playerName, finalScore);

        // Passer à la scène de menu
        SceneManager.LoadScene("Menu");
    }

    private void SavePlayerScore(string playerName, float score)
    {
        // Enregistrer le score sous la forme "Nom:Score"
        string scoreKey = "Leaderboard";
        string currentData = PlayerPrefs.GetString(scoreKey, "");
        string newEntry = $"{playerName}:{score}";
        string updatedData = string.IsNullOrEmpty(currentData) ? newEntry : $"{currentData}|{newEntry}";
        PlayerPrefs.SetString(scoreKey, updatedData);
        PlayerPrefs.Save();
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
