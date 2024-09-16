using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BallController : MonoBehaviour
{
    public GameObject ballPrefab;
    [SerializeField] private TMP_Text _ballCountText = null;
    [SerializeField] private List<GameObject> balls = new List<GameObject>(); 
    [SerializeField] private float ZAxisSpeed;
    [SerializeField] private float horizontalSpeed = 10f;
    [SerializeField] private float horizontalLimit = 2.14f;
    private float horizontal;
    private bool isDragging = false;  // Fareye basılıp sürüklenip sürüklenmediğini takip eder
    private Vector3 lastMousePosition;
    public int gateNumber;
    private int targetCount;

    // Update is called once per frame
    void Update()
    {
        HorizontalMove();    
        ForwardMove();
        UpdateBallCountText();
    }

    private void HorizontalMove()
    {
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
            float newX = Mathf.Clamp(transform.position.x + deltaX, -horizontalLimit, horizontalLimit);  // X pozisyonunu sınırlandır

            transform.position = new Vector3(newX, transform.position.y, transform.position.z);  // Yeni pozisyona taşı
            lastMousePosition = currentMousePosition;  // Son fare pozisyonunu güncelle
        }
    }

    private void ForwardMove()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * ZAxisSpeed);
    }

    private void UpdateBallCountText()
    {
        _ballCountText.text = balls.Count.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BallStack"))
        {
            AddBallToStack(other.gameObject);
        }
        else if (other.CompareTag("Gate"))
        {
            HandleGateInteraction(other.GetComponent<GateController>().GateNumber);
        }
    }

    private void AddBallToStack(GameObject newBall)
    {
        newBall.transform.SetParent(transform);
        newBall.GetComponent<SphereCollider>().enabled = false;

        // Eğer ball listesinde hiç top yoksa ilk topu 0 konumuna yerleştir
        if (balls.Count == 0)
        {
            newBall.transform.localPosition = Vector3.zero;
        }
        else
        {
            // Eğer ball listesinde top varsa, önceki topun arkasına yerleştir
            Vector3 previousBallPosition = balls[balls.Count - 1].transform.localPosition;
            newBall.transform.localPosition = new Vector3(0f, 0f, previousBallPosition.z - 1f);
        }

        balls.Add(newBall);
    }

    private void HandleGateInteraction(int gateValue)
    {
        gateNumber = gateValue;
        targetCount = balls.Count + gateNumber;

        if (gateNumber > 0)
        {
            IncreaseBallCount(gateNumber);
        }
        else if (gateNumber < 0)
        {
            DecreaseBallCount(-gateNumber);  // Pozitif değerle işlem yap
        }
    }

    private void IncreaseBallCount(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject newBall = Instantiate(ballPrefab);
            newBall.transform.SetParent(transform);
            newBall.GetComponent<SphereCollider>().enabled = false;

            Vector3 lastBallPosition = balls[balls.Count - 1].transform.localPosition;
            newBall.transform.localPosition = new Vector3(0f, 0f, lastBallPosition.z - 1f);

            balls.Add(newBall);
        }
    }

    private void DecreaseBallCount(int count)
    {
        for (int i = 0; i < count && balls.Count > 0; i++)
        {
            GameObject lastBall = balls[balls.Count - 1];
            balls.RemoveAt(balls.Count - 1);
            Destroy(lastBall);  // Topu yok et
        }
    }
}

