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
                
        GetComponent<BoxCollider2D>().enabled = playerBoa.marcaFlag;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.name == "Ball" && Ball.current.freeFlag && Ball.current.speed < 5 && playerBoa.marcaFlag && !playerBoa.keepBoa && !Ball.current.isShooted && !Ball.current.respawn) 
        {
            
            playerBoa.SetKeepBoa();
            playerBoa.SetBallBoa();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
       
        if (collision.gameObject.name == "Ball" && Ball.current.freeFlag &&Ball.current.speed<5 && playerBoa.marcaFlag && !playerBoa.keepBoa && !Ball.current.isShooted && !Ball.current.respawn) 
        {
            
            playerBoa.SetKeepBoa();
            playerBoa.SetBallBoa();
        }
    }
}
