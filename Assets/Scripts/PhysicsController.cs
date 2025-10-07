using UnityEngine;

public class PhysicsController : MonoBehaviour
{
    private Rigidbody rb;
    public float springKracht = 500.0f;

    void Start()
    {
        // Krijg referentie naar Rigidbody component
        rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            Debug.Log("Geen Rigidbody gevonden! Voeg er een toe.");
        }
    }

    void Update()
    {
        // Spring omhoog als je Space indrukt
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Spring!");
            rb.AddForce(Vector3.up * springKracht);
        }
    }
}