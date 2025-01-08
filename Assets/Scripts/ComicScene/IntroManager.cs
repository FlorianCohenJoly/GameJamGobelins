using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    [SerializeField] private float displayTime = 5f; // Temps en secondes avant de charger la prochaine scène
    [SerializeField] private string nextSceneName = "SampleScene"; // Nom de la scène à charger

    void Start()
    {
        // Lance un délai pour charger la prochaine scène
        Invoke("LoadNextScene", displayTime);
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
