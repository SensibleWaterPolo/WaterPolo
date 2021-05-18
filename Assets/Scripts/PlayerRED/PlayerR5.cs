public class PlayerR5 : Player
{
    // Start is called before the first frame update
    /* public override void Awake()
     {
         base.Awake();
         this.idTeam = 1;
         sectorAction = 3;
         posAtt = GameObject.Find("PosAttR5").transform.position;
         posDef = GameObject.Find("PosDefR5").transform.position;
         posMiddle = GameObject.Find("PosBattutaR5").transform.position;
         posStart = GameObject.Find("PosStartR5").transform.position;
         posCounter = GameObject.Find("PosCounterAttR5").transform.position;
         posGoal = GameObject.Find("GolLineYellow").transform.position;
         opponent = GameObject.Find("PlayerY1").GetComponent<Player>();
         posBallEndAction = GameObject.Find("DownDx").transform.position;
         armDx = true;
         cpuFlag = true;
     }*/

    /* public override bool PlayerCpu()
     {
         bool hasChoose = false;
         Vector3 destBall = Vector3.zero;
         int idZone = IA.current.ZoneBall(this);
         Player pR1 = GameObject.Find("PlayerR1").GetComponent<Player>();
         Player pR3 = GameObject.Find("PlayerR3").GetComponent<Player>();
         Player pR6 = GameObject.Find("PlayerR6").GetComponent<Player>();
         bool shoot = IA.current.DecisionShoot(this);
      //   Debug.Log(name + " decisione Tiro?: " + shoot);

         if (shoot) //Tiro in porta
         {
           //  Debug.Log(name + " Decido di tirare");

             if (GameCore.current.levelCPUHard)
             {
                 destBall = IA.current.ShootHardR5();
             }
             else
             {
               //  destBall = IA.current.ShootNormalR5();
             }
             Utility.RotateObjToPoint(this.gameObject, destBall);
             this.LoadShoot(destBall, shoot, 0);
             idDecisionCPU =0 ;
           //  Debug.Log("Tiro indirizzato a : " + destBall);
             return true;
         }
         /*********PASSAGGIO********************/
    /*    else
        {
          //  Debug.Log(name + " Decido di passare");
            switch (idZone)
            {
                case 0:

                    if (pR1.arrivedFlagCounterAtt)
                    {
                        destBall = GameObject.Find("PosCounterAttR1").transform.position;
                        Utility.RotateObjToPoint(this.gameObject, destBall);
                        this.LoadShoot(destBall, false, 0);
                        idDecisionCPU = 1;
                        return true;
                    }
                    else if (pR1.arrivedFlagAtt && pR3.arrivedFlagAtt)
                    {
                        if (Random.value > 0.5)
                        {
                            destBall = pR1.transform.position;
                            Utility.RotateObjToPoint(this.gameObject, destBall);
                            this.LoadShoot(destBall, false, 0);
                            idDecisionCPU = 2;
                            return true;
                        }
                        else
                        {
                            destBall = pR3.transform.position;
                            Utility.RotateObjToPoint(this.gameObject, destBall);
                            this.LoadShoot(destBall, false, 0);
                            idDecisionCPU = 3;
                            return true;
                        }
                    }
                    else if (pR1.arrivedFlagAtt)
                    {
                        destBall = pR1.transform.position;
                        Utility.RotateObjToPoint(this.gameObject, destBall);
                        this.LoadShoot(destBall, false, 0);
                        idDecisionCPU = 4;
                        return true;
                    }
                    else if (pR3.arrivedFlagAtt)
                    {
                        destBall = pR3.transform.position;
                        Utility.RotateObjToPoint(this.gameObject, destBall);
                        this.LoadShoot(destBall, false, 0);
                        idDecisionCPU = 5;
                        return true;
                    }
                    break;

                case 1:
                    {
                     if (pR6.marcaFlag && IA.current.PingBoaIsFree(this.name))
                        {
                            destBall = GameObject.Find("SensorBoaR6").transform.position;
                            Utility.RotateObjToPoint(this.gameObject, destBall);
                            this.LoadShoot(destBall, false, 0);
                            idDecisionCPU = 7;
                            return true;
                        }
                        else if (pR1.arrivedFlagAtt)
                        {
                            destBall = pR1.transform.position;
                            Utility.RotateObjToPoint(this.gameObject, destBall);
                            this.LoadShoot(destBall, false, 0);
                            idDecisionCPU = 8;
                            return true;
                        }
                        else if (pR3.arrivedFlagAtt)
                        {
                            destBall = pR3.transform.position;
                            Utility.RotateObjToPoint(this.gameObject, destBall);
                            this.LoadShoot(destBall, false, 0);
                            idDecisionCPU = 9;
                            return true;
                        }
                    }
                    break;

                case 2:
                    {
                        if (pR1.arrivedFlagCounterAtt)
                        {
                            destBall = GameObject.Find("PosCounterAttR1").transform.position;
                            Utility.RotateObjToPoint(this.gameObject, destBall);
                            this.LoadShoot(destBall, false, 0);
                            idDecisionCPU = 12;
                            return true;
                        }
                        else
                        if (pR6.marcaFlag && IA.current.PingBoaIsFree(this.name))
                        {
                            destBall = GameObject.Find("SensorBoaR6").transform.position;
                            Utility.RotateObjToPoint(this.gameObject, destBall);
                            this.LoadShoot(destBall, false, 0);
                            idDecisionCPU = 11;
                            return true;
                        }
                        else if (pR1.arrivedFlagAtt && pR3.arrivedFlagAtt)
                        {
                            if (Random.value > 0.5)
                            {
                                destBall = pR1.transform.position;
                                Utility.RotateObjToPoint(this.gameObject, destBall);
                                this.LoadShoot(destBall, false, 0);
                                idDecisionCPU = 13;
                                return true;
                            }
                            else
                            {
                                destBall = pR3.transform.position;
                                Utility.RotateObjToPoint(this.gameObject, destBall);
                                this.LoadShoot(destBall, false, 0);
                                idDecisionCPU = 14;
                                return true;
                            }
                        }
                        else if (pR1.arrivedFlagAtt)
                        {
                            destBall = pR1.transform.position;
                            Utility.RotateObjToPoint(this.gameObject, destBall);
                            this.LoadShoot(destBall, false, 0);
                            idDecisionCPU = 15;
                            return true;
                        }
                        else if (pR3.arrivedFlagAtt)
                        {
                            destBall = pR3.transform.position;
                            Utility.RotateObjToPoint(this.gameObject, destBall);
                            this.LoadShoot(destBall, false, 0);
                            idDecisionCPU = 16;
                            return true;
                        }
                    }
                    break;
            }
        }
        //  Debug.Log(name + " Non ho preso nessuna decisione");
        if (!swimKeep && !arrivedFlagAtt) {
            SetSwimKeep();
        }
        selected = false;
        return hasChoose;
    }
    */
}