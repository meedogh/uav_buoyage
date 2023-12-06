using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveButton : MonoBehaviour
{
    public GameObject menu;
    public void OnButtonClick()
    {
        // Get the parent of the button's parent (assuming the button is a direct child of the Canvas)
        Transform parentTransform = transform.parent;

        // Check if the parent exists
        if (parentTransform != null)
        {
            // Deactivate the parent GameObject
            parentTransform.gameObject.SetActive(false);
        }
        menu.SetActive(true);
    }
}
