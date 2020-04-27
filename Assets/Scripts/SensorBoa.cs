using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorBoa : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Ball") 
        {
        // transform.parent.GetComponent<Player>(). boa acquisisce palla
        }
    }

}
