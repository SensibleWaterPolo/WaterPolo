using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class GameCore : MonoBehaviour
{
    public static GameCore current;


    public bool isPlay = false;
    public float timeMatch = 180;
    public float time = 0;

    public bool levelCPUHard; //true se livello CPU hard, false se normal

    private void Awake()
    {
        current = this;
        levelCPUHard = true;
    }
    void Start()
    {
        Application.targetFrameRate = 60;
        Invoke("Play", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlay) 
        {
            time = Time.time;
              
        
        }

    }

    public void Play()
    {
        isPlay = true;
        
    }
    public void Pause()
    {
        isPlay = false;
    }

    public int GetMin(float time)
    {
        return Mathf.FloorToInt (time / 60);
        
    }
    public int GetSec(float time) 
    {
        
    return Mathf.FloorToInt(time % 60);
    
    }


}
