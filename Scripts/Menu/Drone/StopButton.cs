using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopButton : MonoBehaviour
{   
    public void OnStopButtonClick()
    {
        DroneController selectedDrone = SelectionManager.Instance.CurrentSelectedObject as DroneController;
        selectedDrone.activeSignal = false;
        selectedDrone.Stop();
    }
}
