using UnityEngine;
using TMPro;

public class LeaderboardManager : MonoBehaviour
{
    [SerializeField] private Transform scrollViewContent; // Conteneur pour les éléments de score
    [SerializeField] private GameObject scoreItemPrefab; // Prefab d'élément de score

    private void Start()
    {
        LoadScores();
    }

    private void LoadScores()
    {
        string scoreKey = "Leaderboard";
        string currentData = PlayerPrefs.GetString(scoreKey, "");

        if (string.IsNullOrEmpty(currentData))
        {
            Debug.Log("Aucun timer enregistré !");
            return;
        }

        string[] entries = currentData.Split('|');
        foreach (string entry in entries)
        {
            string[] data = entry.Split(':');
            if (data.Length == 2)
            {
                string playerName = data[0];
                string score = data[1];

                CreateScoreItem(playerName, score);
            }
        }
    }

    private void CreateScoreItem(string playerName, string score)
    {
        GameObject newScoreItem = Instantiate(scoreItemPrefab, scrollViewContent);
        TextMeshProUGUI scoreText = newScoreItem.GetComponent<TextMeshProUGUI>();
        if (scoreText != null)
        {
            scoreText.text = $"{playerName} : {score}";
        }
    }
}
