using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerR3 : Player
{
    // Start is called before the first frame update

    public Battle battle;
    public Battle battlePrefab;
   
    
    public override void Awake()
    {
        base.Awake();
        this.idTeam = 1;
        sectorAction = 2;
       posAtt = GameObject.Find("PosAttR3").transform.position;
        posDef = GameObject.Find("PosDefR3").transform.position;
        posMiddle = GameObject.Find("PosBattutaR3").transform.position;
         posStart = GameObject.Find("PosStartR3").transform.position;
        posBallEndAction = GameObject.Find("DownSx").transform.position;
        posGoal = GameObject.Find("GolLineYellow").transform.position;
        opponent = GameObject.Find("PlayerY6").GetComponent<Player>();
        posCounter = Vector3.zero ;
        armDx = true;
        cpuFlag = true;
    }
    public override void OnTriggerStay2D(Collider2D collision)
    {
        base.OnTriggerStay2D(collision);
        if (collision.gameObject.name == opponent.name && opponent.arrivedFlagAtt && arrivedFlagDef && !marcaFlag && !keep && !opponent.keep && !opponent.swimKeep && idBall==3)
        {            
            
            battlePrefab = Instantiate(battle, opponent.transform.position, Quaternion.Euler(new Vector3(0, 0, Utility.GetAngleBetweenObjAB(opponent.gameObject, Ball.current.gameObject))));
            battlePrefab.CreateBattle(opponent, this);
        }
        GameObject obj = collision.gameObject;

        if (obj.tag == "Ball" && !keep && !keepBoa && !swimKeep && !loadShoot && Ball.current.CheckBallIsPlayable(20) && !marcaFlag)
        {

            Ball.current.freeFlag = false;
            SetKeep();

        }

      
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if(collision.CompareTag("Ball") && !keep && !swimKeep && !loadShoot && Ball.current.CheckBallIsPlayable(60)&& !Ball.current.shootFlag && marcaFlag)
            {
                if (Ball.current.idTeam != idTeam)
                {

                    GameCore.current.RestartTimeAction();
                }
                Ball.current.freeFlag = false;
                SetKeep();

            }
    }
    public override bool PlayerCpu()
    {
        bool hasChoose = false;
        Vector3 destBall = Vector3.zero;
        int idZone = IA.current.ZoneBall(this);
        Player pR5 = GameObject.Find("PlayerR5").GetComponent<Player>();
        Player pr1 = GameObject.Find("PlayerR1").GetComponent<Player>();
        Player pR6 = GameObject.Find("PlayerR6").GetComponent<Player>();
        bool shoot = IA.current.DecisionShoot(this);
        

        if (shoot) //Tiro in porta
        {
          

            if (GameCore.current.levelCPUHard)
            {
                destBall = IA.current.ShootHardR3();
            }
            else
            {
                destBall = IA.current.ShootNormalR3();
            }
            Utility.RotateObjToPoint(this.gameObject, destBall);
            this.LoadShoot(destBall, shoot, 0);
            idDecisionCPU = 0;
            return true;
        }
        /*********PASSAGGIO********************/
        else
        {
            
            switch (idZone)
            {
                case 0:

                   /* if (pR6.counterAttFlag && !pR6.marcaFlag)
                    {
                        destBall = GameObject.Find("PosCounterAttR6").transform.position;
                        Utility.RotateObjToPoint(this.gameObject, destBall);
                        this.LoadShoot(destBall, false, 0);
                        idDecisionCPU = 1;
                        return true;
                    }
                    else if (pr1.counterAttFlag && pR5.counterAttFlag )
                    {
                        if (Random.value > 0.5)
                        {
                            destBall = GameObject.Find("PosCounterAttR5").transform.position;
                            Utility.RotateObjToPoint(this.gameObject, destBall);
                            this.LoadShoot(destBall, false, 0);
                            idDecisionCPU = 2;
                            return true;
                        }
                        else 
                        {
                            destBall = GameObject.Find("PosCounterAttR1").transform.position;
                            Utility.RotateObjToPoint(this.gameObject, destBall);
                            this.LoadShoot(destBall, false, 0);
                            idDecisionCPU = 3;
                            return true;
                        }
                    }

                    else if (pR5.counterAttFlag)
                    {
                        destBall = GameObject.Find("PosCounterAttR5").transform.position;
                        Utility.RotateObjToPoint(this.gameObject, destBall);
                        this.LoadShoot(destBall, false, 0);
                        idDecisionCPU = 4;
                        return true;
                    }
                    else if (pr1.counterAttFlag)
                    {
                        destBall = GameObject.Find("PosCounterAttR1").transform.position;
                        Utility.RotateObjToPoint(this.gameObject, destBall);
                        this.LoadShoot(destBall, false, 0);
                        idDecisionCPU = 5;
                        return true;
                    }

                    else*/ if (pR5.arrivedFlagAtt && pr1.arrivedFlagAtt)
                    {
                        if (Random.value > 0.5)
                        {
                            destBall = pR5.transform.position;
                            Utility.RotateObjToPoint(this.gameObject, destBall);
                            this.LoadShoot(destBall, false, 0);
                            idDecisionCPU = 6;
                            return true;

                        }
                        else
                        {
                            destBall = pr1.transform.position;
                            Utility.RotateObjToPoint(this.gameObject, destBall);
                            this.LoadShoot(destBall, false, 0);
                            idDecisionCPU = 7;
                            return true;
                        }
                    }
                    else if (pR5.arrivedFlagAtt)
                    {
                        destBall = pR5.transform.position;
                        Utility.RotateObjToPoint(this.gameObject, destBall);
                        this.LoadShoot(destBall, false, 0);
                        idDecisionCPU = 8;
                        return true;
                    }
                    else if (pr1.arrivedFlagAtt)
                    {
                        destBall = pr1.transform.position;
                        Utility.RotateObjToPoint(this.gameObject, destBall);
                        this.LoadShoot(destBall, false, 0);
                        idDecisionCPU = 9;
                        return true;
                    }
                    break;
                case 1:
                    {
                      /*  if (pR6.counterAttFlag && !pR6.marcaFlag)
                        {
                            destBall = GameObject.Find("PosCounterAttR6").transform.position;
                            Utility.RotateObjToPoint(this.gameObject, destBall);
                            this.LoadShoot(destBall, false, 0);
                            idDecisionCPU = 10;
                            return true;
                        }
                        else if (pr1.counterAttFlag && pR5.counterAttFlag)
                        {
                            if (Random.value > 0.5)
                            {
                                destBall = GameObject.Find("PosCounterAttR5").transform.position;
                                Utility.RotateObjToPoint(this.gameObject, destBall);
                                this.LoadShoot(destBall, false, 0);
                                idDecisionCPU = 11;
                                return true;
                            }
                            else
                            {
                                destBall = GameObject.Find("PosCounterAttR1").transform.position;
                                Utility.RotateObjToPoint(this.gameObject, destBall);
                                this.LoadShoot(destBall, false, 0);
                                idDecisionCPU = 12;
                                return true;
                            }
                        }

                        else if (pR5.counterAttFlag)
                        {
                            destBall = GameObject.Find("PosCounterAttR5").transform.position;
                            Utility.RotateObjToPoint(this.gameObject, destBall);
                            this.LoadShoot(destBall, false, 0);
                            idDecisionCPU = 13;
                            return true;
                        }
                        else if (pr1.counterAttFlag)
                        {
                            destBall = GameObject.Find("PosCounterAttR1").transform.position;
                            Utility.RotateObjToPoint(this.gameObject, destBall);
                            this.LoadShoot(destBall, false, 0);
                            idDecisionCPU = 14;
                            return true;
                        }
                        
                        else */if (pR6.marcaFlag && IA.current.PingBoaIsFree(this.name) && !opponent.def)
                        {
                            destBall = GameObject.Find("SensorBoaR6").transform.position;
                            Utility.RotateObjToPoint(this.gameObject, destBall);
                            this.LoadShoot(destBall, false, 0);
                            idDecisionCPU = 15;
                            return true;
                        }
                        else if (pR5.arrivedFlagAtt && pr1.arrivedFlagAtt)
                        {
                            if (Random.value > 0.5)
                            {
                                destBall = pR5.transform.position;
                                Utility.RotateObjToPoint(this.gameObject, destBall);
                                this.LoadShoot(destBall, false, 0);
                                idDecisionCPU = 16;
                                return true;

                            }
                            else
                            {
                                destBall = pr1.transform.position;
                                Utility.RotateObjToPoint(this.gameObject, destBall);
                                this.LoadShoot(destBall, false, 0);
                                idDecisionCPU = 17;
                                return true;
                            }
                        }
                        else if (pR5.arrivedFlagAtt)
                        {
                            destBall = pR5.transform.position;
                            Utility.RotateObjToPoint(this.gameObject, destBall);
                            this.LoadShoot(destBall, false, 0);
                            idDecisionCPU = 18;
                            return true;
                        }
                        else if (pr1.arrivedFlagAtt)
                        {
                            destBall = pr1.transform.position;
                            Utility.RotateObjToPoint(this.gameObject, destBall);
                            this.LoadShoot(destBall, false, 0);
                            idDecisionCPU = 19;
                            return true;
                        }
                    }
                    break;

                case 2:
                    {
                        /*if (pR6.counterAttFlag && !pR6.marcaFlag)
                        {
                            destBall = GameObject.Find("PosCounterAttR6").transform.position;
                            Utility.RotateObjToPoint(this.gameObject, destBall);
                            this.LoadShoot(destBall, false, 0);
                            idDecisionCPU = 20;
                            return true;
                        }

                        else */if (pR6.marcaFlag && IA.current.PingBoaIsFree(this.name) && IA.current.PingBoaIsFree("PlayerR3") && !opponent.def)
                        {   
                            destBall = GameObject.Find("SensorBoaR6").transform.position;
                            Utility.RotateObjToPoint(this.gameObject, destBall);
                            this.LoadShoot(destBall, false, 0);
                            idDecisionCPU = 21;
                            return true;
                        }
                       /* else if (pr1.counterAttFlag && pR5.counterAttFlag)
                        {
                            if (Random.value > 0.5)
                            {
                                destBall = GameObject.Find("PosCounterAttR5").transform.position;
                                Utility.RotateObjToPoint(this.gameObject, destBall);
                                this.LoadShoot(destBall, false, 0);
                                idDecisionCPU = 22;
                                return true;
                            }
                            else
                            {
                                destBall = GameObject.Find("PosCounterAttR1").transform.position;
                                Utility.RotateObjToPoint(this.gameObject, destBall);
                                this.LoadShoot(destBall, false, 0);
                                idDecisionCPU = 23;
                                return true;
                            }
                        }

                        else if (pR5.counterAttFlag)
                        {
                            destBall = GameObject.Find("PosCounterAttR5").transform.position;
                            Utility.RotateObjToPoint(this.gameObject, destBall);
                            this.LoadShoot(destBall, false, 0);
                            idDecisionCPU = 24;
                            return true;
                        }
                        else if (pr1.counterAttFlag)
                        {
                            destBall = GameObject.Find("PosCounterAttR1").transform.position;
                            Utility.RotateObjToPoint(this.gameObject, destBall);
                            this.LoadShoot(destBall, false, 0);
                            idDecisionCPU = 25;
                            return true;
                        }*/
                        else if (pR5.arrivedFlagAtt && pr1.arrivedFlagAtt)
                        {
                            if (Random.value > 0.5)
                            {
                                destBall = pR5.transform.position;
                                Utility.RotateObjToPoint(this.gameObject, destBall);
                                this.LoadShoot(destBall, false, 0);
                                idDecisionCPU = 26;
                                    return true;

                            }
                            else
                            {
                                destBall = pr1.transform.position;
                                Utility.RotateObjToPoint(this.gameObject, destBall);
                                this.LoadShoot(destBall, false, 0);
                                idDecisionCPU = 27;
                                return true;
                            }
                        }
                        else if (pR5.arrivedFlagAtt)
                        {
                            destBall = pR5.transform.position;
                            Utility.RotateObjToPoint(this.gameObject, destBall);
                            this.LoadShoot(destBall, false, 0);
                            idDecisionCPU = 28;
                            return true;
                        }
                        else if (pr1.arrivedFlagAtt)
                        {
                            destBall = pr1.transform.position;
                            Utility.RotateObjToPoint(this.gameObject, destBall);
                            this.LoadShoot(destBall, false, 0);
                            idDecisionCPU = 29;
                            return true;
                        }
                    }
                    break;

            }
        }
        selected = false;
        if (!swimKeep && !arrivedFlagAtt)
        {
            SetSwimKeep();
        }
        return hasChoose;
    }

}
