using UnityEngine;

public class FollowCursor : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Update()
    {
        Follow();
    }

    void Follow()
    {
        Vector3 temp = Input.mousePosition;
        // Set this to be the distance you want the object to be placed in front of the camera.
        temp.z = -Camera.main.gameObject.transform.position.z;
        Vector3 newPos = Camera.main.ScreenToWorldPoint(temp);
        transform.position = new Vector3(newPos.x, newPos.y, newPos.z);
    }
}
