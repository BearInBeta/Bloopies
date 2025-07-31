using System.Collections.Generic;
using UnityEngine;

public class Bloops : MonoBehaviour
{
    [SerializeField] List<Bloop> bloops;
    private Dictionary<string, Bloop> cache;

    private void Awake()
    {
        CacheBloops();
    }

    public void CacheBloops()
    {
        cache = new Dictionary<string, Bloop>(bloops.Count);
        foreach (var bloop in bloops)
        {
            if (!string.IsNullOrEmpty(bloop.name))
                cache[bloop.name] = bloop;
        }
    }

    public Bloop GetBloop(int index)
    {
        return bloops[index];
    }

    public Bloop GetBloop(string name)
    {
        if(cache == null)
        {
            CacheBloops();
        }

        return cache[name];
    }
}
