using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float horizontalLimit;
    private float horizontal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HorizantalMove();    
    }

    private void HorizantalMove()
    {
        float newX;

        if(Input.GetMouseButton(0))
        {
            horizontal = Input.GetAxis("Mouse X");
        }

        newX = transform.position.x + horizontal * horizontalSpeed * Time.deltaTime;
        newX = Mathf.Clamp(newX, -horizontalLimit, horizontalLimit);

        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

    
}
