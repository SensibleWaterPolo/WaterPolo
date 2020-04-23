using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public static class Utility //M: Classe statica che contiene funzioni di utilità
{
         
    public static float GetAngleBetweenPosAB(Vector3 posA,Vector3 posB) //M: restituisce l'angolo di rotazione dalla posizione A verso B
    {
        return Mathf.Atan2(posB.y - posA.y, posB.x - posA.x) * Mathf.Rad2Deg;
    }

    public static void SetRotation(GameObject obj, float angle) //M: Setta la rotazione dell'oggetto obj
    { 
        obj.transform.rotation = Quaternion.Euler(0,0,angle);
              
    }

    public static float GetAngleBetweenObjAB(GameObject objA, GameObject objB) //M: restituisce l'angolo tra due oggetti 
    { 

        return GetAngleBetweenPosAB(objA.transform.position, objB.transform.position);
    }

    public static void RotateObjAtoB(GameObject objA, GameObject objB) //M: ruota l'oggetto A verso l'oggetto B          
    {

        SetRotation(objA, GetAngleBetweenPosAB(objA.transform.position, objB.transform.position));
                      
    }

    public static void RotateObjToPoint(GameObject obj, Vector3 point)  //M: ruota l'oggetto verso una posizione
    {
        SetRotation(obj,GetAngleBetweenPosAB(obj.transform.position,point)); 
     
    }

   
    }
    
    

