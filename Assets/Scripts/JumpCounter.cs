using UnityEngine;
using UnityEngine.UI;
using TMPro;  // Assurez-vous d'inclure le namespace TextMeshPro

public class JumpCounter : MonoBehaviour
{
    // Compteur des bons sauts
    private int m_GoodJumps = 0;

    public int GoodJumps => m_GoodJumps;

    // Références UI
    [SerializeField]
    private TextMeshProUGUI m_GoodJumpText;

    // Méthode pour incrémenter les bons sauts
    public void IncrementGoodJump()
    {
        m_GoodJumps++;
        Debug.Log("Bon saut ! Total : " + m_GoodJumps);
        UpdateJumpUI();
    }

    // Mise à jour des textes UI
    private void UpdateJumpUI()
    {
        if (m_GoodJumpText != null)
        {
            m_GoodJumpText.text = "Bons sauts: " + m_GoodJumps;
        }
    }
}
