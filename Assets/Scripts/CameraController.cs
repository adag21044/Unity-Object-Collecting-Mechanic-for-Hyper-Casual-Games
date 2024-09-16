using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform ballTransform;
    private Vector3 offset;
    [SerializeField] private float lerpRate;

    private void Start()
    {
        offset = transform.position - ballTransform.position;
    }

    private void LateUpdate()
    {
        Vector3 newPos = Vector3.Lerp(transform.position, ballTransform.position + offset, lerpRate * Time.deltaTime);
        transform.position = newPos;
    }
}
