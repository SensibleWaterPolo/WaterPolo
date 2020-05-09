using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerR6 : Player
{
    // Start is called before the first frame update

    public override void Awake()
    {
        base.Awake();
        this.idTeam = 1;
        sectorAction = 2;
        posAtt = GameObject.Find("PosAttR6").transform.position;
        posDef = GameObject.Find("PosDefR6").transform.position;
        posMiddle = GameObject.Find("PosBattutaR6").transform.position;
        posStart = GameObject.Find("PosStartR6").transform.position;
        posCounter = GameObject.Find("PosCounterAttR6").transform.position;
        posBallEndAction = GameObject.Find("DownDx").transform.position;
        posGoal = GameObject.Find("GolLineYellow").transform.position;
        opponent = GameObject.Find("PlayerY3").GetComponent<Player>();
        boaFlag = true;
        armDx = true;
        cpuFlag = true;
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (marcaFlag) {
            if (keepBoa) { GetComponent<CircleCollider2D>().enabled = true; }
            else {
                GetComponent<CircleCollider2D>().enabled = true;
            }
        }
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.CompareTag("Ball"))
        {
            if (collision.CompareTag("Ball") && !keep && !keepBoa && !swimKeep && !loadShoot && Ball.current.CheckBallIsPlayable(4) && marcaFlag)
            {
                Debug.Log(name+"prendo possesso"+Time.time);
                SetKeepBoa();
                // SetBallBoa();

            }
        }

    }

    public override void OnTriggerStay2D(Collider2D collision)
    {
      //  base.OnTriggerStay2D(collision);

      /*  if (collision.CompareTag("Ball"))
        {
            if (Ball.current.CheckBallIsPlayable(3) && marcaFlag && !animator.GetCurrentAnimatorStateInfo(0).IsName("Keep") && !animator.GetCurrentAnimatorStateInfo(0).IsName("KeepBoa"))
            {
                SetKeepBoa();
             //   SetBallBoa();

            }
          /*  else if (Ball.current.CheckBallIsPlayable(3) && !marcaFlag && !keep)

            {
                SetKeep();
              //  SetBall();
            }
            
        }*/
    }

    public override bool PlayerCpu()
    {
        bool hasChoose = false;
        Vector3 destBall = Vector3.zero;
        int idZone = IA.current.ZoneBall(this);
        Player pR5 = GameObject.Find("PlayerR5").GetComponent<Player>();
        Player pr1 = GameObject.Find("PlayerR1").GetComponent<Player>();
        Player pR3 = GameObject.Find("PlayerR3").GetComponent<Player>();
        bool shoot = IA.current.DecisionShoot(this);
      //  Debug.Log(name + " decisione Tiro?: " + shoot);

        if (shoot) //Tiro in porta
        {
            int idShoot = 0;
          //  Debug.Log(name + " Decido di tirare");
         if (!marcaFlag)
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
                this.LoadShoot(destBall, shoot, idShoot);
                idDecisionCPU = 0;
                return true;
          }
            else 
            {
                if (GameCore.current.levelCPUHard)
                {
                    destBall = IA.current.ShootHardR3();
                }
                else
                {
                    destBall = IA.current.ShootNormalR3();
                }
                int idPosBoa = IA.current.IdPosBoa();
                switch (idPosBoa) 
                {


                    
                    case 0:
                        if (destBall.x < 0)
                        {
                            idShoot = 2;
                        }
                        else if (destBall.x >= 0) {
                            idShoot = 1;
                        }

                         break;
                    case 1:
                        idShoot = 3;
                       break;
                    case 2:
                        idShoot = 1;
                        break;
                    case 3:
                        idShoot = 2;
                        break;
                }
                this.LoadShoot(destBall, shoot, idShoot);
                idDecisionCPU = 1;
                return true;
            }
            
        }
        /*********PASSAGGIO********************/
        else
        {
          //  Debug.Log(name + " Decido di passare, sono nella zona " + idZone);
            switch (idZone)
            {
                case 0:

               /*   if (pr1.counterAttFlag && pR5.counterAttFlag)
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
                        idDecisionCPU = 08;
                        return true;
                    }
                    else if (pr1.arrivedFlagAtt)
                    {
                        destBall = pr1.transform.position;
                        Utility.RotateObjToPoint(this.gameObject, destBall);
                        this.LoadShoot(destBall, false, 0);
                        idDecisionCPU = 09;
                        return true;
                    }
                    break;
                case 1:
                    {
                     /*if (pr1.counterAttFlag && pR5.counterAttFlag)
                        {
                            if (Random.value > 0.5)
                            {
                                destBall = GameObject.Find("PosCounterAttR5").transform.position;
                                Utility.RotateObjToPoint(this.gameObject, destBall);
                                this.LoadShoot(destBall, false, 0);
                                idDecisionCPU = 10;
                                return true;
                            }
                            else
                            {
                                destBall = GameObject.Find("PosCounterAttR1").transform.position;
                                Utility.RotateObjToPoint(this.gameObject, destBall);
                                this.LoadShoot(destBall, false, 0);
                                idDecisionCPU = 11;
                                return true;
                            }
                        }

                        else if (pR5.counterAttFlag)
                        {
                            destBall = GameObject.Find("PosCounterAttR5").transform.position;
                            Utility.RotateObjToPoint(this.gameObject, destBall);
                            this.LoadShoot(destBall, false, 0);
                            idDecisionCPU = 12;
                            return true;
                        }
                        else if (pr1.counterAttFlag)
                        {
                            destBall = GameObject.Find("PosCounterAttR1").transform.position;
                            Utility.RotateObjToPoint(this.gameObject, destBall);
                            this.LoadShoot(destBall, false, 0);
                            idDecisionCPU = 13;
                            return true;
                        }

                        else*/ if (pR5.arrivedFlagAtt && pr1.arrivedFlagAtt)
                        {
                            if (Random.value > 0.5)
                            {
                                destBall = pR5.transform.position;
                                Utility.RotateObjToPoint(this.gameObject, destBall);
                                this.LoadShoot(destBall, false, 0);
                                idDecisionCPU = 14;
                                return true;

                            }
                            else
                            {
                                destBall = pr1.transform.position;
                                Utility.RotateObjToPoint(this.gameObject, destBall);
                                this.LoadShoot(destBall, false, 0);
                                idDecisionCPU = 15;
                                return true;
                            }
                        }
                        else if (pR5.arrivedFlagAtt)
                        {
                            destBall = pR5.transform.position;
                            Utility.RotateObjToPoint(this.gameObject, destBall);
                            this.LoadShoot(destBall, false, 0);
                            idDecisionCPU = 16;
                            return true;
                        }
                        else if (pr1.arrivedFlagAtt)
                        {
                            destBall = pr1.transform.position;
                            Utility.RotateObjToPoint(this.gameObject, destBall);
                            this.LoadShoot(destBall, false, 0);
                            idDecisionCPU = 17;
                            return true;
                        }
                    }
                    break;

                case 2:
                    {
                    /*if (pr1.counterAttFlag && pR5.counterAttFlag)
                        {
                            if (Random.value > 0.5)
                            {
                                destBall = GameObject.Find("PosCounterAttR5").transform.position;
                                Utility.RotateObjToPoint(this.gameObject, destBall);
                                this.LoadShoot(destBall, false, 0);
                                idDecisionCPU = 18;
                                return true;
                            }
                            else
                            {
                                destBall = GameObject.Find("PosCounterAttR1").transform.position;
                                Utility.RotateObjToPoint(this.gameObject, destBall);
                                this.LoadShoot(destBall, false, 0);
                                idDecisionCPU = 19;
                                return true;
                            }
                        }

                        else if (pR5.counterAttFlag)
                        {
                            destBall = GameObject.Find("PosCounterAttR5").transform.position;
                            Utility.RotateObjToPoint(this.gameObject, destBall);
                            this.LoadShoot(destBall, false, 0);
                            idDecisionCPU = 20;
                            idDecisionCPU = 21;
                            return true;
                        }
                        else if (pr1.counterAttFlag)
                        {
                            destBall = GameObject.Find("PosCounterAttR1").transform.position;
                            Utility.RotateObjToPoint(this.gameObject, destBall);
                            this.LoadShoot(destBall, false, 0);
                            idDecisionCPU = 22;
                            return true;
                        }
                        else */if (pR5.arrivedFlagAtt && pr1.arrivedFlagAtt)
                        {
                            if (Random.value > 0.5)
                            {
                                destBall = pR5.transform.position;
                                Utility.RotateObjToPoint(this.gameObject, destBall);
                                this.LoadShoot(destBall, false, 0);
                                idDecisionCPU = 23;
                                return true;

                            }
                            else
                            {
                                destBall = pr1.transform.position;
                                Utility.RotateObjToPoint(this.gameObject, destBall);
                                this.LoadShoot(destBall, false, 0);
                                idDecisionCPU = 24;
                                return true;
                            }
                        }
                        else if (pR5.arrivedFlagAtt)
                        {
                            destBall = pR5.transform.position;
                            Utility.RotateObjToPoint(this.gameObject, destBall);
                            this.LoadShoot(destBall, false, 0);
                            idDecisionCPU = 25;
                            return true;
                        }
                        else if (pr1.arrivedFlagAtt)
                        {
                            destBall = pr1.transform.position;
                            Utility.RotateObjToPoint(this.gameObject, destBall);
                            this.LoadShoot(destBall, false, 0);
                            idDecisionCPU = 26;
                            return true;
                        }
                    }
                    break;

            }
        }
        //   Debug.Log(name + " Non ho preso nessuna decisione");
        selected = false;
        return hasChoose;
    }


}
