using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Draggable : MonoBehaviour
{
    public float maxSpeed = 10f;
    private Rigidbody2D rb;
    private bool isDragging = false;
    private Vector3 offset;
    private Camera mainCamera;
    private Bloopy bloopy;

    void Awake()
    {
        bloopy = GetComponent<Bloopy>();
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
    }

    void OnMouseDown()
    {
        bloopy.PlayBloop();
        isDragging = true;
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;
        offset = transform.position - mouseWorldPos;

        rb.gravityScale = 0;
        rb.linearVelocity = Vector2.zero;
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0;
            Vector2 targetPos = mouseWorldPos + offset;

            Vector2 currentPos = rb.position;
            Vector2 direction = (targetPos - currentPos);
            float distance = direction.magnitude;

            float moveStep = Mathf.Min(maxSpeed * Time.fixedDeltaTime, distance);
            Vector2 move = currentPos + direction.normalized * moveStep;

            rb.MovePosition(move);
        }
    }

    void OnMouseUp()
    {
        bloopy.StopBloop();
        isDragging = false;
        rb.gravityScale = 1;
        rb.linearVelocity = Vector2.zero;
    }
}
