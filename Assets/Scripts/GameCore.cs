using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCore : MonoBehaviour
{
    public  static GameCore current;

    public  bool isPlay = false;
    // Start is called before the first frame update

    private void Awake()
    {
        current = this;
    }
    void Start()
    {
        Invoke("Play", 2);
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
