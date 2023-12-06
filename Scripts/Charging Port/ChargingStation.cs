using UnityEngine;

public class ChargingStation : MonoBehaviour
{
    [SerializeField] private float maxCharge = 100f;
    [SerializeField] private float currentCharge;
    public float chargingRate;
    public float chargingEfficiency;
    private bool active = false;

    private void Start()
    {
        currentCharge = maxCharge;
    }
    private void Update()
    {
        if(!HasCharge()) active = false;
        while (active)
        {
            currentCharge -= chargingRate * Time.deltaTime;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Drone"))
        {
            active = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        active = false;
    }

    public bool HasCharge()
    {
        return currentCharge > 0f;
    }

    public void UseCharge(float amount)
    {
        currentCharge = Mathf.Max(0f, currentCharge - amount);
    }
}
