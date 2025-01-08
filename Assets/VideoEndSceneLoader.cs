using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoEndSceneLoader : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Référence au VideoPlayer
    public string sceneToLoad = "SampleScene"; // Nom de la scène à charger

    void Start()
    {
        // Associer l'événement à la méthode OnVideoEnd
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // Charger la scène spécifiée
        SceneManager.LoadScene(sceneToLoad);
    }
}
