using System.Collections;
using TMPro;
using UnityEngine;

public class Bloopy : MonoBehaviour
{
    [SerializeField] SpriteRenderer body, glasses1, glasses2;
    public BloopType bloopType;
    Bloop bloop;
    private AudioSource bloopSource;
    [SerializeField] TMP_Text nameText,symbolText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        bloopSource = GetComponent<AudioSource>();

    }

    public void SetUpBloop(string bloopName)
    {

        bloop = GameObject.FindGameObjectWithTag("GameController").GetComponent<Bloops>().GetBloop(bloopName);
        if (bloop.symbol != "")
        {
            symbolText.text = bloop.symbol;
        }
        else
        {
            nameText.text = bloopName;
        }
        bloopSource.clip = bloop.clip;
        body.color = bloop.color;
        glasses1.color = bloop.glasses;

        glasses2.color = bloop.glasses;
        bloopType = bloop.type;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayBloop()
    {
        bloopSource.Play();

    }

    public void ScanBloop()
    {
        PlayBloop();

        if (bloopType == BloopType.Ghost)
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().toBeDestroyed.Add(gameObject);
        }
        if (bloop.name == "RV")
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().scanSpeed *= -1;
        }



    }

    public void ApplyBuff(Bloopy toBeBuffed)
    {
        if(bloop.name == "GH" && toBeBuffed.bloopType == BloopType.Normal)
        {
            toBeBuffed.bloopType = BloopType.Ghost;
            toBeBuffed.gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(toBeBuffed.gameObject.GetComponent<SpriteRenderer>().color, bloop.color, 0.5f);
            Destroy(gameObject);
        }
    }



}


public enum BloopType { Normal, Buff, Ghost };
