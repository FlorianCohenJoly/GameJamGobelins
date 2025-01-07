using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform m_Player;
    [SerializeField]
    private Vector3 m_Offset;
    [SerializeField]
    private float m_Smoothing;

    [SerializeField]
    private float slowMotionDuration = 0.5f; // Durée du ralentissement
    [SerializeField]
    private float slowMotionFactor = 0.8f; // Facteur de ralentissement (moins que 1 = ralentissement)

    [SerializeField]
    private float shakeDuration = 0.2f; // Durée du shake de la caméra
    [SerializeField]
    private float shakeStrength = 1f; // Force du shake
    [SerializeField]
    private int shakeVibrato = 10; // Nombre de secousses

    private float normalSpeed = 1f; // Vitesse normale de la caméra

    public void ApplySlowMotion()
    {
        Debug.Log("Effet de ralentissement de la caméra activé !");
        
        // Ralentit la caméra
        DOTween.To(() => normalSpeed, x => Time.timeScale = x, slowMotionFactor, slowMotionDuration)
               .OnComplete(() =>
               {
                   // Retour à la vitesse normale après le ralentissement
                   DOTween.To(() => Time.timeScale, x => Time.timeScale = x, 1f, slowMotionDuration);
                   Debug.Log("Ralentissement terminé, retour à la vitesse normale.");
               });

        // Applique le shake de la caméra
        ApplyCameraShake();
    }

    private void ApplyCameraShake()
    {
        // Effectue un shake de la caméra pour accentuer le ralentissement
        // Nous n'affectons que la caméra, sans toucher à la position du joueur
        transform.DOKill(); // Annule les shakes précédents avant de lancer un nouveau shake
        transform.DOShakePosition(shakeDuration, shakeStrength, shakeVibrato, 90f, false, true)
                 .OnStart(() => Debug.Log("Secousse de la caméra activée !"));
    }

    void Update()
    {
        Vector3 target = m_Player.position + m_Offset;
        float distance = Mathf.Clamp(Vector2.Distance(target, transform.position), 0, 2);
        target = Vector3.Lerp(transform.position, target, m_Smoothing * Time.deltaTime * distance);
        target.y = Mathf.Max(2, target.y);

        transform.position = target;
    }
}
