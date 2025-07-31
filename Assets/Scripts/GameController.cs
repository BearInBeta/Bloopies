using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] GameObject scanner, bloopGO;
    [SerializeField] float scanSpeed;
    public bool reachedTop;
    Vector3 scannerOriginalPosition;
    [SerializeField] string[] levelBloops;
    IEnumerator routine = null;
    void Start()
    {
        scannerOriginalPosition = scanner.transform.position;
        StartLevel();
    }

    void StartLevel()
    {
        foreach (string levelBloop in levelBloops)
        {
            SpawnBloop(levelBloop);
        }
    }

    void SpawnBloop(string name)
    {
        // Limit X to [0.0, 0.75] (left 3/4 of screen)
        // Limit Y to [0.75, 1.0] (top 1/4 of screen)
        float x = Random.Range(0f, 0.25f);
        float y = Random.Range(0.5f, 1f);
        Vector2 viewportPos = new Vector2(x, y);

        // Convert to world position (z = 10 assumes orthographic camera distance)
        Vector3 worldPos = Camera.main.ViewportToWorldPoint(new Vector3(viewportPos.x, viewportPos.y, 10f));

        // Instantiate the bloop
        GameObject newBloop = Instantiate(bloopGO, worldPos, Quaternion.identity);
        newBloop.GetComponent<Bloopy>().SetUpBloop(name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SubmitAnswer()
    {
        if(routine == null)
        {
            routine = SubmitAnswerCoroutine();
            StartCoroutine(routine);

        }
        else
        {
            scanner.transform.position = scannerOriginalPosition;
            StopCoroutine(routine);
            routine = null;
        }
    }

    IEnumerator SubmitAnswerCoroutine()
    {
        scanner.transform.position = scannerOriginalPosition;
        reachedTop = false;
        GameObject[] bloops = GameObject.FindGameObjectsWithTag("Bloop");
        /*foreach (GameObject bloop in bloops)
       {
           bloop.GetComponent<Draggable>().enabled = false;
       }

      bool stillMoving;
       do
       {
           stillMoving = false;
           foreach (GameObject bloop in bloops)
           {
               if(bloop.GetComponent<Rigidbody2D>().linearVelocityY > 0)
               {
                   stillMoving = true;
                   break;
               }
           }
       }while (stillMoving);*/

        while (true)
        {
            scanner.transform.position += new Vector3(0, scanSpeed,0);
            yield return new WaitForFixedUpdate();
            if (reachedTop)
            {
                reachedTop = false;
                scanner.transform.position = scannerOriginalPosition;
            }
        }

    }
}
