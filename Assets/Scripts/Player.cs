using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour

{
    [Header("SECTOR")]
    public int sectorAction; //M: settore di azione del giocatore 1: Sx, 2: Mid, 3: Dx
    public int idDecisionMaking; //Indentifica la scelta presa dal giocatore per passare da uno stato all'altro
    public int idDecisionCPU;


    private EPosizionePalla _ePosizionePalla;

    private EStato _eStatoCorrente;

    private EPosizione _ePosizioneGiocatore;


    public int idAnim; //m: ID ANIMAZIONE

    [Header("STAT Var")] //M: statistiche giocatore
    [Range(0, 50)]
    public float speed;
    public float pass;
    [Range(0f, 10f)]
    public float shoot;
    public int stamina;//M: forza del giocatore per vincere lo scontro, se mosso da cpu ha un minimo X e un massimo Y
    public int brain; //velocità della cpu nell'eseguire un'azione di passaggio o di tiro
    public Vector2 clickCPU; //range di click della cpu, da un minimo X ad un massimo Y, utilizzati per decidere chi vince uno scontro 

    internal void OnTriggerEnter2D(Collider2D collision)
    {
        throw new System.NotImplementedException();
    }

    protected bool armDx; //M: true se è destro, false se è un mancino
    public bool isInContact; //True se sono in contatto con un altro giocatore


    [Header("FLAG")]
    public bool cpuFlag; //M: true se muove la cpu
    public bool ballFlag; //M: true se è in possesso della palla
    public bool counterAttFlag; //M: true se è in controfuga
    public bool fightFlag; //M:il giocatore ha perso lo scontro
    public bool flagShoot; //M: true se si sta preparando ad un tiro, false se è un passaggio


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
    public Vector3 posCounter;
    public Vector3 posBallEndAction;

    [Header("OPPONENT")]//M:fa riferimento al diretto avversario
    public Player opponent;

    [Header("ANIMATION")]
    private Animator _animator;
    protected RuntimeAnimatorController ctrlAnim;

    public float limitVelBall; //M: limite per il quale intercetto il passaggio

    protected int ballExit = 0; //M: risolve un bug per il quale la palla esce due volte dalla collisione

    public bool selected; //true se il giocatore è selezionato per tirare o passare


    public int idTeam; //M: 0:YELLOW 1:RED


    //VARIABILI PER LE BOE
    public bool marcaFlag; //M:Variabile utilizzata per far ruotare le boe
    public bool boaFlag;  //M: per le animazione delle boe
    public float angleBoaZ; //M: angolodi rotazione della boa
    public float angleBoaW;

    //STATISTICHE RISPETTO LA POSIZIONE
    public float distaceBall; //M:distanza dalla palla
    public float distanceAtt; //M:distanza dal punto di attacco
    public float distanceDef; //M:distanza dal punto di difesa

    //VARIABILI PER LA DIFESA
    public bool pushAtt; //se in fase di difesa devo avanzare verso il difensore
    public bool beginPush;//comincia ad avanzare verso l'attaccante
    public float speedToPush;//velocità di spostamento verso l'attaccante
    public bool coverOpponent; //true se sto coprendo quasi tutto lo specchio della porta
    public int distOpponentToDef; //Distanza minima per difendersi dall'avversario
    public float waitAfterShoot; //Tempo di attesa dopo aver tirato

    //VARIABILI COMBATTIMENTO
    public FightManager fightManager;
    public FightManager prefabFight;


    public Head head;
    internal bool arrivedFlagCounterAtt;

    public virtual void Awake()
    {
        /* animator = GetComponent<Animator>();
          swim = false;
          backSwim = false;
          bicy = true;
          keep = false;
          loadShoot = false;

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
          swimKeep = false;
          transform.GetChild(3).gameObject.SetActive(false);
          selected = false;
          distOpponentToDef = 0;
          waitAfterShoot = 1.5f;*/

    }
    public virtual void Start()
    {

        shoot = Random.Range(6, 10);
        pass = Random.Range(6, 10);
        speed = Random.Range(6, 10);
        stamina = Random.Range(6, 10);
        clickCPU = new Vector2(6, 12);
        transform.position = posStart;
        head = transform.GetChild(4).GetComponent<Head>();
        _animator = GetComponent<Animator>();


    }

    /* public virtual void FixedUpdate()
     {
         if (GameCore.current.isPlay && Ball.current.inGameFlag)
         {
             if (GameCore.current.secExpired && (keep || swimKeep || keepBoa) && !loadShoot)
             {

                 GameCore.current.ShootPlayer();

             }
             if (boaFlag)
             {
                 if (marcaFlag && !keepBoa && !keep)
                 {
                     GetComponent<Collider2D>().enabled = false;
                     transform.GetChild(10).GetComponent<BoxCollider2D>().enabled = true;
                 }
                 else
                 {
                     GetComponent<Collider2D>().enabled = true;
                     transform.GetChild(10).GetComponent<BoxCollider2D>().enabled = false;
                 }
             }


             if (!keep && !keepBoa && !stun && !Ball.current.isShooted && !fightFlag)
             {
                 UpdateIdBall();
                 UpdateState();
             }


             CheckBallSwim();

             if ((swim || backSwim || swimKeep))
             {

                 transform.GetChild(3).gameObject.SetActive(true);
                 Swim();
                 // SwimDodge();
             }
             else
             {
                 transform.GetChild(3).gameObject.SetActive(false);
             }

             if (!uploadShoot)
             {
                 //    AnimTrigger(); 
                 CheckUpdateAnim();
             }

             UpdateFlagCounterAtt();
             CheckArrivedToPos();
             MoveToAttPlayer();


         }
         else if (!Ball.current.inGameFlag)
         {

             //  AnimTrigger();
             CheckUpdateAnim();

         }
     }


     public void UpdateState()

     {
         if (Ball.current.inGameFlag && !Ball.current.isShooted && _statoCorrente != EStato.Stunnato && !fightFlag)
         {
             // Attiva o disattiva il braccio in caso di posizione di difesa
             if (def)
             {
                 transform.GetChild(2).gameObject.layer = LayerMask.NameToLayer("Arm");

             }
             else
             {
                 transform.GetChild(2).gameObject.layer = LayerMask.NameToLayer("Pool");

             }

             //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// STATO BICY

             // BICY :Bicicletta se la palla è libera non nel mio settore e attendo l'evoluzione del gioco
             if (idBall == 0 && Ball.current.inGameFlag && !keep && !loadShoot && (arrivedFlagAtt || arrivedFlagDef))
             {
                 idDecisionMaking = 1;
                 SetBicy();


             }
             //BICY: palla in possesso avversario, sono in posizione di difesa ma la palla non ce l'ha il mio diretto opponent
             if (idBall == 3 && arrivedFlagDef && def && Ball.current.statePos != sectorAction && !marcaFlag && !stun && !fightFlag && !opponent.ballFlag)
             {
                 idDecisionMaking = 2;
                 SetBicy();

             }




             ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// STATO SWIM
             // SWIM : La palla è libera NON nel mio settore ma sono quello più vicino della mia squadra

             //Scatto iniziale
             if (idBall == 0 && Ball.current.CheckBallIsPlayable(0) && Ball.current.idTeam == -1)
             {
                 idDecisionMaking = 22;
                 SetSwim(posAtt, false);

             }

             //SWIM: Palla vicina al boa o al marca boa
             if (idBall == 1 && marcaFlag && Ball.current.CheckBallIsPlayable(0) && distaceBall < opponent.distaceBall && PosPlayerMng.curret.GetPlayerNameNearBall() == name)
             {
                 idDecisionMaking = 3;
                 SetSwim(Ball.current.transform.position, false);


             }
             //SWIM: iL PIù VICINO PRENDE LA PALLA
             if (Ball.current.CheckBallIsPlayable(0) && PosPlayerMng.curret.GetPlayerForTeamNearBall(idTeam, boaFlag) == name)
             {
                 idDecisionMaking = 33;
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

             //SWIM: Palla libera nel mio settore e sono quello più vicino della mia squadra ma la palla è anche un pò distante dall'avversario

             if (idBall == 1 && Ball.current.CheckBallIsPlayable(0) && !loadShoot && !marcaFlag && PosPlayerMng.curret.GetPlayerForTeamNearBall(idTeam, boaFlag) == name && !stun && !fightFlag && !boaFlag && opponent.distaceBall > 3)
             {
                 idDecisionMaking = 6;
                 SetSwim(Ball.current.transform.position, false);

             }

             //SWIM: palla in possesso di un mio compagno
             if (idBall == 2 && !arrivedFlagAtt && !stun && !fightFlag && !swimKeep && Ball.current.gk == null)
             {
                 if (!counterAttFlag)
                 {

                     idDecisionMaking = 7;
                     SetSwim(posAtt, false);



                 }
                 else if (counterAttFlag && !arrivedFlagCounterAtt && !opponent.arrivedFlagDef && !marcaFlag)
                 {


                     idDecisionMaking = 8;

                     SetSwim(posCounter, true);


                 }
                 //Il difensore mi ha recuperato in difesa
                 else if (counterAttFlag && arrivedFlagCounterAtt && opponent.arrivedFlagDef && !marcaFlag)
                 {

                     idDecisionMaking = 888;

                     SetSwim(posAtt, true);

                 }
             }
             else

             if (idBall == 2 && Ball.current.gk != null)
             {
                 idDecisionMaking = 88;
                 if (!counterAttFlag)
                 {
                     SetSwim(posAtt, true);
                 }
                 else if (counterAttFlag && !arrivedFlagCounterAtt)
                 { SetSwim(posCounter, true); }



             }

             //SWIM: palla in possesso avversario
             if (idBall == 3 && !arrivedFlagDef && !marcaFlag && !stun && !fightFlag && !pushAtt && !def)
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

             if (idBall == 3 && !arrivedFlagDef && !opponent.ballFlag && !marcaFlag && Ball.current.speed < 3)
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
                     {

                         SetSwim(posDef, true);


                     }
                     else
                     {
                         SetSwim(posDef, false);


                     }

                 }

             }
             //La palla non è nel mio settore ma sono la persona più vicina
             if (idBall == 0 && Ball.current.CheckBallIsPlayable(0) && PosPlayerMng.curret.GetPlayerNameNearBall() == name && !stun && !fightFlag)
             {
                 idDecisionMaking = 133;
                 SetSwim(Ball.current.transform.position, false);


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
             if (idBall == 3 && marcaFlag && !boaFlag)
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
             else
             {

             }


         }
         ChangeLvlSprite();
     }
     public void UpdateIdBall()
     {
         distaceBall = Vector2.Distance(transform.position, Ball.current.transform.position);
         distanceAtt = Vector2.Distance(transform.position, posAtt);
         distanceDef = Vector2.Distance(transform.position, posDef);

         if (!Ball.current.freeFlag)
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
         else if (Ball.current.statePos != sectorAction && Ball.current.CheckBallIsPlayable(0))
         {
             idBall = 0;
         }

     }  //M: aggiorna lo stato della palla rispetto al giocatore;

     public virtual void OnTriggerEnter2D(Collider2D collision)
     {
         GameObject obj = collision.gameObject;

         if (obj.tag == "Ball" && !keep && !keepBoa && !swimKeep && !loadShoot && Ball.current.CheckBallIsPlayable(4) && !marcaFlag && !stun)
         {
             if (Ball.current.idTeam != idTeam)
             {

                 GameCore.current.RestartTimeAction();
             }
             Ball.current.freeFlag = false;
             SetKeep();

         }
         if (collision.CompareTag("Player"))
         {
             isInContact = true;
             if (swimKeep)
             {
                 SetKeep();
             }
             else
             {



             }

         }

     }


     private void OnTriggerExit2D(Collider2D collision)
     {
         if (collision.CompareTag("Player"))
         {
             isInContact = false;

         }
     }
     public virtual void OnTriggerStay2D(Collider2D collision)
     {
         GameObject obj = collision.gameObject;

         if (obj.tag == "Ball" && !keep && !keepBoa && !swimKeep && !loadShoot && Ball.current.CheckBallIsPlayable(4) && !marcaFlag && !stun)
         {
             if (Ball.current.idTeam != idTeam)
             {
                 GameCore.current.RestartTimeAction();
             }
             Ball.current.freeFlag = false;
             SetKeep();

         }
         if (collision.CompareTag("Player"))
         {
             isInContact = true;
             if (swimKeep)
             {

                 SetKeep();
             }

         }
     }*/
    public void SetBall()
    { //M: sposta la palla nella posizione corretta all'interno del giocatore


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
    }
    private void SetStatoBicicletta()
    {
        _eStatoCorrente = EStato.Bicicletta;

        switch (_ePosizioneGiocatore)
        {
            case EPosizione.InPosAttacco:

                if (!boaFlag)
                {
                    Utility.RotateObjToPoint(this.gameObject, posGoal);
                }
                break;

            case EPosizione.InPosDifesa:
                Utility.RotateObjToPoint(this.gameObject, opponent.transform.position);
                break;
        }
    }


    public void SetSwim(Vector3 nextPos, bool backSwim)
    {
        if (!Ball.current.isShooted && _eStatoCorrente != EStato.Possesso
                && _eStatoCorrente != EStato.CaricaTiro && _eStatoCorrente != EStato.NuotaConPalla
                    && _eStatoCorrente != EStato.Stunnato)
        {
            if (nextPos != posFinal)
                posFinal = nextPos;

            switch (backSwim)
            {
                case true:
                    _eStatoCorrente = EStato.Dorso;
                    break;
                case false:
                    _eStatoCorrente = EStato.Nuoto;
                    break;
            }
        }
    }

    public void Swim() //M: nuova funzione per il nuoto
    {

        /* float distanzaMancante = (this.transform.position - posFinal).sqrMagnitude;

         if (distanzaMancante > 0)
         {
             Vector3 nuova_Pos = Vector3.MoveTowards(transform.position, posFinal, (speed + 2) * Time.deltaTime);
             GetComponent<Rigidbody2D>().MovePosition(nuova_Pos);
             Utility.RotateObjToPoint(this.gameObject, posFinal);

         }
         else
         {

             if (swimKeep)
             {
                 SetKeep();
             }
             else
             {

                 SetBicy();
             }
         }*/
    }

    public void SetKeep()
    {
        //  UnityEngine.Debug.Log(name + " ->possesso" + Time.time);
        /*  if (!keep && !stun)
          {


              //animator.SetInteger("IdAnim", 2);
              keep = true;
              keepBoa = false;
              swim = false;
              backSwim = false;
              bicy = false;
              def = false;
              loadShoot = false;
              ballFlag = true;
              stun = false;
              swimKeep = false;
              colonnello = false;
              sciarpa = false;
              rovesciata = false;


              SetBall();
              AnimTrigger();
              Utility.RotateObjToPoint(this.gameObject, posGoal);


              if (cpuFlag)
              {
                  brain = Random.Range(1, 3);
                  Invoke("BrainCpu", brain);
                  if (idTeam == 1 && transform.position.y > posAtt.y)
                  {
                      Invoke("SetSwimKeep", 1);
                  }
              }

          }*/
    }
    public void SetKeepBoa()
    {
        /*    if (!keep && !keepBoa && !selected && !swimKeep)
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
                swimKeep = false;

                colonnello = false;
                sciarpa = false;
                rovesciata = false;

                SetBallBoa();
                AnimTrigger();
                if (cpuFlag)
                {
                    brain = Random.Range(1, 3);
                    Invoke("BrainCpu", brain);
                }
            }*/
    }

    public void SetShoot()
    {


        /*   keep = false;
           swim = false;
           backSwim = false;
           bicy = false;
           def = false;
           loadShoot = true;
           stun = false;
           swimKeep = false;
           keepBoa = false;

           colonnello = false;
           sciarpa = false;
           rovesciata = false;
           AnimatorClipInfo[] infoClip = animator.GetCurrentAnimatorClipInfo(0);
           if (infoClip.Length > 0)
           {
               if (infoClip[0].clip.name != "SHOOT")
               {
                   animator.CrossFade("SHOOT", 0.1f);
               }
           }
           //   animator.SetTrigger("Shoot");*/


    }

    public void SetShootBoaRovesciata()
    {
        /*      if (keepBoa)
              {
                  keep = false;
                  swim = false;
                  backSwim = false;
                  bicy = false;
                  def = false;
                  loadShoot = true;
                  swimKeep = false;
                  stun = false;
                  keepBoa = false;
                  colonnello = false;
                  sciarpa = false;
                  rovesciata = true;

                  Ball.current.transform.position = transform.GetChild(9).position;
                  AnimatorClipInfo[] infoClip = animator.GetCurrentAnimatorClipInfo(0);
                  if (infoClip.Length > 0)
                  {
                      if (infoClip[0].clip.name != "ROVESCIATA")
                      {
                          animator.CrossFade("ROVESCIATA", 0.1f);
                      }
                  }
                  //   animator.SetTrigger("Rovesciata");
              }*/
    }
    public void SetShootBoaSciarpa()
    {
        /*  if (keepBoa)
          {
              keep = false;
              swim = false;
              backSwim = false;
              bicy = false;
              def = false;
              loadShoot = true;
              stun = false;
              swimKeep = false;
              keepBoa = false;
              colonnello = false;
              sciarpa = true;
              rovesciata = false;

              Ball.current.transform.position = transform.GetChild(8).position;
              AnimatorClipInfo[] infoClip = animator.GetCurrentAnimatorClipInfo(0);
              if (infoClip.Length > 0)
              {
                  if (infoClip[0].clip.name != "SCIARPA")
                  {
                      animator.CrossFade("SCIARPA", 0.1f);
                  }
              }
              //animator.SetTrigger("Sciarpa");
          }*/
    }
    public void SetShootBoaColonnello()
    {
        /*    if (keepBoa)
            {
                keep = false;
                swim = false;
                backSwim = false;
                bicy = false;
                def = false;
                loadShoot = true;
                stun = false;
                keepBoa = false;
                swimKeep = false;
                colonnello = true;
                sciarpa = false;
                rovesciata = false;
                AnimatorClipInfo[] infoClip = animator.GetCurrentAnimatorClipInfo(0);
                if (infoClip.Length > 0)
                {
                    if (infoClip[0].clip.name != "COLONNELLO")
                    {
                        animator.CrossFade("COLONNELLO", 0.1f);
                    }
                }
                // animator.SetTrigger("Colonnello");

            }*/
    }
    public void LoadShoot(Vector3 directions, bool flag, int idShoot) //idShoot= 0: classico, 1 rovesciata, 2 sciarpa, 3 colonnello
    {
        /*   uploadShoot = true;
           animator.ResetTrigger("Keep");
           if (boaFlag)
           {
               animator.ResetTrigger("KeepBoa");
           }
           if (cpuFlag)
           {

               selected = true;
           }
           if (keep || swimKeep || keepBoa)
           {
               Ball.current.transform.parent = null; //Libera la palla 



               switch (idShoot)
               {
                   case 0:


                       SetShoot();


                       break;

                   case 1:

                       SetShootBoaRovesciata();



                       break;
                   case 2:


                       SetShootBoaSciarpa();


                       break;
                   case 3:


                       SetShootBoaColonnello();

                       break;

               }
               destShoot = directions;
               flagShoot = flag;
           }*/
    }

    public void Shoot()  //M: chiamata dall'animazione
    {
        /*     Ball.current.shoot = shoot;
             Ball.current.pas = pass;
             Utility.RotateObjToPoint(this.gameObject, destShoot);
             Ball.current.ShootBall(destShoot, flagShoot, 0);

             Invoke("UpdateLoadShoot", waitAfterShoot); //tempo di attesa dopo aver tirato*/
    }
    public void UpdateLoadShoot() //M:fine tiro
    {
        /*  if (loadShoot)
          {
              SetBicy();
          }*/
    }
    public void SetDef() //M: setta lo stato def
    {
        /*   if (!def && !keep && !keepBoa && !swimKeep && !stun)
           {

               keep = false;
               swim = false;
               backSwim = false;
               bicy = false;
               def = true;
               ballFlag = false;
               swimKeep = false;
               stun = false;
               keepBoa = false;
               colonnello = false;
               sciarpa = false;
               rovesciata = false;


               Utility.RotateObjAtoB(this.gameObject, opponent.gameObject);

           }*/

    }

    public void SetStun()
    {
        /*   if (!stun)
           {
               keepBoa = false;
               stun = true;
               bicy = false;
               keep = false;
               backSwim = false;
               bicy = false;
               def = false;
               ballFlag = false;
               swimKeep = false;
               colonnello = false;
               sciarpa = false;
               rovesciata = false;
               GameCore.current.AddPlayerStun(this);
               Utility.RotateObjToPoint(this.gameObject, posFinal);

           }*/

    }
    public void SetSwimKeep()
    {

        /*    if (keep && !swimKeep && !selected && !arrivedFlagAtt)
            {

                posFinal = posAtt;
                swimKeep = true;
                keepBoa = false;
                swim = false;
                stun = false;
                bicy = false;
                keep = false;
                backSwim = false;
                bicy = false;
                def = false;
                ballFlag = false;
                colonnello = false;
                sciarpa = false;
                rovesciata = false;


                Utility.RotateObjToPoint(this.gameObject, posFinal);
            }*/


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

    public void StartGame()
    {

    }



    public float DistanceFromBall()
    { //M calcola la distanza tra il giocatore e la palla
        return Vector2.Distance(transform.position, Ball.current.transform.position);
    }

    public void CheckArrivedToPos()
    {
        /* arrivedFlagDef = transform.position == posDef;
         arrivedFlagBall = keep;
         arrivedFlagAtt = transform.position == posAtt;
         arrivedFlagCounterAtt = transform.position == posCounter;

         if (arrivedFlagAtt && bicy && !marcaFlag)
         {
             Utility.RotateObjToPoint(this.gameObject, posGoal);
             return;
         }

         else if (arrivedFlagDef && bicy)
         {
             Utility.RotateObjAtoB(this.gameObject, opponent.gameObject);
             return;
         }
        */
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
        if (Vector3.Distance(posGoal, transform.position) < Vector3.Distance(posGoal, opponent.transform.position))
        {
            counterAttFlag = true;

        }
        else
        {
            counterAttFlag = false;

        }
    }

    public void MoveToAttPlayer()   //Sposta il giocatore verso il difensore

    {
        /*   if (def)
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
                   float dist = distOpponentToDef;
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
           }*/

    }

    public bool CheckOpponentShoot() //Ritorna true, se l'avversario è in possesso o sta caricando il tiro o ha tirato e la palla sta viaggiando

    {
        /*if (Ball.current.player != null)
        {
           if (opponent.keep || opponent.loadShoot || (Ball.current.isShooted && Ball.current.player.name == opponent.name))
            {
                return true;
            }
            else
                return false;
        }
        else
            return false;*/
        return true;
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
        /*   if (brainCpuCoroutine != null)
          {
               StopCoroutine(StartDecisioMaking());
           }
           brainCpuCoroutine = StartCoroutine(StartDecisioMaking());*/

    }


    public IEnumerator LifeCycle()
    {
        /*  while (true)
          {
              if (GameCore.current.isPlay && Ball.current.inGameFlag && !stun && !fightFlag)
              {
                  if (GameCore.current.secExpired && keep && !loadShoot)
                  {

                      GameCore.current.ShootPlayer();

                  }
                  if (boaFlag)
                  {
                      if (marcaFlag && !keepBoa && !keep)
                      {
                          GetComponent<Collider2D>().enabled = false;
                          transform.GetChild(10).GetComponent<BoxCollider2D>().enabled = true;
                      }
                      else
                      {
                          GetComponent<Collider2D>().enabled = true;
                          transform.GetChild(10).GetComponent<BoxCollider2D>().enabled = false;
                      }
                  }

                  UpdateIdBall();
                  if (!keep && !keepBoa && !stun && !Ball.current.isShooted && !fightFlag)
                  { UpdateState(); }


                  CheckBallSwim();

                  if ((swim || backSwim || swimKeep))
                  {

                      transform.GetChild(3).gameObject.SetActive(true);
                      Swim();
                      // SwimDodge();
                  }
                  else
                  {
                      transform.GetChild(3).gameObject.SetActive(false);
                  }

                  if (!uploadShoot)
                  {
                      AnimTrigger();
                  }

                  UpdateFlagCounterAtt();
                  CheckArrivedToPos();
                  MoveToAttPlayer();


              }
              else if (!Ball.current.inGameFlag)
              {

                  AnimTrigger();
              }
  */
        yield return new WaitForFixedUpdate();
        //}

    }

    public void CheckBallSwim()
    {
        /*  if (swimKeep && selected)
          {
              SetKeep();
          }*/

    }
    void AnimTrigger()
    {

        /* foreach (AnimatorControllerParameter p in animator.parameters)
             if (p.type == AnimatorControllerParameterType.Trigger)
                 animator.ResetTrigger(p.name);



         if (bicy)
         {
             animator.SetTrigger("Bicy");
             return;
         }
         else if (swim)
         {
             animator.SetTrigger("Swim");
             return;
         }
         else if (keep)
         {
             animator.SetTrigger("Keep");

         }
         else if (backSwim)
         {
             animator.SetTrigger("BackSwim");
             return;
         }
         else if (def)
         {
             animator.SetTrigger("Def");
             return;
         }
         else if (swimKeep)
         {
             animator.SetTrigger("SwimBall");
             return;
         }
         else if (keepBoa)
         {
             animator.SetTrigger("KeepBoa");
             return;
         }
         else if (stun)
         {
             animator.SetTrigger("Stun");
             return;

         }*/

    }

    public void ChangeLvlSprite() //caambia il livello di render a seconda dello stato del giocatore
    {
        /*    if (swimKeep || keep)
            {
                GetComponent<SpriteRenderer>().sortingLayerName = "MarcaBoa";
            }
            else
            {
                GetComponent<SpriteRenderer>().sortingLayerName = "Player";
            }*/

    }

    public void SwimDodge() //M: nuova funzione per il nunoto
    {


        /*  if (!head.inCollision)
          {
              float distanzaMancante = (this.transform.position - posFinal).sqrMagnitude;

              if (distanzaMancante > 0)
              {
                  Vector3 nuova_Pos = Vector3.MoveTowards(transform.position, posFinal, (speed + 2) * Time.deltaTime);
                  GetComponent<Rigidbody2D>().MovePosition(nuova_Pos);
                  Utility.RotateObjToPoint(this.gameObject, posFinal);

              }
              else
              {

                  if (swimKeep)
                  {
                      SetKeep();
                  }
                  else
                  {

                      SetBicy();
                  }
              }
          }
          else if (head.inCollision)
          {
              Debug.Log("Cambio direzione");
              ChandeDirForDodge();
          }*/
    }




    //Controlla se il giocatore può entrare in combattimento
    public bool CanFight(string _name)
    {

        /*   bool result = (swim || backSwim) && !stun && !arrivedFlagDef && !arrivedFlagAtt && Ball.current.inGameFlag && !marcaFlag && !fightFlag && opponent.name == _name && !boaFlag;
           Debug.Log(name + " Can Fight:" + result);
           return result;*/
        return true;
    }

    public void HidePlayer()
    {
        GetComponent<Renderer>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;

    }

    public void ShowPlayer()
    {
        GetComponent<Renderer>().enabled = true;
        GetComponent<CircleCollider2D>().enabled = true;
    }

    public void CheckUpdateAnim()
    {
        /* AnimatorClipInfo[] infoClip = animator.GetCurrentAnimatorClipInfo(0);

         if (infoClip.Length > 0)
         {
             if (bicy && !(infoClip[0].clip.name == "BICY"))
             {
                 animator.CrossFade("BICY", 0.1f);
                 return;
             }
             else if (swim && !(infoClip[0].clip.name == "SWIM"))
             {
                 animator.CrossFade("SWIM", 0.1f);
                 return;
             }
             else if (keep && !(infoClip[0].clip.name == "KEEP"))
             {
                 animator.CrossFade("KEEP", 0.1f);
                 return;
             }
             else if (backSwim && !(infoClip[0].clip.name == "BACKSWIM"))
             {
                 animator.CrossFade("BACKSWIM", 0.1f);
                 return;
             }
             else if (def && !(infoClip[0].clip.name == "DEF"))
             {
                 animator.CrossFade("DEF", 0.1f);
                 return;
             }
             else if (swimKeep && !(infoClip[0].clip.name == "SWIMBALL"))
             {
                 animator.CrossFade("SWIMBALL", 0.1f);
                 return;
             }
             else if (keepBoa && !(infoClip[0].clip.name == "KEEPBOA"))
             {
                 animator.CrossFade("KEEPBOA", 0.1f);
                 return;
             }
             else if (stun && !(infoClip[0].clip.name == "STUN"))
             {
                 animator.CrossFade("STUN", 0.1f);
                 return;

             }

         }*/


    }


    public enum EPosizionePalla
    {
        LiberaNonInZona,
        LiberaInZona,
        PossessoAmico,
        PossessoAvversario,
        Possesso,
        PallaVicina,
        NoSense
    }

    public enum EStato
    {
        Bicicletta,
        Nuoto,
        Possesso,
        Difesa,
        Dorso,
        Stunnato,
        PossessoBoa,
        NuotaConPalla,
        CaricaTiro,
        Collonnello,
        Sciarpa,
        FRovesciata,
        InFight
    }

    public enum EPosizione
    {
        InPosAttacco,
        InPosDifesa,
        SullaPalla,
        InPosControfuga
    }

}




