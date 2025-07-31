using UnityEngine;

public class ClampPupil : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Mathf.Abs(transform.localPosition.x) > 0.25f || Mathf.Abs(transform.localPosition.y) > 0.25f)
        {
            transform.localPosition = new Vector3(0,0,transform.localPosition.z);
        }
    }
}
