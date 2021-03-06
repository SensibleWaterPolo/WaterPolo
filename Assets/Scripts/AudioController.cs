﻿using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController current;
    public AudioClip fischio;
    public AudioClip esultanza;
    public AudioClip piscina;

    private void Awake()
    {
        current = this;
    }

    public void DoFischio()
    {
        AudioSource.PlayClipAtPoint(fischio, Vector3.zero, 1f);
        //Ciao
        //ALE
    }

    public void DoEsultanza()
    {
        AudioSource.PlayClipAtPoint(esultanza, Vector3.zero, 1f);
    }
}