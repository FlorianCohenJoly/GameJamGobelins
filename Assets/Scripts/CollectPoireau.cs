using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectPoireau : MonoBehaviour
{
    // Compteur des poireaux
    private int m_Poireau = 0;

    public int Poireau => m_Poireau;

    // Références UI
    [SerializeField]
    private TextMeshProUGUI m_PoireauText;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Vérifie si l'objet collecté est un poireau
        if (other.CompareTag("Poireau"))
        {
            IncrementPoireau();
            Debug.Log("Poireau collecté !");
            Destroy(other.gameObject); // Détruit l'objet poireau collecté
        }
    }

    // Méthode pour incrémenter les poireaux
    public void IncrementPoireau()
    {
        m_Poireau++;
        Debug.Log("Poireau collecté ! Total : " + m_Poireau);
        UpdatePoireauUI();
    }

    // Mise à jour des textes UI
    private void UpdatePoireauUI()
    {
        if (m_PoireauText != null)
        {
            m_PoireauText.text = "Poireaux: " + m_Poireau;
        }
    }
}
