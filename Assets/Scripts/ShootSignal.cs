using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootSignal : MonoBehaviour
{

    public Animator anim;
    
    
    // Start is called before the first frame update
    
    
    
    private void Awake()
    {
        GetComponent<MeshRenderer>().sortingLayerName = "Shoot";
        GetComponent<MeshRenderer>().sortingOrder = 0;
        anim = GetComponent < Animator>();
        anim.SetBool("Shoot", true);
    }

    public void ShootOk() 
    {
       anim.SetBool("Shoot",true);
    }

    public void ShootAbort() 
    {
        anim.SetBool("Shoot",false);
    }

}
