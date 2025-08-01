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
            collision.gameObject.GetComponent<Bloopy>().ScanBloop();
        }else if (collision.gameObject.CompareTag("TopScan"))
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().reachedTop = true;
        }
        else if (collision.gameObject.CompareTag("DestroyScan"))
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().reachedDestroy = true;
        }
        else if (collision.gameObject.CompareTag("BottomScan") && GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().scanSpeed < 0)
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().reachedTop = true;
        }
    }
}
