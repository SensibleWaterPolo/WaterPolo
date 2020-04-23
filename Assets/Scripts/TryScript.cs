using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Permissions;
using System.Text;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.Rendering;

public class TryScript : MonoBehaviour
{
    public Transform target;
    public float moveSpeed;
    public float rotationSpeed;
    public int maxDistance = 2;
    public static TryScript current;
    RaycastHit hit;
    Transform left;
    Transform right;
    public bool f, r, l = false;
    public float speedRot;
    public bool dodge = false;

    public float speed;
    //private Transform myTransform;
    // Use this for initialization
    void Awake()
    {
        current = this;
        //myTransform = transform;    
    }
    void Start()
    {
        GameObject goTo = GameObject.Find("Ball");
        target= goTo.transform;
        left = transform.GetChild(1).transform;
        right = transform.GetChild(0).transform;
       

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 dir = (target.position - transform.position).normalized;



        /* Debug.DrawLine(sensorL.position, nuova_Pos + new Vector3(0, 6, 0), Color.black);
        Debug.DrawLine(sensorR.position, nuova_Pos+ new Vector3(0,6,0), Color.red);*/


        
         RaycastHit2D hitfront = Physics2D.Raycast(transform.position, transform.right,5);
        Debug.DrawLine(transform.position, transform.position + transform.right * 5, Color.blue);
        RaycastHit2D hitRight = Physics2D.Raycast(right.position, right.right, 7);
        Debug.DrawLine(right.position, right.position + right.right * 7, Color.black);
        Debug.DrawLine(left.position, left.position + left.right * 7, Color.red);
        RaycastHit2D hitLeft = Physics2D.Raycast(left.position, left.right, 7);


       
        if (hitfront.collider)
        {
            Debug.Log("front: " + hitfront.collider.gameObject.name);
            f = true;
            dodge = true;
        }
        else
        {
            f = false;
        }

        if (hitLeft.collider && !hitfront.collider)
        {
            Debug.Log("left: " + hitLeft.collider.gameObject.name);
           
            l = true;
            dodge = true;
        }
        else
            l = false;

        if (hitRight.collider)
        {
            Debug.Log("right: " + hitRight.collider.gameObject.name);
            r = true;
            dodge = true;
        }
        else r = false;

        if (!hitfront.collider && !hitLeft.collider && !hitRight.collider)
            dodge = false;

        //look at our target

        Debug.Log("dodge : "+dodge+ " /f: "+f+  " /R: "+r+ " /L:" +l + " hit value: "+hitfront.collider+" "+hitLeft+ " "+hitRight );

         
        if(l && !f)
        transform.Rotate(Vector3.forward * Time.deltaTime * -100);
       
       if(l && f && !r)
        transform.Rotate(Vector3.forward * Time.deltaTime * -200);

        if (r && !f)
            transform.Rotate(Vector3.forward * Time.deltaTime * 100);
        if(r && f && !l)
            transform.Rotate(Vector3.forward * Time.deltaTime *200);

        if (r && f && l)
            Debug.Log("devo stare fermo");



        if (!f)
            transform.transform.Translate(Vector3.right * Time.deltaTime * 2);

        if (!f && !l && !r)
            dodge = false;

        if (!dodge)
        {
            Debug.Log("Nuoto dritto");
            Vector3 nuova_Pos = Vector3.MoveTowards(transform.position, target.position, 7 * Time.deltaTime);
            Vector3 relativePos = (target.transform.position - transform.position).normalized;
             transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0,0,Utility.GetAngleBetweenPosAB(transform.position,target.position)), Time.deltaTime);

            // Utility.RotateObjToPoint(this.gameObject, nuova_Pos);
            // GetComponent<Rigidbody2D>().MovePosition(nuova_Pos);
        }
        else {

           
            //  transform.Translate(Vector3.right * Time.deltaTime * 2); 
        }
        transform.Translate(Vector3.right * Time.deltaTime * 2);

    }

}
