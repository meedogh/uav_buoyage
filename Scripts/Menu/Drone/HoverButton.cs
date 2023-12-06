using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HoverButton : MonoBehaviour
{
    public TMP_InputField targetYField;
    public void OnHoverButtonClick()
    {
        DroneController selectedDrone = SelectionManager.Instance.CurrentSelectedObject as DroneController;
        // Parse input values from the text fields
        float targetY = (targetYField.text != "") ? float.Parse(targetYField.text) : selectedDrone.transform.position.y;

        print(targetY);
        print(selectedDrone.name);
        // Check if a drone is selected
        if (selectedDrone != null)
        {
            print(selectedDrone.name);
            // Create the target position vector
            Vector3 targetPosition = new Vector3(selectedDrone.transform.position.x, targetY, selectedDrone.transform.position.z);

            // Call the MoveToPosition function on the selected drone
            selectedDrone.Hover(targetPosition);
        }
    }
}
