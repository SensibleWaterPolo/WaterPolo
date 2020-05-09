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
        if (playerBoa.keepBoa || !playerBoa.marcaFlag)
        {
            playerBoa.GetComponent<CircleCollider2D>().enabled = true;
        }
        else if (playerBoa.marcaFlag && !playerBoa.keepBoa) 
        {
            playerBoa.GetComponent<CircleCollider2D>().enabled = false;
        } 
    
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


      
            if (collision.CompareTag("Ball") && !playerBoa.keep && !playerBoa.keepBoa && !playerBoa.swimKeep && !playerBoa.loadShoot && Ball.current.CheckBallIsPlayable(4) && playerBoa. marcaFlag)
            {
                Debug.Log(name + "prendo possesso" + Time.time);
                playerBoa.SetKeepBoa();
            }
       
    }
    private void OnTriggerStay2D(Collider2D collision)
    {

       
            if (collision.CompareTag("Ball") && !playerBoa.keep && !playerBoa.keepBoa && !playerBoa.swimKeep && !playerBoa.loadShoot && Ball.current.CheckBallIsPlayable(4) && playerBoa.marcaFlag)
            {
                Debug.Log(name + "prendo possesso" + Time.time);
                playerBoa.SetKeepBoa();
            }

         
    }}
