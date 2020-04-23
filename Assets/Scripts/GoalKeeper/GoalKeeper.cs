using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoalKeeper : MonoBehaviour
{
    [Header("STAT Var")] //M: statistiche giocatore
    public float block; //M: probabilità di prendere parare centrale
    [Range(1f, 8f)]
    public float agility; //M: salto verso la palla
    [Range(0f, 3f)]
    public float vel; //M: velocità di spostamento nella porta


    [Header("FLAG")]
    public bool cpuFlag; //M: true se muove la cpu
    public bool flagJump;

    [Header("STATE")]

    public bool bicy; //M: bicicletta

    [Header("POSITION")] //M: variabili delle posizioni 
    private Vector3 posDefault;

    [Header("ANIMATION")]
    public Animator animator;
    protected RuntimeAnimatorController ctrlAnim;

    protected Vector3 posMid;
    protected Vector3 posLeft;
    protected Vector3 posRight;
    public Vector3 finalPos;
    public bool arrived;

    public float distanceBall;
    public float posXBall;


    public float gap; //M: Valore che determina il tipo di parata
    public float gapRadius; //M: Valore che determina il raggio di azione oltre il quale il portiere non interviene

    protected Coroutine updateGKPos;

    protected Rigidbody2D rb;

    protected float limitGKL; //M:limiti per i quali il portiere non interviene
    protected float limitGKR;

    protected int idTeam;


    public virtual void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        flagJump = false;
        limitGKL = GameObject.Find("LimitGkL").transform.position.x;
        limitGKR = GameObject.Find("LimitGkR").transform.position.x;
    }

    public void Start()
    {
        block = Random.Range(7,10);
        arrived = false;
    }

    // Update is called once per frame
    void Update()
    {

      
    }
    private void FixedUpdate()
    {

        if ( GameCore.current.isPlay)
        { UpdateFinalPos();
            if(!arrived && !flagJump)
            SwimGk();
          //  Utility.RotateObjAtoB(this.gameObject, Ball.current.gameObject);
        }
    }



    public void SwimGk()
    {
        float distanzaMancante = (this.transform.position - finalPos).sqrMagnitude;
        if (distanzaMancante > 0)

        {    
            arrived = false;
            Vector3 nuova_Pos = Vector3.MoveTowards(transform.position, finalPos, vel * Time.deltaTime);
            rb.MovePosition(nuova_Pos);
            
           }
        else
            arrived = true;


    }

    public virtual void UpdateFinalPos() 
    {

                
    }
    
    public void UpdateDist_PosBall()
    {
        if (Ball.current.inGameFlag)
        {
            distanceBall = Vector3.Distance(transform.position, Ball.current.transform.position);
            posXBall = Ball.current.transform.position.x;
        }
                
    }
    
    public bool CalcBlock() //M: Calcola se il portiere effettua o no la parata considenrando la velocità della palla, variabile block del portiere, casualità 1/4
    {
        bool save;

        float coeffVelBall = Ball.current.speed /3;

        float coeffGk = (block*10)-5; 

        float coeff =coeffGk-coeffVelBall;

        if (Random.Range(0,99) <=coeff)
            save = true;
        else save = false;
        Debug.Log(coeff+" Save: "+save);       
        return save;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {


        bool save;
        if (collision.gameObject.tag == "Ball" && !flagJump && Ball.current.transform.position.x > limitGKL && Ball.current.transform.position.x < limitGKR && Ball.current.isShooted)
        {
            flagJump = true;
            save = CalcBlock();
            
            float x = transform.position.x - Ball.current.transform.position.x;

            if (x <= -4.5) //M:se la palla si trova alla destra 
            {                
                if (idTeam == 1)
                    SaveLeft(save);
                else
                    SaveRight(save);
            }
            else if (x >= 4.5) //M:se la palla si trova alla sinistra
            {
                
                if (idTeam == 1)
                    SaveRight(save);
                else
                    SaveLeft(save);
            }
            
            else if (-1 <= x && x <= 1)
            {
                
                SaveUP(save);
            }
            else
            {
                SaveMid(save);
               
            }
        }

    }

    public virtual void SaveUP(bool save)
    {
        animator.SetTrigger("Up");
        if (save)
            transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("Side");
        Invoke("DisableSave", 1f);

    }

    public virtual void SaveLeft(bool save)
    {
        animator.SetTrigger("Left");
        if (save)
            transform.GetChild(1).gameObject.layer = LayerMask.NameToLayer("Side");
        
    }
    public virtual void SaveRight(bool save)
    {
        animator.SetTrigger("Right");
        if (save)
            transform.GetChild(2).gameObject.layer = LayerMask.NameToLayer("Side");
           }

    public virtual void SaveMid(bool save)
    {
        animator.SetTrigger("Front");
        if (save)
        {
            transform.GetChild(3).gameObject.layer = LayerMask.NameToLayer("Side");
            transform.GetChild(4).gameObject.layer = LayerMask.NameToLayer("Side");
        }
        Invoke("DisableSave", 1f);
    }
    public void DisableSave()
    {
        transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("Water");
        transform.GetChild(1).gameObject.layer = LayerMask.NameToLayer("Water");
        transform.GetChild(2).gameObject.layer = LayerMask.NameToLayer("Water");
        transform.GetChild(3).gameObject.layer = LayerMask.NameToLayer("Water");
        transform.GetChild(4).gameObject.layer = LayerMask.NameToLayer("Water");

        flagJump = false;
    }

}
