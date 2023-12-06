using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovement : MonoBehaviour
{

    public Transform navMeshObject;
    public Rigidbody rb;             // The Rigidbody object to follow

    public float movementSpeed = 5f; // Adjust this speed as needed
    public float rotationSpeed = 5f; // Adjust this speed as needed
    public float forceAmount = 5f;

    void FixedUpdate()
    {
        if (navMeshObject != null && rb != null)
        {
            Vector3 direction = (navMeshObject.position - transform.position).normalized;

            // Add force in the calculated direction
            rb.AddForce(direction * forceAmount);
            // Rotate towards the NavMeshAgent's rotation around the Y-axis with interpolation
            Quaternion newRotation = Quaternion.Euler(0f, navMeshObject.eulerAngles.y, 0f);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, newRotation, Time.deltaTime * rotationSpeed));
        }
    }
}
