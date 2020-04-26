using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEditor;
using UnityEngine;

public class Battle : MonoBehaviour
{
    private int forceAtt;
    private int forceDef;
    private int idAtt;
    private int idDef;
    private Player att;
    private Player def;
    private bool start;
    private bool whoWin;
    private Quaternion currentRot;
    private float degree;
    public int numclick;
    public int speed; //M: velocità rotazione
    public int idWin;
      

    private void Awake()
    {
        start = false;
        whoWin = false;
        numclick = 0;
        degree = 0;
        idWin = -1;
        speed = 50;
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (start)
        {
            CheckId();
           UpdateAngle();
        }
        
    }

    public void CreateBattle(Player att, Player def) 
    {
        this.att = att;
        this.def = def;

        Utility.RotateObjAtoB(att.gameObject, Ball.current.gameObject);
        Utility.RotateObjAtoB(def.gameObject, Ball.current.gameObject);

        att.transform.parent = transform;
        def.transform.parent = transform;

        start = true;
        GetComponent<CircleCollider2D>().enabled = true;

    }

    public void UpdateAngle()
    {
        if (!Ball.current.isShooted && !Ball.current.freeFlag)
        {
            Vector3 direction = Ball.current.transform.position - transform.position;
            float speedRot = speed * Time.deltaTime;
            float angle = Utility.GetAngleBetweenObjAB(this.gameObject,Ball.current.gameObject);
            Quaternion wantedRotation = Quaternion.Euler(0, 0, angle+degree);
            transform.rotation= Quaternion.RotateTowards(transform.rotation, wantedRotation,speedRot );
                                 
        }
    }

        public void CheckId()

    {
        if (!whoWin) 
        {
            whoWin = true;
            Invoke("WhoWin",3f);
        }
        if (att.swim || att.keep || def.swim || def.keep || att.idBall==3 || def.idBall==2)
            StopBattle();
    }

    public void StopBattle() 
    {
        start = false;
        GetComponent<CircleCollider2D>().enabled = start;
        att.transform.parent = null;
        def.transform.parent = null;
        att.marcaFlag = false;
        def.marcaFlag = false;
         Destroy(this.gameObject);

    }

    public void WhoWin() 
    {
        
        int newId;
        if (Random.value > 0.5)
            newId = 0;
        else newId = 1;

        if (newId != idWin)
        {
            idWin = newId;
            if (idWin == 0)
            {
               
                degree = 180;
            }
            else 
            {
            
                degree = 0;
            }
            if (Random.Range(0,100) > 80)
                speed *= -1;
        }
       

       

        whoWin = false;
    }

    public void AddClick() 
    {
        numclick++;
        
    }

    

}
