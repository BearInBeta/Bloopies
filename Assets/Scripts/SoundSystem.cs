using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : MonoBehaviour
{
    List<Bloop[]> music;
    [SerializeField] AudioSource[] speakers;
    [SerializeField] float barLength;
    
    public void GenerateMusic(string[] bars)
    {
        music = new List<Bloop[]>();
        foreach (string bar in bars)
        {
            string[] notes = bar.Split('|');
            Bloop[] bloops = new Bloop[notes.Length];
            for(int i = 0; i < notes.Length; i++)
            {
                Bloop bloop = GetComponent<Bloops>().GetBloop(notes[i]);
                bloops[i] = bloop;
            }
            music.Add(bloops);
        }
    }

    public void PlayMusic()
    {
        StartCoroutine(PlayMusicCoroutine());
    }

    IEnumerator PlayMusicCoroutine()
    {
        foreach (Bloop[] bloops in music)
        {
            for (int i = 0; i < bloops.Length; i++)
            {
                speakers[i].PlayOneShot(bloops[i].clip);
            }
            yield return new WaitForSeconds(barLength);
        }
    }
}
