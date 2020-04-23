using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour

{
    [Header("SECTOR")]
    public int sectorAction; //M: settore di azione del giocatore 1: Sx, 2: Mid, 3: Dx

    [Header("STAT Var")] //M: statistiche giocatore
    
    [Range(0,50)]
    public float speed;
    public float pass;
    [Range(0f, 10f)]
    public float shoot;
    public int stamina;//M: forza del giocatore per vincere lo scontro, se mosso da cpu ha un minimo X e un massimo Y

    [Header("FLAG")]
    public bool cpuFlag; //M: true se muove la cpu
    public bool ballFlag; //M: true se è in possesso della palla
    public bool counterAttFlag; //M: true se è in controfuga
    public bool stunFlag; //M:il giocatore ha perso lo scontro
    public bool flagShoot; //M: true se si sta preparando ad un tiro, false se è un passaggio
    public bool arrivedFlagAtt;//M: true se è arrivato a destinazione
    public bool arrivedFlagBall;//M: true se è arrivato a destinazione
    public bool arrivedFlagDef;//M: true se è arrivato a destinazione
    [Header("STATE")]
    public bool bicy; //M: bicicletta
    public bool swim; //M: nuota
    public bool keep; //M: mantieni palla
    public bool def; //M: difende con il braccio alzato
    public bool backSwim;//M:nuota sul dorso
       
    [Header("SHOOT")]
    public bool loadShoot;
    public Vector3 destShoot;
    
    [Header("POSITION")] //M: variabili delle posizioni 
    protected Vector3 posAtt;
    protected Vector3 posDef;
    protected Vector3 posStart;
    protected Vector3 posMiddle;
    public Vector3 posFinal;
    public Vector3 nextPosFinal;
    public Vector3 posToWatch; //Posizione da guardare
    public Vector3 posGoal;

    [Header("OPPONENT")]//M:fa riferimento al diretto avversario
    public Player opponent;

    [Header("ANIMATION")]
    public Animator animator;
    protected RuntimeAnimatorController ctrlAnim;

    public float limitVelBall; //M: limite per il quale intercetto il passaggio
        
    protected int ballExit=0; //M: risolve un bug per il quale la palla esce due volte dalla collisione

    
    private bool pause;
       
    public ShootSignal shootSignalPrefab;
    public ShootSignal shootSignal;

    public int idTeam; //M: 0:YELLOW 1:RED
    public int idBall; //M: 0: libera non nel mio settore, 1: libera nel mio settore, 2: in possesso amico, 3: in possesso avversario, 4: sono io in possesso, 5: palla nel mio settore e sono il più vicino -1 indefinito  
    public int idAnim;

    public bool marcaFlag; //M:Variabile utilizzata per far ruotare le boe
   
    public bool boaFlag;
    
    public float distaceBall; //M:distanza dalla palla
    public float distanceAtt; //M:distanza dal punto di attacco
    public float distanceDef; //M:distanza dal punto di difesa
   

    public virtual void Awake()
    {
        animator = GetComponent<Animator>();
        swim = false;
        backSwim = false;
        bicy = true;
        keep = false;
        loadShoot = false;
        pause = true;
        arrivedFlagBall = false;
        arrivedFlagAtt = false;
        arrivedFlagDef = false;
        transform.GetChild(2).gameObject.layer = LayerMask.NameToLayer("Water");
        limitVelBall = 4;
        posFinal = Vector3.zero;
        idAnim = 0;
        ballFlag = false;
    }
    private void Start()
    {
        pass = Random.Range(7, 10);
        speed = Random.Range(7,10);
        stamina = Random.Range(6,10);
        
    }
        
    public virtual void FixedUpdate()
    {
       if (GameCore.current.isPlay)
        {
            UpdateIdBall();
            UpdateState();
            CheckAnim();
            UpdateFlagCounterAtt();
            
            if (swim || backSwim)
                Swim();
        }
    }

    public void UpdateState()

    {
        if (def)
            transform.GetChild(2).gameObject.layer = LayerMask.NameToLayer("Side");
        else
            transform.GetChild(2).gameObject.layer = LayerMask.NameToLayer("Water");

        if (idBall == 0 && Ball.current.inGameFlag && !keep && !loadShoot )
        {
           
            SetBicy();
        }

        if (idBall == 0 && Ball.current.inGameFlag && !keep && !loadShoot && PosPlayerMng.curret.GetPlayerForTeamNearBall(idTeam)==name && distaceBall<10)
        {

            SetSwim(Ball.current.transform.position,false);
        }

        if (idBall == 1 && Ball.current.motionlessFlag && Ball.current.inGameFlag && !ballFlag  && !loadShoot && !marcaFlag && (opponent.distaceBall> 1)) //M:palla nel mio settore e libera

        {
            SetSwim(Ball.current.transform.position,false);

        }

        if( idBall == 1 && Ball.current.motionlessFlag && Ball.current.inGameFlag && !ballFlag && !loadShoot && marcaFlag && PosPlayerMng.curret.GetPlayerForTeamNearBall(idTeam)==name)
        {
            SetSwim(Ball.current.transform.position,false);
        }

        
        if (idBall == 1 && Ball.current.isShooted && Ball.current.player.name == opponent.name && arrivedFlagDef)
        {
            SetDef();
        }

        if (idBall == 2 && !arrivedFlagAtt)
        {
            SetSwim(posAtt,false);

        }

        if (idBall == 3 && !arrivedFlagDef && !marcaFlag)
        {
            if (opponent.counterAttFlag)
                SetSwim(posDef, false);
            else
            {
                if (idTeam == 0)//M: Yellow
                {
                    if (transform.position.y > posDef.y)
                        SetSwim(posDef, true);
                    else SetSwim(posDef, false);
                }
                else
                {
                    if (transform.position.y < posDef.y)
                        SetSwim(posDef, true);
                    else SetSwim(posDef, false);
                }
            }
          
        }
        if (idBall == 3 && arrivedFlagDef && !marcaFlag)
        {
           SetBicy();
        
        }
        if (idBall == 2 && marcaFlag)
        { 
        SetDef();
        
        }
        
        if (idBall == 3 && arrivedFlagDef && (opponent.keep || opponent.loadShoot))
        {
            SetDef();
        }


        if (idBall == 4)
        {
        }


    }
    public void UpdateIdBall()
    {
        distaceBall = Vector2.Distance(transform.position,Ball.current.transform.position);  
        distanceAtt= Vector2.Distance(transform.position, posAtt);
        distanceDef= Vector2.Distance(transform.position, posDef);

        if (Ball.current.statePos == 0)//M:la palla è in possesso
        {
            if (Ball.current.idTeam == idTeam && ballFlag)
                idBall = 4; //M: sono io in possesso
            if (Ball.current.idTeam == idTeam && !ballFlag)
                idBall = 2;//M è in possesso un mio compagno di squadra
            if (Ball.current.idTeam != idTeam)
                idBall = 3; //M: la palla è in possesso dell'avversario
        }//
        else if (Ball.current.statePos == sectorAction) //M:la palla è nel mio settore
        {            
            idBall = 1;
        }
        else
        {
            idBall = 0;
        }
        
    }  //M: aggiorna lo stato della palla rispetto al giocatore;
  
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        
            if (obj.tag == "Ball" && !keep  && Ball.current.freeFlag && Ball.current.speed < 5f && !Ball.current.isShooted)
            {
               SetKeep();
               SetBall();
            }

        
    }

    
    private void OnTriggerExit2D(Collider2D collision)
    {
      
    }
    public virtual void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball") & Ball.current.freeFlag & Ball.current.motionlessFlag & !Ball.current.isShooted)
        {
            SetKeep();
            SetBall();
        }
    }




    public void SetBall() { //M: sposta la palla nella posizione corretta all'interno del giocatore
                  
            Ball.current.SetPlayer(this);
            Ball.current.DisableBall();
            Ball.current.transform.parent = transform;
            Ball.current.transform.position = transform.GetChild(0).position;
    }

          
    
    public void SetBicy()
    {
        if (swim || loadShoot || def || backSwim)
        {
           // UnityEngine.Debug.Log(name + " ->biciciletta" + Time.time);

            bicy = true;
            keep = false;
            swim = false;
            backSwim = false;
            def = false;
            loadShoot = false;
            ballFlag = false;
                       
            if (arrivedFlagAtt)
                Utility.RotateObjToPoint(this.gameObject, posGoal);
            else if (arrivedFlagDef)
                Utility.RotateObjToPoint(this.gameObject, opponent.transform.position);
        }
     
    } 
    public void SetSwim(Vector3 nextPos, bool _backSwim)
    {
      //  UnityEngine.Debug.Log(name + " -> provo a nuoto" + Time.time);

        if (!Ball.current.isShooted)
        {
            if (nextPos != posFinal)
                posFinal = nextPos;

            swim = !_backSwim;
            backSwim = _backSwim;
            keep = false;
            bicy = false;
            def = false;
            loadShoot = false;
                       
        }
    }
    public void Swim() //M: nuova funzione per il nunoto
    {

        float distanzaMancante = (this.transform.position - posFinal).sqrMagnitude;

        if (distanzaMancante > 0)
        {
            Vector3 nuova_Pos = Vector3.MoveTowards(transform.position, posFinal, speed * Time.deltaTime);
            GetComponent<Rigidbody2D>().MovePosition(nuova_Pos);
            Utility.RotateObjToPoint(this.gameObject, posFinal);
            distanzaMancante = (this.transform.position - posFinal).sqrMagnitude;
        }

        {
            CheckArrivedToPos();
            SetBicy();
        }
    }

    public void SetKeep() 
    {
      //  UnityEngine.Debug.Log(name + " ->possesso" + Time.time);
        keep = true;
        swim = false;
        backSwim = false;
        bicy = false;
        def = false;
        loadShoot = false;
        ballFlag = true;
        Utility.RotateObjToPoint(this.gameObject, posGoal);
        
    } 

    public void SetShoot() {
        
       //UnityEngine.Debug.Log(name + " ->tiro" + Time.time);
        keep = false;
        swim = false;
        backSwim = false;
        bicy = false;
        def = false;
        loadShoot = true;
             
    }
    public void LoadShoot(Vector3 directions, bool flag)
    {
        Ball.current.transform.parent = null; //Libera la palla 
      
        SetShoot();
        destShoot = directions;
        flagShoot = flag;
    
    }

    public void Shoot()  //M: chiamato dal TouchManager
    {
        Ball.current.shoot = shoot;
        Ball.current.pas = pass;
        Ball.current.ShootBall(destShoot, flagShoot);
        
        Invoke("UpdateLoadShoot", 3f);
    }
    public void UpdateLoadShoot() //M:fine tiro
    {
        SetBicy();
    }
    public void SetDef() //M: setta lo stato def
    {
        if (!def)
        {
        //    UnityEngine.Debug.Log(name + " ->Def"+Time.time);
            keep = false;
            swim = false;
            backSwim = false;
            bicy = false;
            def = true;
            ballFlag = false;
             Utility.RotateObjAtoB(this.gameObject, opponent.gameObject);
            
        }

    }
    
       
    public virtual void SetPosition() { } //M: imposta le posizioni all'interno del campo

    public void SetCpuFlag(bool flag)
    {
        this.cpuFlag = flag;

    }

  
    public void SetCounterAttFlag(bool flag)
    {
        this.counterAttFlag = flag;
    }

    public void SetStunFlag(bool flag)
    {
        this.stunFlag = flag;
    }

    public void StartGame() {
        pause = false;
    }

    
    
    public float DistanceFromBall() { //M calcola la distanza tra il giocatore e la palla
        return Vector2.Distance(transform.position,Ball.current.transform.position);
    }
      



    public void CheckArrivedToPos() 
    {
        arrivedFlagDef= (transform.position - posDef).sqrMagnitude <=0;
        arrivedFlagBall = keep;
        arrivedFlagAtt = (transform.position - posAtt).sqrMagnitude <=0;
    }


   

    public void UpdateIdAnim(int newIdAnim) 
    { if (newIdAnim != idAnim)
        {   
            idAnim = newIdAnim;
            animator.SetInteger("IdAnim",idAnim);
        }
    }

    public void CheckAnim() 
    {
        if (bicy)
            UpdateIdAnim(0);
        if (swim)
            UpdateIdAnim(1);
        if (backSwim)
            UpdateIdAnim(10);
        if (keep)
            UpdateIdAnim(2);
        if (loadShoot)
            UpdateIdAnim(3);
        if (def)
            UpdateIdAnim(4);
    //   UnityEngine.Debug.Log( name + " ->bicy"+bicy+" /swim "+swim+"  /keep "+keep+" /loadshoot "+loadShoot+ " /def" +def);
    
    }
    
    public bool CheckOpponent(string name) 
    {
        if (GameObject.Find(name))
            return true;
        else return false;
    
    }


    public void UpdateFlagCounterAtt() 
    {
        if (distanceAtt < opponent.distanceDef)
            counterAttFlag = true;
        else counterAttFlag = false;
    }
    

    
   
}

    


        

