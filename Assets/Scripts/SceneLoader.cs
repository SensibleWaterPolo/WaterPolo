using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    public void LoadSingleplayer()
    {
         SceneManager.LoadScene(1);
        
    }

    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }

 public void LoadOptionMenu()
    {
        SceneManager.LoadScene(2);
    }
}
