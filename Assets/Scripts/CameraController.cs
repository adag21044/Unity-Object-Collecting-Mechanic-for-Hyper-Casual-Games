using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform ballTransform;
    private Vector3 offset;
    [SerializeField] private float lerpRate;

    private void Start()
    {
        if (ballTransform != null)
        {
            offset = transform.position - ballTransform.position;
        }
    }

    private void LateUpdate()
    {
        if (ballTransform != null)
        {
            Vector3 newPos = Vector3.Lerp(transform.position, ballTransform.position + offset, lerpRate * Time.deltaTime);
            transform.position = newPos;
        }
        else
        {
            Debug.Log("Game Over! Ball has been destroyed.");
        }
    }
}
