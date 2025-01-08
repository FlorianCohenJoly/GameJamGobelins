using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUI; // UI Game Over
    private bool isGameOver = false; // Pour éviter les déclenchements multiples

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isGameOver && other.CompareTag("Player")) // Vérifie si c'est le joueur
        {
            isGameOver = true; // Déclenchement unique
            TriggerGameOver();
        }
    }

    private void TriggerGameOver()
    {
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true); // Affiche l'UI
        }

        Time.timeScale = 0f; // Arrête le jeu
        Debug.Log("Game Over : La caméra a rattrapé le joueur !");
    }
}
