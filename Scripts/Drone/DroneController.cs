using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DroneController : SelectableObject
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private Vector3 hoverForce;
    private float signalSpeed = 5f;

    public bool activeSignal = false;

    private Rigidbody rb;
    private float prevZ;
    private Quaternion rot;

    private Coroutine moveCoroutine;
    private Coroutine hoverCoroutine;
    private Coroutine signalCoroutine;

    private void Start()
    {
        hoverForce = -Physics.gravity;
        rb = GetComponent<Rigidbody>();
        prevZ = transform.position.z;
        movePosition = transform.position;
        rot = rb.transform.rotation;
    }

    private void FixedUpdate()
    {
        GetComponent<Rigidbody>().AddForce(hoverForce, ForceMode.Acceleration);
    }
    private void Update()
    {
        transform.rotation = rot;
        if (isSelected)
        {
            MoveToPosition(movePosition);
        }
    }

    public void Stop()
    {
        StopAllCoroutines();
        rb.velocity = new Vector3(0,0,0);
        hoverForce = -Physics.gravity;
    }

    public void MoveToPosition(Vector3 targetPosition)
    {
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }
        moveCoroutine = StartCoroutine(MoveToPositionCoroutine(targetPosition));
    }

    private IEnumerator MoveToPositionCoroutine(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            Vector3 moveAmount = moveDirection;
            moveAmount.y = 0f;

            transform.Translate(moveAmount * moveSpeed * Time.deltaTime, Space.World);
            yield return null;
        }
    }

    public void Hover(Vector3 targetPosition)
    {
        if (hoverCoroutine != null)
        {
            StopCoroutine(hoverCoroutine);
        }
        hoverCoroutine = StartCoroutine(HoverCoroutine(targetPosition.y));
    }

    private IEnumerator HoverCoroutine(float targetY)
    {
        float direction = transform.position.y - targetY;
        while (Mathf.Abs(transform.position.y - targetY) > 0.5f)
        {
            hoverForce = (-Physics.gravity) + Mathf.Sign(direction) * (Physics.gravity / 8);
            yield return null;
        }
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        hoverForce = -Physics.gravity;
    }

    public void SendSignal(float width, float height, char dirSymbol)
    {
        if (signalCoroutine != null)
        {
            StopCoroutine(signalCoroutine);
        }
        signalCoroutine = StartCoroutine(SendSignalCoroutine(width, height, dirSymbol));
    }

    private IEnumerator SendSignalCoroutine(float width, float height, char dirSymbol)
    {
        float startX = transform.position.x;
        float startY = transform.position.y;
        float newX, newY;
        float timeElapsed = 0f;

        while (activeSignal)
        {
            if (dirSymbol == 'E' || dirSymbol == 'W')
            {
                newX = startX + Mathf.Cos((timeElapsed / 2) * signalSpeed) * width;
                newY = startY + Mathf.Sin((timeElapsed) * signalSpeed) * height;
            }
            else
            {
                newX = startX + Mathf.Cos((timeElapsed) * signalSpeed) * width;
                newY = startY + Mathf.Sin((timeElapsed/2) * signalSpeed) * height;
            }
           
            Vector3 newPosition = new Vector3(newX, transform.position.y, transform.position.z);
            MoveToPosition(newPosition);

            Vector3 newHoverPosition = new Vector3(transform.position.x, newY, transform.position.z);
            Hover(newHoverPosition);

            timeElapsed += Time.deltaTime;

            yield return null;
        }
    }
    public void StartMoveToStationCoroutine(Vector3 stationPosition)
    {
        StartCoroutine(MoveToStationCoroutine(stationPosition));
    }

    private IEnumerator MoveToStationCoroutine(Vector3 stationPosition)
    {
        Vector2 stationPos = new Vector2(stationPosition.x, stationPosition.z);
        Vector2 dronePos = new Vector2(transform.position.x, transform.position.z);

        SetMoveTarget(stationPosition);

        while (Vector2.Distance(dronePos, stationPos) >= 5f)
        {
            yield return null;
        }

        Hover(stationPosition);
    }
}

