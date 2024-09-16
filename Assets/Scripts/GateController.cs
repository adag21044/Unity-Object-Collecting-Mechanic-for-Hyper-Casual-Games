using UnityEngine;
using TMPro;

public class GateController : MonoBehaviour
{
    [SerializeField] private TMP_Text gateNumberText = null;
    [SerializeField] private enum GateType
    {
        PosiitiveGate,
        NegativeGate
    };

    [SerializeField] private GateType gateType;
    [SerializeField] private int gateNumber;

    public int GateNumber => gateNumber;

    // Start is called before the first frame update
    void Start()
    {
        RandomGateNumber();
    }

    private void RandomGateNumber()
    {
        switch(gateType)
        {
            case GateType.PosiitiveGate:
                gateNumber = Random.Range(1, 10);
                gateNumberText.text = gateNumber.ToString();
                break;

            case GateType.NegativeGate:
                gateNumber = Random.Range(-10, 0);
                gateNumberText.text = gateNumber.ToString();
                break;
        }
    }
}
