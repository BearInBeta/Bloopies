using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Draggable : MonoBehaviour
{
    [Header("Drag Force Settings")]
    public float forceStrength = 1000f;
    public float damping = 20f;
    public float maxForce = 75f;
    public float gravityScale = 3f;

    [Header("Click Settings")]
    public LayerMask draggableLayers; // Assign only the draggable layer(s)

    private static Draggable activeDraggable; // The one currently being dragged

    private Rigidbody2D rb;
    private Camera cam;
    private bool isDragging = false;
    private Vector3 offset;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        rb.gravityScale = gravityScale;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mouseWorld = GetMouseWorldPos();

            RaycastHit2D hit = Physics2D.Raycast(mouseWorld, Vector2.zero, 0f, draggableLayers);
            if (hit.collider != null && hit.collider.attachedRigidbody != null)
            {
                Draggable found = hit.collider.GetComponent<Draggable>();
                if (found != null)
                {
                    found.TryStartDrag();
                }
            }
        }

        if (Input.GetMouseButtonUp(0) && activeDraggable != null)
        {
            activeDraggable.StopDrag();
            activeDraggable = null;
        }
    }

    public void TryStartDrag()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        if (activeDraggable != null) return;

        GetComponent<Bloopy>()?.PlayBloop();

        isDragging = true;
        activeDraggable = this;

        rb.gravityScale = 0f;
        rb.linearVelocity = Vector2.zero;
        Vector3 mouseWorld = GetMouseWorldPos();
        offset = transform.position - mouseWorld;
    }

    public void StopDrag()
    {
        isDragging = false;
        rb.gravityScale = gravityScale;
        rb.constraints = RigidbodyConstraints2D.None;

    }

    void FixedUpdate()
    {
        if (!isDragging) return;
        transform.rotation = Quaternion.identity;
        Vector2 targetPos = GetMouseWorldPos() + offset;
        Vector2 toTarget = targetPos - rb.position;

        Vector2 force = (toTarget * forceStrength) - (rb.linearVelocity * damping);
        force = Vector2.ClampMagnitude(force, maxForce);

        rb.AddForce(force, ForceMode2D.Force);
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mp = Input.mousePosition;
        mp.z = -cam.transform.position.z;
        return cam.ScreenToWorldPoint(mp);
    }
}
