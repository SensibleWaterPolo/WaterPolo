using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalShoot : MonoBehaviour
{
    public Player player;


    // Start is called before the first frame update

    private void Awake()
    {
        
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        { GetComponent<Animator>().SetBool("Shoot", player.signalOK); }    
    
    }

}
