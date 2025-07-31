using UnityEngine;

public class Bloopy : MonoBehaviour
{
    [SerializeField] string bloopName;
    Bloop bloop;
    private AudioSource bloopSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        bloopSource = GetComponent<AudioSource>();
        bloop = GameObject.FindGameObjectWithTag("GameController").GetComponent<Bloops>().GetBloop(name);
        bloopSource.clip = bloop.clip;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayBloop()
    {
        bloopSource.Play();
    }
}
