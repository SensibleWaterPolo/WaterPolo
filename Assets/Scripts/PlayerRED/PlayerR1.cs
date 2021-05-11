using UnityEngine;

public class PlayerR1 : Player
{


    // Start is called before the first frame update
    public override void Awake()
    {
        base.Awake();
        this.idTeam = 1;
        sectorAction = 1;
        posAtt = GameObject.Find("PosAttR1").transform.position;
        posDef = GameObject.Find("PosDefR1").transform.position;
        posMiddle = GameObject.Find("PosBattutaR1").transform.position;
        posStart = GameObject.Find("PosStartR1").transform.position;
        posGoal = GameObject.Find("GolLineYellow").transform.position;
        opponent = GameObject.Find("PlayerY5").GetComponent<Player>();
        posCounter = GameObject.Find("PosCounterAttR1").transform.position;
        posBallEndAction = GameObject.Find("DownSx").transform.position;

        armDx = false;
        cpuFlag = true;
    }
    /* public override bool PlayerCpu()
     {
         bool hasChoose = false;
         Vector3 destBall = Vector3.zero;
         int idZone = IA.current.ZoneBall(this);
         Player pR5 = GameObject.Find("PlayerR5").GetComponent<Player>();
         Player pR3 = GameObject.Find("PlayerR3").GetComponent<Player>();
         Player pR6 = GameObject.Find("PlayerR6").GetComponent<Player>();
         bool shoot = IA.current.DecisionShoot(this);
         //  Debug.Log(name + " decisione Tiro?: " + shoot);

         if (shoot) //Tiro in porta
         {


             if (GameCore.current.levelCPUHard)
             {
                 destBall = IA.current.ShootHardR1();
             }
             else
             {
                 // destBall = IA.current.ShootNormalR1();
             }
             idDecisionCPU = 0;
             Utility.RotateObjToPoint(this.gameObject, destBall);

             this.LoadShoot(destBall, shoot, 0);

             return true;
         }
         /*********PASSAGGIO********************/
    /*   else
       {

           switch (idZone)
           {
               case 0:

                   if ((pR5.arrivedFlagCounterAtt || pR5.counterAttFlag) && !pR5.stun)
                   {
                       destBall = GameObject.Find("PosCounterAttR5").transform.position;
                       Utility.RotateObjToPoint(this.gameObject, destBall);
                       this.LoadShoot(destBall, false, 0);
                       idDecisionCPU = 1;
                       return true;
                   }
                   else if (pR5.arrivedFlagAtt && pR3.arrivedFlagAtt)
                   {
                       if (Random.value > 0.5)
                       {
                           destBall = pR5.transform.position;
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
                   else if (pR5.arrivedFlagAtt)
                   {
                       destBall = pR5.transform.position;
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
                       if (pR6.marcaFlag && (IA.current.PingBoaIsFree(this.name)))
                       {
                           destBall = GameObject.Find("SensorBoaR6").transform.position;
                           Utility.RotateObjToPoint(this.gameObject, destBall);

                           this.LoadShoot(destBall, false, 0);
                           idDecisionCPU = 7;
                           return true;
                       }
                       else if (pR5.arrivedFlagAtt)
                       {
                           destBall = pR5.transform.position;
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
                       if (pR5.arrivedFlagCounterAtt)
                       {
                           destBall = GameObject.Find("PosCounterAttR5").transform.position;
                           Utility.RotateObjToPoint(this.gameObject, destBall);
                           this.LoadShoot(destBall, false, 0);
                           idDecisionCPU = 12;
                           return true;
                       }
                       else if (pR6.marcaFlag && IA.current.PingBoaIsFree(this.name))
                       {
                           destBall = GameObject.Find("SensorBoaR6").transform.position;
                           Utility.RotateObjToPoint(this.gameObject, destBall);

                           this.LoadShoot(destBall, false, 0);
                           idDecisionCPU = 11;
                           return true;
                       }


                       else if (pR5.arrivedFlagAtt && pR3.arrivedFlagAtt)
                       {
                           if (Random.value > 0.5)
                           {
                               destBall = pR5.transform.position;
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
                       else if (pR5.arrivedFlagAtt)
                       {
                           destBall = pR5.transform.position;
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
       if (!swimKeep && !arrivedFlagAtt)
       {
           SetSwimKeep();
       }
       selected = false;
       return hasChoose;
   }*/
}
