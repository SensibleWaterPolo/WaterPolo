using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorBoa : MonoBehaviour
{
    public Player playerBoa;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerBoa.keepBoa)
        {
            this.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        }
        else 
        {
            this.gameObject.layer = LayerMask.NameToLayer("Side");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Ball" && !Ball.current.isShooted && Ball.current.freeFlag && !playerBoa.stun && playerBoa.marcaFlag && !Ball.current.respawn) 
        {
            playerBoa.SetKeepBoa();
            playerBoa.SetBallBoa();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
       
        if (collision.gameObject.name == "Ball" && !Ball.current.isShooted && Ball.current.freeFlag && !playerBoa.stun && playerBoa.marcaFlag && !Ball.current.respawn)
        {
            
            playerBoa.SetKeepBoa();
            playerBoa.SetBallBoa();
        }
    }
}
