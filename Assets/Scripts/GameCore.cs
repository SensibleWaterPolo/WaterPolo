using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCore : MonoBehaviour
{
    public  static GameCore current;

    public  bool isPlay = false;
  
    public bool levelCPUHard; //true se livello CPU hard, false se normal

    private void Awake()
    {
        current = this;
        levelCPUHard = true;
    }
    void Start()
    {
        Invoke("Play", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play() {
        isPlay = true;
    }
    public void Pause() 
    {
        isPlay = false;
    }
}
