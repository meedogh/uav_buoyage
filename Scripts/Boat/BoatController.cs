using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : SelectableObject
{
    public float speed = 10f;
    public float rotationSpeed = 100f;

    float horizontalInput;
    float verticalInput;
    private void Start()
    {
        movePosition = transform.position;
    }
    void Update()
    {
        MoveShip(verticalInput);

        RotateShip(horizontalInput);
    }

    void MoveShip(float input)
    {
        Vector3 movement = transform.forward * input * speed * Time.deltaTime;
        transform.position += movement;
    }

    void RotateShip(float input)
    {
        float rotation = input * rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up, rotation);
    }

    public void SetInput(float forward, float turn)
    {
        verticalInput = forward;
        horizontalInput = turn;
    }
}