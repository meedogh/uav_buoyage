using UnityEngine;

public class SelectableObject : MonoBehaviour
{
    [SerializeField] protected bool isSelected;
    [SerializeField] protected GameObject menuCanvas;
    protected Vector3 movePosition;
    protected static SelectableObject currentSelectedObject;

    protected virtual void Start()
    {
        SelectionManager.Instance.RegisterSelectable(this);
    }

    public virtual void Select()
    {
        isSelected = true;
        ShowMenu();
        SelectionManager.Instance.SetCurrentSelectedObject(this);
    }

    public virtual void Deselect()
    {
        isSelected = false;
        HideMenu();
        SelectionManager.Instance.SetCurrentSelectedObject(null);
    }

    public void SetMoveTarget(Vector3 target)
    {
        movePosition = target;
    }

    public Vector3 GetMoveTarget()
    {
        return movePosition;
    }

    protected void ShowMenu()
    {
        if (menuCanvas != null)
        {
            menuCanvas.SetActive(true);
        }
    }

    protected void HideMenu()
    {
        if (menuCanvas != null)
        {
            menuCanvas.SetActive(false);
        }
    }

    protected void ToggleSelection()
    {
        if (isSelected)
        {
            Deselect();
        }
        else if (currentSelectedObject != null)
        {
            Deselect();
            currentSelectedObject = this;
            isSelected = true;
        }
    }

    public bool IsDrone()
    {
        return transform.IsChildOf(GameObject.Find("Drones").transform);
    }
    protected bool IsCharger()
    {
        return transform.IsChildOf(GameObject.Find("ChargingPorts").transform);
    }
}
