using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalButton : MonoBehaviour
{
    [SerializeField] float height, width;
    [SerializeField] char dirSymbol;
    public void OnSignalButtonClick()
    {
        DroneController selectedDrone = SelectionManager.Instance.CurrentSelectedObject as DroneController;
        selectedDrone.activeSignal = true;
        selectedDrone.SendSignal(width, height, dirSymbol);
    }
}
