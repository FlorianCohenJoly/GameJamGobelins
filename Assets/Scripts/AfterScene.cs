using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AfterScene : MonoBehaviour
{
    public string sceneToLoad = "Menu"; // Remplacez par le nom de votre scène
    public float delay = 5f; // Délai avant de changer de scène (en secondes)

    void Start()
    {
        // Lancer la coroutine pour changer de scène après le délai
        StartCoroutine(ChangeSceneAfterDelay());
    }

    IEnumerator ChangeSceneAfterDelay()
    {
        // Attendre le délai spécifié
        yield return new WaitForSeconds(delay);

        // Charger la nouvelle scène
        SceneManager.LoadScene(sceneToLoad);
    }
}
