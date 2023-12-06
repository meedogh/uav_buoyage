using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoveAction : MonoBehaviour
{
    public TMP_InputField targetXField;
    public TMP_InputField targetYField;
    public TMP_InputField targetZField;

    // Attach this function to the button click event in the Unity Editor
    public void OnMoveButtonClick()
    {
        DroneController selectedDrone = SelectionManager.Instance.CurrentSelectedObject as DroneController;
        // Parse input values from the text fields
        float targetX = (targetXField.text != "") ? float.Parse(targetXField.text) : selectedDrone.transform.position.x;
        float targetY = (targetYField.text != "") ? float.Parse(targetYField.text) : selectedDrone.transform.position.y;
        float targetZ = (targetZField.text != "") ? float.Parse(targetZField.text) : selectedDrone.transform.position.z;


        print(selectedDrone.name);
        // Check if a drone is selected
        if (selectedDrone != null)
        {
            print(selectedDrone.name);
            // Create the target position vector
            Vector3 targetPosition = new Vector3(targetX, targetY, targetZ);

            // Call the MoveToPosition function on the selected drone
            selectedDrone.MoveToPosition(targetPosition);
        }
    }
}
