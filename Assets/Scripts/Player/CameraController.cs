using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform m_Player;
    [SerializeField]
    private Vector3 m_Offset;
    [SerializeField]
    private float m_Smoothing;

    void Update()
    {
        Vector3 target = m_Player.position + m_Offset;
        float distance = Mathf.Clamp(Vector2.Distance(target, transform.position), 0, 2);
        target = Vector3.Lerp(transform.position, target, m_Smoothing * Time.deltaTime * distance);
        target.y = Mathf.Max(2, target.y);

        transform.position = target;
    }
}
