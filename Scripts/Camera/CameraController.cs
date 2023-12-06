using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class CameraController : MonoBehaviour { 
    
    public float rotationSpeed = 2f; // Adjust the speed of camera rotation
    public float movementSpeed = 5f; // Adjust the speed of camera movement

    private bool isRotating = false;
    private GraphicRaycaster raycaster;
    private EventSystem eventSystem;
    private SelectableObject selectedTarget;

    void Start()
    {
        // Assuming your Canvas has a GraphicRaycaster component
        raycaster = FindObjectOfType<GraphicRaycaster>();
        eventSystem = FindObjectOfType<EventSystem>();
    }

    void Update()
    {
        HandleMouseInput();
        HandleKeyboardInput();
    }

    void HandleMouseInput()
    {
        // Rotate the camera based on right mouse button
        if (Input.GetMouseButtonDown(1))
        {
            isRotating = true;
            if (IsMouseOverUI()) return;

            // Deselect drone if right-clicked without UI interference
            if (selectedTarget != null)
            {
                selectedTarget.Deselect();
                selectedTarget = null;
            }
        }

        if (Input.GetMouseButtonUp(1))
        {
            isRotating = false;
        }

        if (isRotating)
        {
            if (IsMouseOverUI())
            {
                return; // Don't perform rotation if the mouse is over UI
            }
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            // Rotate the camera around the Y and X axes, ignoring Z-axis rotation
            transform.Rotate(Vector3.up * mouseX * rotationSpeed, Space.World);
            transform.Rotate(Vector3.left * mouseY * rotationSpeed);
        }

        // Handle left mouse button click
        if (Input.GetMouseButtonDown(0) && !IsMouseOverUI())
        {
            HandleLeftMouseButtonClick();
        }
    }

    void HandleKeyboardInput()
    {
        if (isRotating)
        {
            // Get input for movement
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            // Calculate movement direction relative to camera
            Vector3 moveDirection = (transform.right * horizontal + transform.forward * vertical).normalized;

            // Move the camera based on input
            transform.Translate(moveDirection * movementSpeed * Time.deltaTime, Space.World);
        }
    }

    private void HandleLeftMouseButtonClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            SelectableObject hitTarget = hit.collider.GetComponent<SelectableObject>();
            ChargingStation hitStation = hit.collider.GetComponent<ChargingStation>();
            if (hitTarget != null)
            {
                if (selectedTarget != null && selectedTarget != hitTarget)
                {
                    selectedTarget.Deselect();
                    selectedTarget = hitTarget;
                    selectedTarget.Select();
                }
                else if (selectedTarget == null)
                {
                    selectedTarget = hitTarget;
                    selectedTarget.Select();
                }
            }

            else if (hitTarget == null)
            {
                print(hit.point);
                if (hitStation != null && selectedTarget.IsDrone())
                {
                    DroneController selectedDrone = selectedTarget.GetComponent<DroneController>();

                    selectedDrone.StartMoveToStationCoroutine(hitStation.transform.position);
                }
                else
                {
                    selectedTarget.SetMoveTarget(hit.point);
                }
            }
        }
    }

    private bool IsMouseOverUI()
    {
        PointerEventData eventData = new PointerEventData(eventSystem);
        eventData.position = Input.mousePosition;

        // Create a list to store the results of the GraphicRaycast
        List<RaycastResult> results = new List<RaycastResult>();

        // Perform the raycast
        raycaster.Raycast(eventData, results);

        // Check if any UI element was hit
        return results.Count > 0;
    }
}
