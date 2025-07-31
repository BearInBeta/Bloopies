using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]
public class CircularEdgeCollider : MonoBehaviour
{
    [Range(0.1f, 100f)] public float radius = 5f;
    [Range(3, 256)] public int segments = 64;

    void Start()
    {
        GenerateCircle();
    }

    public void GenerateCircle()
    {
        EdgeCollider2D edge = GetComponent<EdgeCollider2D>();
        Vector2[] points = new Vector2[segments + 1]; // +1 to close the loop

        for (int i = 0; i <= segments; i++)
        {
            float angle = 2 * Mathf.PI * i / segments;
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;
            points[i] = new Vector2(x, y);
        }

        edge.points = points;
    }
}
