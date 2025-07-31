using UnityEngine;

public class Hit : MonoBehaviour
{
    [SerializeField] AudioClip hitClip;
    private AudioSource hitSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hitSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        hitSource.PlayOneShot(hitClip);
    }
}
