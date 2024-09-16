using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private List<GameObject> balls = new List<GameObject>(); 
    [SerializeField] private float ZAxisSpeed;
    [SerializeField] private float horizontalSpeed = 10f;
    [SerializeField] private float horizontalLimit = 2.14f;
    private float horizontal;
    private bool isDragging = false;  // Fareye basılıp sürüklenip sürüklenmediğini takip eder
    private Vector3 lastMousePosition;

    // Update is called once per frame
    void Update()
    {
        HorizontalMove();    
        ForwardMove();
    }

    private void HorizontalMove()
    {
        float newX;

        if (Input.GetMouseButtonDown(0)) // Fareye basıldığında
        {
            isDragging = true;  // Sürükleme işlemi başlıyor
            lastMousePosition = Input.mousePosition;  // İlk fare pozisyonunu kaydet
        }
        else if (Input.GetMouseButtonUp(0)) // Fare bırakıldığında
        {
            isDragging = false;  // Sürükleme işlemi bitiyor
        }

        if (isDragging)
        {
            Vector3 currentMousePosition = Input.mousePosition;  // Şu anki fare pozisyonunu al
            float deltaX = (currentMousePosition.x - lastMousePosition.x) * horizontalSpeed * Time.deltaTime;  // X eksenindeki fare hareketini al

            newX = transform.position.x + deltaX;
            newX = Mathf.Clamp(newX, -horizontalLimit, horizontalLimit);  // X pozisyonunu sınırlandır

            transform.position = new Vector3(newX, transform.position.y, transform.position.z);  // Yeni pozisyona taşı

            lastMousePosition = currentMousePosition;  // Son fare pozisyonunu güncelle
        }
    }

    private void ForwardMove()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * ZAxisSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
         if (other.gameObject.CompareTag("BallStack"))
        {
            other.gameObject.transform.SetParent(transform);
            other.gameObject.GetComponent<SphereCollider>().enabled = false;

            // Eğer ball listesinde hiç top yoksa ilk topu 0 konumuna yerleştir
            if (balls.Count == 0)
            {
                other.gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
            }
            else
            {
                // Eğer ball listesinde top varsa, önceki topun arkasına yerleştir
                other.gameObject.transform.localPosition = new Vector3(0f, 0f, balls[balls.Count - 1].transform.localPosition.z - 1f);
            }

            // Yeni topu ball listesine ekle
            balls.Add(other.gameObject);
        }
    }
}
