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

    [Range(0, 50)]
    public float speed;
    public float pass;
    [Range(0f, 10f)]
    public float shoot;
    public int stamina;//M: forza del giocatore per vincere lo scontro, se mosso da cpu ha un minimo X e un massimo Y
    public int brain; //velocità della cpu nell'eseguire un'azione di passaggio o di tiro
    public Vector2 clickCPU; //range di click della cpu, da un minimo X ad un massimo Y, utilizzati per decidere chi vince uno scontro 
    protected bool armDx; //M: true se è destro, false se è un mancino
    public int idDecisionMaking; //Indentifica la scelta presa dal giocatore per passare da uno stato all'altro
    public int idDecisionCPU;

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
    public bool keepBoa; //M: quando la boa è in possesso di palla ed è spalle alla porta

    [Header("SHOOT")]
    public bool loadShoot;
    public Vector3 destShoot;

    [Header("POSITION")] //M: variabili delle posizioni 
    public Vector3 posAtt;
    public Vector3 posDef;
    public Vector3 posStart;
    public Vector3 posMiddle;
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

    protected int ballExit = 0; //M: risolve un bug per il quale la palla esce due volte dalla collisione

    private bool pause; //M: per la pausa del gioco


    //PREFAB FIGHT

    public int idTeam; //M: 0:YELLOW 1:RED
    public int idBall; //M: 0: libera non nel mio settore, 1: libera nel mio settore, 2: in possesso amico, 3: in possesso avversario, 4: sono io in possesso, 5: palla nel mio settore e sono il più vicino -1 indefinito  
    public int idAnim; //m: ID ANIMAZIONE

    //VARIABILI PER LE BOE
    public bool marcaFlag; //M:Variabile utilizzata per far ruotare le boe
    public bool boaFlag;  //M: per le animazione delle boe
    public float angleBoaZ; //M: angolodi rotazione della boa
    public float angleBoaW;

    public float distaceBall; //M:distanza dalla palla
    public float distanceAtt; //M:distanza dal punto di attacco
    public float distanceDef; //M:distanza dal punto di difesa


    public bool pushAtt; //se in fase di difesa devo avanzare verso il difensore
    public bool beginPush;//comincia ad avanzare verso l'attaccante
    public float speedToPush;//velocità di spostamento verso l'attaccante
    public bool coverOpponent; //true se sto coprendo quasi tutto lo specchio della porta

    public float waitAfterShoot; //Tempo di attesa dopo aver tirato

    protected Coroutine brainCpuCoroutine;

   

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
        marcaFlag = false;
        coverOpponent = false;
       
        

    }
    public virtual void Start()
    {
        pass = Random.Range(7, 10);
        speed = Random.Range(7, 10);
        stamina = Random.Range(6, 10);
        clickCPU = new Vector2(2, 10);
        brain = Random.Range(1, 2);
        waitAfterShoot = 1.5f;
     }

    public virtual void FixedUpdate()
    {
        if (GameCore.current.isPlay)
        {
            UpdateIdBall();
            UpdateState();
            UpdateFlagCounterAtt();
            CheckArrivedToPos();
            if (swim || backSwim)
            {
                Swim();
            }
            MoveToAttPlayer();
            RestorePlayer();
            if (boaFlag)
            {
                if (marcaFlag && !keepBoa && !keep)
                {
                    GetComponent<CircleCollider2D>().enabled = false;
                }
                else
                { GetComponent<CircleCollider2D>().enabled = true; 
                }
            }
        }
    }


    public virtual void UpdateState()

    {
        if (Ball.current.inGameFlag && !Ball.current.isShooted)
        {
            // Attiva o disattiva il braccio in caso di posizione di difesa
            if (def)
                transform.GetChild(2).gameObject.layer = LayerMask.NameToLayer("Arm");
            else
                transform.GetChild(2).gameObject.layer = LayerMask.NameToLayer("Pool");

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// STATO BICY

            // BICY :Bicicletta se la palla è libera non nel mio settore e attendo l'evoluzione del gioco
            if (idBall == 0 && Ball.current.inGameFlag && !keep && !loadShoot)
            {
                idDecisionMaking = 1;
                SetBicy();
            }
            //BICY: palla in possesso avversario, sono in posizione di difesa ma la palla non ce l'ha il mio diretto opponent
            if (idBall == 3 && arrivedFlagDef && !marcaFlag && !stun && !fightFlag && !opponent.ballFlag)
            {
                idDecisionMaking = 2;
                SetBicy();

            }


            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// STATO SWIM
            // SWIM : La palla è libera NON nel mio settore ma sono quello più vicino della mia squadra


            //SWIM: Palla vicina al boa o al marca boa
            if (idBall == 1 && marcaFlag && Ball.current.CheckBallIsPlayable(0) && distaceBall < opponent.distaceBall)
            {
                idDecisionMaking = 3;
                SetSwim(Ball.current.transform.position, false);

            }

            //SWIM: la palla è libera NON nel mio settore ma io sono il player più vicino 
            if (idBall == 0 && Ball.current.CheckBallIsPlayable(0) && PosPlayerMng.curret.GetPlayerForTeamNearBall(idTeam, boaFlag) == name && distaceBall < 10 && !stun && !fightFlag)
            {
                idDecisionMaking = 4;
                SetSwim(Ball.current.transform.position, false);
            }

            // SWIM: La palla è libera nel mio settore, sono in marcatura ma la palla è talmente vicina che provo a prenderla
            if (idBall == 1 && Ball.current.CheckBallIsPlayable(0) && marcaFlag && PosPlayerMng.curret.GetPlayerForTeamNearBall(idTeam, boaFlag) == name && distaceBall < 6)

            {
                idDecisionMaking = 5;
                SetSwim(Ball.current.transform.position, false);

            }
            //La palla è nel mio settore e sono un laterale
            if (idBall == 1 && Ball.current.CheckBallIsPlayable(0) && !loadShoot && !marcaFlag && sectorAction != 2 && !stun && !fightFlag)
            {
                idDecisionMaking = 55;
                SetSwim(Ball.current.transform.position, false);
            }

            //SWIM: Palla libera nel mio settore e sono quello più vicino della mia squadra

            if (idBall == 1 && Ball.current.CheckBallIsPlayable(0) && !loadShoot && !marcaFlag && PosPlayerMng.curret.GetPlayerForTeamNearBall(idTeam, boaFlag) == name && !stun && !fightFlag)
            {
                idDecisionMaking = 6;
                SetSwim(Ball.current.transform.position, false);
            }

            //SWIM: palla in possesso di un mio compagno
            if (idBall == 2 && !arrivedFlagAtt && !stun && !fightFlag)
            {
                if (!counterAttFlag)
                {
                    idDecisionMaking = 7;
                    SetSwim(posAtt, false);
                }
                else
                {
                    idDecisionMaking = 8;
                    SetSwim(posAtt, true);
                }
                if (Ball.current.gk != null)
                {
                    SetSwim(posAtt, true);
                }

            }

            //SWIM: palla in possesso avversario
            if (idBall == 3 && !arrivedFlagDef && !marcaFlag && !stun && !fightFlag && !pushAtt && !beginPush)
            {
                if (opponent.counterAttFlag)
                {
                    idDecisionMaking = 9;
                    SetSwim(posDef, false);

                }
                else
                {
                    if (idTeam == 0)//M: Yellow
                    {
                        if (transform.position.y > posDef.y)
                        {
                            idDecisionMaking = 10;
                            SetSwim(posDef, true);
                        }
                        else
                        {
                            idDecisionMaking = 11;
                            SetSwim(posDef, false);
                        }
                    }
                    else
                    {
                        if (transform.position.y < posDef.y)
                        {
                            idDecisionMaking = 12;
                            SetSwim(posDef, true);
                        }
                        else
                        {
                            idDecisionMaking = 13;
                            SetSwim(posDef, false);
                        }
                    }
                }

            }

            //Se la palla è in possesso avversario ma non del mio diretto mi riposiziono in difesa

            if (idBall == 3 && !arrivedFlagDef && !opponent.ballFlag)
            {
                if (opponent.counterAttFlag)

                {
                    idDecisionMaking = 131;
                    SetSwim(posDef, false);
                }
                else
                {
                    idDecisionMaking = 132;
                    if (idTeam == 0 && transform.position.y > posDef.y || idTeam == 1 && transform.position.y < posDef.y)
                    { SetSwim(posDef, true); }
                    else { SetSwim(posDef, false); }
                }

            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// STATO DEF

            // DEF: Palla è stata tirata e viaggia lungo il mio settore

            if (idBall == 1 && Ball.current.isShooted && speed > 5 && def && !stun && !fightFlag)
            {
                idDecisionMaking = 14;
                if (Ball.current.player != null)
                {
                    if (Ball.current.player.name == opponent.name)
                    {
                        SetDef();
                    }
                }
            }

            //DEF: se sono il marcaboa e la palla è in possesso
            if (idBall == 3 && marcaFlag)
            {
                idDecisionMaking = 15;
                SetDef();
            }
            //DEF: palla in possesso del mio diretto avversario e sono arrivato nella posizione di difesa 
            if (idBall == 3 && arrivedFlagDef && opponent.ballFlag && !stun && !fightFlag && Vector3.Distance(transform.position, opponent.transform.position) < 25 && !opponent.keepBoa)
            {
                idDecisionMaking = 16;
                SetDef();
            }

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if (idBall == 4)
            {
            }


        }
    }
    public void UpdateIdBall()
    {
        distaceBall = Vector2.Distance(transform.position, Ball.current.transform.position);
        distanceAtt = Vector2.Distance(transform.position, posAtt);
        distanceDef = Vector2.Distance(transform.position, posDef);

        if (!Ball.current.freeFlag)//M:la palla è in possesso
        {
            if (Ball.current.idTeam == idTeam && ballFlag)
                idBall = 4; //M: sono io in possesso
            if (Ball.current.idTeam == idTeam && !ballFlag)
                idBall = 2;//M è in possesso un mio compagno di squadra
            if (Ball.current.idTeam != idTeam)
                idBall = 3; //M: la palla è in possesso dell'avversario
        }//
        else if (Ball.current.statePos == sectorAction && Ball.current.CheckBallIsPlayable(0)) //M:la palla è nel mio settore
        {
            idBall = 1;
        }
        else if(Ball.current.statePos != sectorAction && Ball.current.CheckBallIsPlayable(0))
        {
            idBall = 0;
        }

    }  //M: aggiorna lo stato della palla rispetto al giocatore;

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;

        if (obj.tag == "Ball" &&  Ball.current.CheckBallIsPlayable(5f)  && !keep && !marcaFlag && !stun && !loadShoot)
        {
           // Debug.Log(name + " 1");
            SetBall();
            SetKeep();
            
        }
    
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball")) 
        {
            ballExit++;
            if (ballExit>1) 
            {
                Ball.current.player = null;
                ballExit = 0;
            }
        
        }
    }
    public virtual void OnTriggerStay2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.tag == "Ball" && Ball.current.CheckBallIsPlayable(5f)  && !keep && !marcaFlag && !stun && !loadShoot)
        {
          //  Debug.Log(name + " 2");
            SetKeep();
            SetBall();
        }
    }




    public void SetBall() { //M: sposta la palla nella posizione corretta all'interno del giocatore
        
        
            Ball.current.SetPlayer(this);
            Ball.current.transform.parent = transform;
            Ball.current.transform.position = transform.GetChild(0).position;
            Ball.current.freeFlag = false;
    }

    public void SetBallBoa()//M:la boa acquisisce il controllo del pallone
    {
        Ball.current.SetPlayer(this);
        Ball.current.DisableBall();
        Ball.current.transform.parent = transform;
        Ball.current.transform.position = transform.GetChild(7).position;
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
            if (_backSwim)
            {
                animator.SetInteger("IdAnim", 10);
            }
            else if(swim) 
            {
                animator.SetInteger("IdAnim", 1);
            }

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
        if (!keep && !keepBoa)
        {
            keep = true;
            keepBoa = false;
            swim = false;
            backSwim = false;
            bicy = false;
            def = false;
            loadShoot = false;
            ballFlag = true;
            stun = false;
             animator.SetInteger("IdAnim", 2);
            Utility.RotateObjToPoint(this.gameObject, posGoal);
            if (cpuFlag)
            {
                Invoke("BrainCpu", brain);
            }
        }
   }
    public void SetKeepBoa() 
    { if(!keep && !keepBoa)
            {
            keepBoa = true;
            keep = false;
            swim = false;
            backSwim = false;
            bicy = false;
            def = false;
            loadShoot = false;
            ballFlag = true;
            stun = false;
            animator.SetInteger("IdAnim", 20);
            if (cpuFlag)
            {
                Invoke("BrainCpu", brain);
            }
        }
    }

    public void SetShoot()
    {
        keep = false;
        swim = false;
        backSwim = false;
        bicy = false;
        def = false;
        loadShoot = true;
        stun = false;
        keepBoa = false;
        animator.SetInteger("IdAnim", 3);
    }

    public void SetShootBoaRovesciata()
    {
        keep = false;
        swim = false;
        backSwim = false;
        bicy = false;
        def = false;
        loadShoot = true;
        stun = false;
        keepBoa = false;
        Ball.current.transform.position = transform.GetChild(9).position;
        animator.SetInteger("IdAnim", 21);
    }
    public void SetShootBoaSciarpa()
    {
        keep = false;
        swim = false;
        backSwim = false;
        bicy = false;
        def = false;
        loadShoot = true;
        stun = false;
        keepBoa = false;
        Ball.current.transform.position = transform.GetChild(8).position;
        animator.SetInteger("IdAnim", 22);
    }
    public void SetShootBoaColonnello()
    {
        keep = false;
        swim = false;
        backSwim = false;
        bicy = false;
        def = false;
        loadShoot = true;
        stun = false;
        keepBoa = false;
        animator.SetInteger("IdAnim", 23);
    }
    public void LoadShoot(Vector3 directions, bool flag, int idShoot) //idShoot= 0: classico, 1 rovesciata, 2 sciarpa, 3 colonnello
    {
        Ball.current.isShooted = true;
        Ball.current.transform.parent = null; //Libera la palla 
        
        switch (idShoot)
        {
            case 0:
                SetShoot();
              
            break;

            case 1:
                SetShootBoaRovesciata();
              
                break;
            case 2:SetShootBoaSciarpa();

                break;
            case 3:SetShootBoaColonnello();
                break;

        }
        destShoot = directions;
        flagShoot = flag;

    }

    public void Shoot()  //M: chiamata dall'animazione
    {
        Ball.current.shoot = shoot;
        Ball.current.pas = pass;
        Ball.current.ShootBall(destShoot, flagShoot,0);
        
        Invoke("UpdateLoadShoot", waitAfterShoot); //tempo di attesa dopo aver tirato
    }
    public void UpdateLoadShoot() //M:fine tiro
    {
        SetBicy();
    }
    public void SetDef() //M: setta lo stato def
    {
        if (!def)
        {
        //    UnityEngine.DebugDebug.Log(name + " ->Def"+Time.time);
            keep = false;
            swim = false;
            backSwim = false;
            bicy = false;
            def = true;
            ballFlag = false;
            stun = false;
            keepBoa = false;
            animator.SetInteger("IdAnim", 4);
            Utility.RotateObjAtoB(this.gameObject, opponent.gameObject);
            
        }

    }

    public void SetStun() 
    {
        if (!stun)
        {
            keepBoa = false;
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

    public void MoveToAttPlayer()   //Sposta il giocatore verso il difensore

   {
        if (def)
        {
            if (arrivedFlagDef && CheckOpponentShoot() && distaceBall <= 10)
            {
                beginPush = true;
            }
            else if (!CheckOpponentShoot())
            {
                beginPush = false;
                pushAtt = false;
            }
              if (beginPush && def && !marcaFlag)
               {
                
                 LayerMask mask = 1 << 9; //strato player
                 Vector3 posSensor = transform.GetChild(6).transform.position;
                 Vector3 dir = (opponent.transform.position - posSensor).normalized;
                 float dist = 1;
                 RaycastHit2D hitAttPlayer = Physics2D.Raycast(posSensor, dir, dist, mask);
                Debug.DrawRay(posSensor, dir * dist, Color.black, 5f);
                 if (hitAttPlayer.collider != null)
                 {
                   

                    if (hitAttPlayer.collider.name == opponent.name)
                       {

                        pushAtt = false;
                        coverOpponent = true;
                       }
                 }
                else
                {
                    pushAtt = true;
                    coverOpponent = false;

                }
               
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
            coverOpponent = false;
        }
        
    }

    public bool CheckOpponentShoot() //Ritorna true, se l'avversario è in possesso o sta caricando il tiro o ha tirato e la palla sta viaggiando
    
    {
        if (Ball.current.player != null)
        {
            if (opponent.keep || opponent.loadShoot || (Ball.current.isShooted && Ball.current.player.name == opponent.name))
            {
                return true;
            }
            else
                return false;
        }
        else
            return false;
    }



    public void RestorePlayer() //funzione che tenta di resettare lo stato dei giocatori eliminando piccole imperfezioni
    {
        if (bicy) 
        {
            ballFlag = false;
        }
    
    }

    public virtual bool PlayerCpu()
    {
        return false;
    }
    
    public IEnumerator StartDecisioMaking() 
    {
        while (true)
        {
            bool stop = this.PlayerCpu();
            if (stop) 
            {
                yield break;         
            }

            yield return new WaitForSeconds(1);
        }
    }

    public void BrainCpu() 
    {
      //  Debug.Log(name+ "inizio a ragionare");
        if (brainCpuCoroutine != null) 
        {
            StopCoroutine(StartDecisioMaking());
        }
        brainCpuCoroutine = StartCoroutine(StartDecisioMaking());
    
    }
}

    


        

