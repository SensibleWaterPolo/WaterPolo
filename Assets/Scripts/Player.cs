using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Principal;
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
    public Vector2 clickCPU; //range di click della cpu, da un minimo X ad un massimo Y, utilizzati per decidere chi vince uno scontro 
    protected bool armDx; //M: true se è destro, false se è un mancino

    [Header("FLAG")]
    public bool cpuFlag; //M: true se muove la cpu
    public bool ballFlag; //M: true se è in possesso della palla
    public bool counterAttFlag; //M: true se è in controfuga
    public bool fightFlag; //M:il giocatore ha perso lo scontro
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
    public bool stun; //M:affogato
       
    [Header("SHOOT")]
    public bool loadShoot;
    public Vector3 destShoot;
    
    [Header("POSITION")] //M: variabili delle posizioni 
    public Vector3 posAtt;
    public Vector3 posDef;
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

    
    private bool pause; //M: per la pausa del gioco
       
    //PREFAB SHOOT
    public SignalShoot shootSignalPrefab;
    public SignalShoot shootSignal;
    public bool signalOK;

    //PREFAB FIGHT

    public int idTeam; //M: 0:YELLOW 1:RED
    public int idBall; //M: 0: libera non nel mio settore, 1: libera nel mio settore, 2: in possesso amico, 3: in possesso avversario, 4: sono io in possesso, 5: palla nel mio settore e sono il più vicino -1 indefinito  
    public int idAnim; //m: ID ANIMAZIONE

    public bool marcaFlag; //M:Variabile utilizzata per far ruotare le boe
     public bool boaFlag;  //M: per le animazione delle boe
    
    public float distaceBall; //M:distanza dalla palla
    public float distanceAtt; //M:distanza dal punto di attacco
    public float distanceDef; //M:distanza dal punto di difesa

   
    public bool pushAtt; //se in fase di difesa devo avanzare verso il difensore
    public bool beginPush;//comincia ad avanzare verso l'attaccante
    public float speedToPush;//velocità di spostamento verso l'attaccante


    
    //VARIABILI PER IL CONTROLLO DELLE COLLISIONI E CAMBIO DI DIREZIONE
  /*  protected Transform sensorRight;
    protected Transform sensorLeft;
    protected bool obstacleFront, obstacleLeft, obstacleRight;
    protected bool dodgeObstacle;
    protected LayerMask layer = 1 << 9;
    protected float rotSensor=40; //gradi dei sensori*/

   

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
        fightFlag = false;
        transform.GetChild(2).gameObject.layer = LayerMask.NameToLayer("Water");
        limitVelBall = 4;
        posFinal = Vector3.zero;
        idAnim = 0;
        ballFlag = false;
        pushAtt = false;
        speedToPush = 3;
        beginPush = false;
        
    }
    public virtual void Start()
    {
        pass = Random.Range(7, 10);
        speed = Random.Range(7,10);
        stamina = Random.Range(6,10);
        clickCPU = new Vector2(2, 10);

        /*  sensorRight = transform.GetChild(4).transform;
          sensorRight.transform.localRotation = Quaternion.Euler(0,0,-rotSensor);
          sensorLeft = transform.GetChild(5).transform;
          sensorLeft.transform.localRotation = Quaternion.Euler(0, 0, rotSensor);*/



    }
        
    public virtual void FixedUpdate()
    {
       if (GameCore.current.isPlay)
       {
            UpdateIdBall();
            UpdateState();
          //  CheckAnim();
            UpdateFlagCounterAtt();
            CheckArrivedToPos();
            if (swim || backSwim )
            {
                //  CheckObstacle();
                // SwimV2();
                Swim();
            }
        MoveToAttPlayer();
       }
    }


    public void UpdateState()

    {  // Attiva o disattiva il braccio in caso di posizione di difesa
        if (def)
            transform.GetChild(2).gameObject.layer = LayerMask.NameToLayer("Side");
        else
            transform.GetChild(2).gameObject.layer = LayerMask.NameToLayer("Water");

        // BICY :Bicicletta se la palla è libera nel mio settore e attendo l'evoluzione del gioco
        if (idBall == 0 && Ball.current.inGameFlag && !keep && !loadShoot )
        {
           
            SetBicy();
        }

        // SWIM : La palla è libera NON nel mio settore ma sono quello più vicino della mia squadra

        if (idBall == 0 && Ball.current.inGameFlag && !keep && !loadShoot && PosPlayerMng.curret.GetPlayerForTeamNearBall(idTeam)==name && distaceBall<10 &&!stun &&!fightFlag && !Ball.current.isShooted)
        {

            SetSwim(Ball.current.transform.position,false);
        }

        // SWIM: La palla è libera nel mio settore
        if (idBall == 1 && Ball.current.motionlessFlag && Ball.current.inGameFlag && !ballFlag  && !loadShoot && !marcaFlag && (opponent.distaceBall> 1) &&!stun && !fightFlag && !Ball.current.isShooted) //M:palla nel mio settore e libera

        {
            SetSwim(Ball.current.transform.position,false);

        }

        //SWIM: Palla libera nel mio settore e sono quello più vicino della mia squadra

        if( idBall == 1 && Ball.current.motionlessFlag && Ball.current.inGameFlag && !ballFlag && !loadShoot && !marcaFlag && PosPlayerMng.curret.GetPlayerForTeamNearBall(idTeam)==name && !stun && !fightFlag && !Ball.current.isShooted)
        {
            SetSwim(Ball.current.transform.position,false);
        }
        // DEF: Palla è stata tirata e viaggia lungo il mio settore
        
        if (idBall == 1 && Ball.current.isShooted && Ball.current.player.name == opponent.name && arrivedFlagDef && !stun && !fightFlag)
        {
            SetDef();
        }
        //SWIM: palla in possesso di un mio compagno
        if (idBall == 2 && !arrivedFlagAtt && !stun && !fightFlag)
        {
            if (!counterAttFlag)
            {
                SetSwim(posAtt, false);
            }
            else 
            {
                SetSwim(posAtt, true);
            }
            //SetSwim(posAtt, true) se sono in controfuga o il mio portiere è in possesso

        }
        //SWIM: palla in possesso avversario
        if (idBall == 3 && !arrivedFlagDef && !marcaFlag && !stun && !fightFlag && !pushAtt && !beginPush)
        {
            if (opponent.counterAttFlag)
            {
                SetSwim(posDef, false);

            }
            else
            {
                if (idTeam == 0)//M: Yellow
                {
                    if (transform.position.y > posDef.y)
                    {
                        SetSwim(posDef, true);
                    }
                    else
                    {
                        SetSwim(posDef, false);
                    }
                }
                else
                {
                    if (transform.position.y < posDef.y)
                    {
                        SetSwim(posDef, true);
                    }
                    else
                    {
                        SetSwim(posDef, false); 
                    }
                }
            }
          
        }
        //BICY: palla in possesso avversario, sono in posizione di difesa ma la palla non ce l'ha il mio diretto opponent
        if (idBall == 3 && arrivedFlagDef && !marcaFlag && !stun && !fightFlag)
        {
           SetBicy();
        
        }
        //DEF: se sono il marcaboa e la palla è in possesso
        if (idBall == 3 && marcaFlag && !boaFlag)
        { 
        SetDef();
        
        }
        //DEF: palla in possesso del mio diretto avversario e sono arrivato nella posizione di difesa 
        if (idBall == 3 && arrivedFlagDef && (opponent.keep || opponent.loadShoot) && !stun && !fightFlag && Vector3.Distance(transform.position, opponent.transform.position) < 20) 
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
        if (swim || loadShoot || def || backSwim || stun )
        {
           // UnityEngine.Debug.Log(name + " ->biciciletta" + Time.time);

            bicy = true;
            keep = false;
            swim = false;
            backSwim = false;
            def = false;
            loadShoot = false;
            ballFlag = false;
            stun = false;
            animator.SetInteger("IdAnim", 0);

            if (arrivedFlagAtt)
            {
                if (!boaFlag)
                {
                    Utility.RotateObjToPoint(this.gameObject, posGoal);
                }
                else 
                {
                    Utility.RotateObjToPoint(this.gameObject, Ball.current.transform.position);
                } 
            
            }
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
            stun = false;
            animator.SetInteger("IdAnim", 1);

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
           
        }
        else
        {       
          
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
        stun = false;
        animator.SetInteger("IdAnim", 2);
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
        stun = false;
        animator.SetInteger("IdAnim", 3);
        

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
            stun = false;
           animator.SetInteger("IdAnim", 4);
            Utility.RotateObjAtoB(this.gameObject, opponent.gameObject);
            
        }

    }

    public void SetStun() 
    {
        if (!stun)
        {
            
            stun = true;
            bicy = false;
            keep = false;
            backSwim = false;
            bicy = false;
            def = false;
            ballFlag = false;
            animator.SetInteger("IdAnim", 5);
            Utility.RotateObjToPoint(this.gameObject, posFinal);
            
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

    public void StartGame() {
        pause = false;
    }

    
    
    public float DistanceFromBall() { //M calcola la distanza tra il giocatore e la palla
        return Vector2.Distance(transform.position,Ball.current.transform.position);
    }
      



    public void CheckArrivedToPos() 
    {
        arrivedFlagDef= transform.position==posDef;
        arrivedFlagBall = keep;
        arrivedFlagAtt = transform.position == posAtt;
    }

    
    public bool CheckOpponent(string name) 
    {
        if (GameObject.Find(name))
            return true;
        else return false;
    
    }
    public void SetFightFlag(bool flag) 
    {
        fightFlag = flag;
    
    }


    public void UpdateFlagCounterAtt() 
    {
        if (Vector3.Distance(posGoal,transform.position)<Vector3.Distance(posGoal,opponent.transform.position))
            counterAttFlag = true;
        else counterAttFlag = false;
    }

    public void MoveToAttPlayer()

   {
        if (def)
        {
            if (arrivedFlagDef && def && CheckOpponentShoot())
            {
                beginPush = true;
            }
            else if (!def || !CheckOpponentShoot())
            {
                beginPush = false;
                pushAtt = false;
            }
            if (beginPush && def && !marcaFlag)
            {
                LayerMask mask = 1 << 9; //strato player
                Vector3 posSensor = transform.GetChild(6).transform.position;
                Vector3 dir = (opponent.transform.position - posSensor).normalized;
                float dist = 2;
                RaycastHit2D hitAttPlayer = Physics2D.Raycast(posSensor, dir, dist, mask);
                if (hitAttPlayer.collider != null)
                {
                    if (hitAttPlayer.collider.name == opponent.name)
                    {

                        pushAtt = false;
                    }
                }
                else
                {
                    pushAtt = true;

                }
                Debug.Log(name + " mi sposto?" + pushAtt);
                if (pushAtt)
                {
                    Vector3 newPos = Vector3.MoveTowards(transform.position, opponent.transform.position, speedToPush * Time.deltaTime);
                    GetComponent<Rigidbody2D>().MovePosition(newPos);

                }
                Utility.RotateObjAtoB(this.gameObject, opponent.gameObject);
            }
        }
        else
        {
            pushAtt = false;
            beginPush = false;
        }
        
    }

    public bool CheckOpponentShoot() //Ritorna true, se l'avversario è in possesso o sta caricando il tiro o ha tirato e la palla sta viaggiando
    
    {
        if (opponent.keep || opponent.loadShoot)
        {
            return true;
        }
        else
            return false;
    }

    public void CreateSignalShoot() 
    {
       shootSignalPrefab = Instantiate(shootSignal,transform.position,Quaternion.identity);
        shootSignalPrefab.player = this;
    
    }

    public void DestroySignalShoot() 
    {
        Destroy(shootSignalPrefab.gameObject);
    }
 

}

    


        

