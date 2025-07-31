using TMPro;
using UnityEngine;

public class Bloopy : MonoBehaviour
{
    [SerializeField] SpriteRenderer body, glasses1, glasses2;
    Bloop bloop;
    private AudioSource bloopSource;
    [SerializeField] TMP_Text nameText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        bloopSource = GetComponent<AudioSource>();

    }

    public void SetUpBloop(string bloopName)
    {
        nameText.text = bloopName;
        bloop = GameObject.FindGameObjectWithTag("GameController").GetComponent<Bloops>().GetBloop(bloopName);
        bloopSource.clip = bloop.clip;
        body.color = bloop.color;
        glasses1.color = bloop.glasses;

        glasses2.color = bloop.glasses;
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
