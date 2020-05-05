using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public static AudioController current;
    public AudioClip fischio;
    private void Awake()
    {
        current = this;
    }
    public void DoFischio()
    {
        AudioSource.PlayClipAtPoint(fischio, Vector3.zero, 10f);
    }






}
