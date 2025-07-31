using UnityEngine;

public class Hit : MonoBehaviour
{
    [SerializeField] AudioClip[] Sounds;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        //GetComponentInParent<Bloopy>().PlayBloop();
    }
}

