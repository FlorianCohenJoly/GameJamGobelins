using UnityEngine;
using TMPro; // Assurez-vous d'inclure TextMeshPro

public class LeaderboardManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText; // Référence au texte où afficher le score dans la scène du menu
    [SerializeField] private Transform scrollViewContent; // Le conteneur pour la scroll view
    [SerializeField] private GameObject scoreItemPrefab; // Le prefab pour chaque item de score dans la scroll view

    private void Start()
    {
        // Récupérer le score sauvegardé
        float savedScore = PlayerPrefs.GetFloat("PlayerScore", 0f); // 0f comme valeur par défaut si aucune donnée n'est trouvée

        // Afficher le score récupéré dans le texte
        if (scoreText != null)
        {
            scoreText.text = "Dernier Score : " + savedScore.ToString("F2");
        }

        // Optionnel : Afficher ce score dans une ScrollView (liste)
        DisplayScoreInScrollView(savedScore);
    }

    private void DisplayScoreInScrollView(float score)
    {
        // Créez un nouvel item pour la ScrollView (ici une simple ligne avec le score)
        GameObject newScoreItem = Instantiate(scoreItemPrefab, scrollViewContent);
        TextMeshProUGUI newScoreText = newScoreItem.GetComponent<TextMeshProUGUI>();
        if (newScoreText != null)
        {
            newScoreText.text = "Score: " + score.ToString("F2");
        }
    }
}
