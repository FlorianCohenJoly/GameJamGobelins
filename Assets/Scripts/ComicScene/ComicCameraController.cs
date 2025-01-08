using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ComicCameraController : MonoBehaviour
{
    public Transform[] casePositions; // Liste des positions des cases
    public float zoomLevel = 5f; // Niveau de zoom (plus petit = plus proche)
    public float moveSpeed = 2f; // Vitesse de déplacement
    public float zoomSpeed = 2f; // Vitesse de zoom
    public float delayBetweenCases = 2f; // Temps d'attente entre les cases

    [SerializeField] private string nextSceneName = "SampleScene"; // Nom de la scène à charger

    private Camera cam;
    private int currentCaseIndex = 0;

    void Start()
    {
        cam = Camera.main; // Récupère la caméra principale
        if (casePositions.Length > 0)
        {
            MoveToCase(0); // Commence à la première case
        }
    }

    void MoveToCase(int index)
    {
        if (index < casePositions.Length)
        {
            currentCaseIndex = index;
            Transform target = casePositions[index];

            cam.transform.DOMove(target.position, moveSpeed);
            cam.DOOrthoSize(zoomLevel, zoomSpeed).OnComplete(() =>
            {
                if (currentCaseIndex + 1 < casePositions.Length)
                {
                    Invoke("MoveToNextCase", delayBetweenCases);
                }
                else
                {
                    // Si c'est la dernière case, charge la prochaine scène
                    Invoke("LoadNextScene", delayBetweenCases);
                }
            });
        }
    }

    void MoveToNextCase()
    {
        MoveToCase(currentCaseIndex + 1);
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
