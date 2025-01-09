using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoEndSceneLoader : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Référence au VideoPlayer
    public string sceneToLoad = "SampleScene"; // Nom de la scène à charger

    void Start()
{
    // Charger la vidéo depuis StreamingAssets
    string videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, "intro_scene.mp4");
    videoPlayer.url = videoPath;

    // Associer l'événement à la méthode OnVideoEnd
    videoPlayer.loopPointReached += OnVideoEnd;

    // Préparer et démarrer la lecture
    videoPlayer.Prepare();
    videoPlayer.prepareCompleted += vp => videoPlayer.Play();
}


    void OnVideoEnd(VideoPlayer vp)
    {
        // Charger la scène spécifiée
        SceneManager.LoadScene(sceneToLoad);
    }
}
