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
    public int throwin; //Rimessa in gioco

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
    protected Vector3 posThrowIn;
    public Vector3 finalPos;
    public bool arrived;
    public bool readyToBlock;
    public Vector3 posBallEndAction;

    public float distanceBall;
    public float posXBall;


    public float gap; //M: Valore che determina il tipo di parata
    public float gapRadius; //M: Valore che determina il raggio di azione oltre il quale il portiere non interviene

    protected Coroutine updateGKPos;

    protected Rigidbody2D rb;

    protected float limitGKL; //M:limiti per i quali il portiere non interviene
    protected float limitGKR;

    public int idTeam;
   
    protected Coroutine brainCpuCoroutine;


    [Header("SHOOT")]
    public bool loadShoot;
    public Vector3 direction;
    public bool keep;
    
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
        block = Random.Range(6,10);
        arrived = false;
        throwin = Random.Range(7, 10);
        keep = false;
        bicy = true;
       
    }

    // Update is called once per frame
    void Update()
    {

      
    }
    private void FixedUpdate()
    {

        if ( GameCore.current.isPlay)
        {
            readyToBlock = GetComponent<BoxCollider2D>().enabled;
            UpdateFinalPos();
          
            if(!arrived && !flagJump && !keep)

            SwimGk();
         
        }

        if (!keep) 
        {
            GetComponent<CircleCollider2D>().enabled = false;

        }
        else { GetComponent<CircleCollider2D>().enabled = true; }

        if (transform.position == posMid) 
        {
            Utility.RotateObjToPoint(this.gameObject, Vector3.zero);
        
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
        { arrived = true;
            if (finalPos == posMid) 
            {
                Utility.RotateObjToPoint(this.gameObject, Vector3.zero);
            }
        
        }



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
        
        float coeffVelBall = Ball.current.speed /5;
        
        float coeffGk = (block*10); 

        float coeff =coeffGk-coeffVelBall;
        float prob = Random.Range(0, 99);
        
        if (prob<=coeff)
            save = true;
        else save = false;
      // Debug.Log("coeffBall " + coeffVelBall + " probabilità parare ->" + coeff + " valore estratto->" + prob + " save->"+save);
        return save;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {


        bool save;

        if (collision.gameObject.tag == "Ball" && !flagJump && Ball.current.transform.position.x > limitGKL && Ball.current.transform.position.x < limitGKR && readyToBlock && Ball.current.freeFlag)
        {
          
            flagJump = true;
            save = CalcBlock();
            agility = Random.Range(4,7);
            float x = transform.position.x - Ball.current.transform.position.x;

            if (x <= -2.5) //M:se la palla si trova alla destra 
            {                
                if (idTeam == 1)
                    SaveLeft(save);
                else
                    SaveRight(save);
            }
            else if (x >= 2.5) //M:se la palla si trova alla sinistra
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
        transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("DisableCollision");
        transform.GetChild(1).gameObject.layer = LayerMask.NameToLayer("DisableCollision");
        transform.GetChild(2).gameObject.layer = LayerMask.NameToLayer("DisableCollision");
        transform.GetChild(3).gameObject.layer = LayerMask.NameToLayer("DisableCollision");
        transform.GetChild(4).gameObject.layer = LayerMask.NameToLayer("DisableCollision");

        flagJump = false;
    }

    public void SetKeep() 
    {
        keep = true;
        animator.SetInteger("IdAnim", 1);
        SetBallGK();
        Ball.current.SetGK(this);
        if (cpuFlag)
        {
            Invoke("BrainCpu", 2);
        }
       GameCore.current.RestartTimeAction();
       
    }

    public void SetShoot()
    {
        
        }

    public void SetBallGK()
    { //M: sposta la palla nella posizione corretta all'interno del giocatore
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = true;
        transform.position = posThrowIn;
        Utility.RotateObjToPoint(this.gameObject, Vector3.zero);
        Ball.current.transform.parent = transform;
        Ball.current.transform.position = transform.GetChild(6).position;
        Ball.current.freeFlag = false;
        
           
    }
    public void Shoot()  //M: chiamata dall'animazione
    {
        Ball.current.throwIn = throwin;
        Ball.current.ShootBall(direction, false,1); //IL GK effettua sempre un passaggio mai un tiro
        Invoke("ResetGk",2f);        
    }
    

    public void LoadShoot(Vector3 dir) 
    {
        direction = dir;
        animator.SetInteger("IdAnim", 2);
    }

    public void ResetGk()
    {
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<CircleCollider2D>().enabled = false;
        keep = false;
        bicy = true;
        Utility.RotateObjToPoint(this.gameObject,Vector3.zero);
    }

    public IEnumerator StartDecisioMaking()
    {
        while (true)
        {
            bool stop = this.GkCpu();
            if (stop)
            {
                yield break;
            }

            yield return new WaitForSeconds(1);
        }
    }

    public void BrainCpu()
    {
      //  Debug.Log(name + "inizio a ragionare");
        if (brainCpuCoroutine != null)
        {
            StopCoroutine(StartDecisioMaking());
        }
        brainCpuCoroutine = StartCoroutine(StartDecisioMaking());

    }

    public virtual bool GkCpu()
    {
        return false;
    }
}
