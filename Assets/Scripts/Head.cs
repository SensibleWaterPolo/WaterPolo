using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    public bool inCollision;
    
    // Start is called before the first frame update
    void Start()
    {
        inCollision = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Head"))
        {
            Player player = collision.transform.parent.GetComponent<Player>();
            if(player.name!=name && !player.swim && !player.swimKeep && !player.backSwim)
            inCollision = true;
        }
        else 
        {
            inCollision = false;
        
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Head"))
        {
            inCollision = false;
        }
    }

}
