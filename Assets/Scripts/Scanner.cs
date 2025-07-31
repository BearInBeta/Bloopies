using UnityEngine;

public class Scanner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bloop"))
        {
            collision.gameObject.GetComponent<Bloopy>().PlayBloop();
        }else if (collision.gameObject.CompareTag("TopScan"))
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().reachedTop = true;
        }
    }
}
