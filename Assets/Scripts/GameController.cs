using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] GameObject scanner, bloopGO;
    public float scanSpeed;
    public bool reachedTop, reachedDestroy;
    public List<GameObject> toBeDestroyed;
    Vector3 scannerOriginalPosition;
    [SerializeField] string[] levelBloops, winningBloops;
    IEnumerator routine = null;
    
    void Start()
    {
        toBeDestroyed = new List<GameObject>();
        scannerOriginalPosition = scanner.transform.position;
        GetComponent<SoundSystem>().GenerateMusic(winningBloops);

        StartCoroutine(StartLevel());
    }

    IEnumerator StartLevel()
    {
        foreach (string levelBloop in levelBloops)
        {
            SpawnBloop(levelBloop);
            yield return new WaitForFixedUpdate();
        }
    }

    void SpawnBloop(string name)
    {

        float x = Random.Range(0f, 0.25f);
        float x2 = Random.Range(0.75f, 1f);
        if(Random.Range(1,3) == 1)
        {
            x = x2;
        }
        float y = Random.Range(0.5f, 1f);
        Vector2 viewportPos = new Vector2(x, y);

        // Convert to world position (z = 10 assumes orthographic camera distance)
        Vector3 worldPos = Camera.main.ViewportToWorldPoint(new Vector3(viewportPos.x, viewportPos.y, 10f));

        // Instantiate the bloop
        GameObject newBloop = Instantiate(bloopGO, worldPos, Quaternion.identity);
        Bloopy newBloopy = newBloop.GetComponent<Bloopy>();
        newBloopy.SetUpBloop(name);
        newBloopy.PlayBloop();
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
        scanSpeed = Mathf.Abs(scanSpeed);
        scanner.transform.position = scannerOriginalPosition;
        reachedTop = false;
        GameObject[] bloops = GameObject.FindGameObjectsWithTag("Bloop");

        //TODO: Add code to make sure everything is stopped before playing

        while (true)
        {
            scanner.transform.position += new Vector3(0, scanSpeed,0);
            yield return new WaitForFixedUpdate();
            if (reachedDestroy)
            {
                reachedDestroy = false;
                foreach (GameObject toBeDestroyedGO in toBeDestroyed)
                {
                    Destroy(toBeDestroyedGO);
                }
            }
            if (reachedTop)
            {
                
                reachedTop = false;
                scanSpeed = Mathf.Abs(scanSpeed);
                scanner.transform.position = scannerOriginalPosition;
            }
        }

    }
}
