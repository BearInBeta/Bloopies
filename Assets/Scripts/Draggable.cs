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
    public Bloopy toBeBuffed; 

    private Rigidbody2D rb;
    private Camera cam;
    private bool isDragging = false;
    private Vector3 offset;

    [SerializeField] BoxCollider2D m_collider2D;
    [SerializeField] GameObject rightEye, leftEye, rightLid, leftLid;

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

        if(isDragging && GetComponent<Bloopy>()?.bloopType == BloopType.Buff)
        {
            Vector2 mouseWorld = GetMouseWorldPos();

            RaycastHit2D hit = Physics2D.Raycast(mouseWorld, Vector2.zero, 0f, draggableLayers);
            if (hit.collider != null && hit.collider.attachedRigidbody != null)
            {
                Bloopy found = hit.collider.GetComponent<Bloopy>();
                
                toBeBuffed = found;

            }
            else
            {
                toBeBuffed = null;
            }
        }else
        {
            toBeBuffed = null;
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
        if(GetComponent<Bloopy>()?.bloopType == BloopType.Buff)
        {
            rightEye.GetComponent<CircleCollider2D>().enabled = false;
            leftEye.GetComponent<CircleCollider2D>().enabled = false;
            leftLid.GetComponent<EdgeCollider2D>().enabled = false;
            rightLid.GetComponent<EdgeCollider2D>().enabled = false;

            rightEye.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            leftEye.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            GetComponent<BoxCollider2D>().enabled = false;
            m_collider2D.enabled = false;
        }
        Vector3 mouseWorld = GetMouseWorldPos();
        offset = transform.position - mouseWorld;
    }

    public void StopDrag()
    {
        if(GetComponent<Bloopy>()?.bloopType == BloopType.Buff && toBeBuffed != null)
        {
            GetComponent<Bloopy>()?.ApplyBuff(toBeBuffed);
            toBeBuffed = null;
        }
        isDragging = false;
        rb.gravityScale = gravityScale;
        rb.constraints = RigidbodyConstraints2D.None;
        if (GetComponent<Bloopy>()?.bloopType == BloopType.Buff)
        {
            rightEye.GetComponent<CircleCollider2D>().enabled = true;
            leftEye.GetComponent<CircleCollider2D>().enabled = true;
            leftLid.GetComponent<EdgeCollider2D>().enabled = true;
            rightLid.GetComponent<EdgeCollider2D>().enabled = true;

            rightEye.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            leftEye.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            GetComponent<BoxCollider2D>().enabled = true;

            m_collider2D.enabled = true;
        }

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
