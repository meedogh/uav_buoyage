using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    private static SelectionManager instance;
    public static SelectionManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SelectionManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("SelectionManager");
                    instance = obj.AddComponent<SelectionManager>();
                }
            }
            return instance;
        }
    }

    private List<SelectableObject> selectedObjects = new List<SelectableObject>();
    private SelectableObject currentSelectedObject;

    public SelectableObject CurrentSelectedObject
    {
        get { return currentSelectedObject; }
    }

    public void RegisterSelectable(SelectableObject selectable)
    {
        if (!selectedObjects.Contains(selectable))
        {
            selectedObjects.Add(selectable);
        }
    }

    public void DeselectAll()
    {
        foreach (SelectableObject selectable in selectedObjects)
        {
            selectable.Deselect();
        }
        selectedObjects.Clear();
        currentSelectedObject = null;
    }

    public void SetCurrentSelectedObject(SelectableObject selectable)
    {
        currentSelectedObject = selectable;
    }

}
