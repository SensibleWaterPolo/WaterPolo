using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GkRed : GoalKeeper
{
    // Start is called before the first frame update

    public override void Awake()
    {
        base.Awake();
        posMid = GameObject.Find("PosMidRed").transform.position;
        posLeft = GameObject.Find("PosLeftRed").transform.position;
        posRight = GameObject.Find("PosRightRed").transform.position;
        posThrowIn = GameObject.Find("PosThrowInRed").transform.position;
        transform.position = posMid;
        DisableSave();
        idTeam = 1;
        cpuFlag = true;
    }
   
    public override void SaveUP(bool save)
    {
        base.SaveUP(save);
    }
    public override void SaveLeft(bool save)
    {
        base.SaveLeft(save);
        rb.MovePosition(new Vector3(transform.position.x + agility, transform.position.y, transform.position.z));
        Invoke("DisableSave", 1f);
    }

    public override void SaveRight(bool save)
    {
        base.SaveRight(save);
        rb.MovePosition(new Vector3(transform.position.x - agility, transform.position.y, transform.position.z));
        Invoke("DisableSave", 1f);
    }

    public override void SaveMid(bool save)
    {
        base.SaveMid(save);
    }



    public override void UpdateFinalPos()
    {
        UpdateDist_PosBall();
        if (transform.position != finalPos)
            arrived = false;
        if (!Ball.current.fieldYellow && Ball.current.idTeam!=idTeam && Ball.current.idTeam > -1)
        {
            if (-8 <= posXBall && posXBall <= 8)
                finalPos = posMid;
            if (posXBall < -8)
                finalPos = posRight;
            if (posXBall > 8)
                finalPos = posLeft;
        }
        else finalPos = posMid;
    }

    public override bool GkCpu()
    {
        bool hasChoose = false;
        Vector3 destBall = Vector3.zero;
        Player pR5 = GameObject.Find("PlayerR5").GetComponent<Player>();
        Player pr1 = GameObject.Find("PlayerR1").GetComponent<Player>();
        Player pR3 = GameObject.Find("PlayerR3").GetComponent<Player>();
       
          /*   if (pr1.counterAttFlag && pR5.counterAttFlag)
             {
                       if (Random.value > 0.5)
                        {
                            destBall = GameObject.Find("PosCounterAttR5").transform.position;
                            Utility.RotateObjToPoint(this.gameObject, destBall);
                            this.LoadShoot(destBall);
                            return true;
                        }
                        else
                        {
                            destBall = GameObject.Find("PosCounterAttR1").transform.position;
                            Utility.RotateObjToPoint(this.gameObject, destBall);
                            this.LoadShoot(destBall);
                            return true;
                        }
             }

                    else if (pR5.counterAttFlag)
                    {
                        destBall = GameObject.Find("PosCounterAttR5").transform.position;
                        Utility.RotateObjToPoint(this.gameObject, destBall);
                        this.LoadShoot(destBall);
                        return true;
                    }
                    else if (pr1.counterAttFlag)
                    {
                        destBall = GameObject.Find("PosCounterAttR1").transform.position;
                        Utility.RotateObjToPoint(this.gameObject, destBall);
                        this.LoadShoot(destBall);
                        return true;
                    }
                        else*/ if (pR3.arrivedFlagAtt)
                        {
                        destBall = pR3.transform.position;
                        Utility.RotateObjToPoint(this.gameObject, destBall);
                        this.LoadShoot(destBall);
                         }
                         else if (pR5.arrivedFlagAtt && pr1.arrivedFlagAtt)
                         {
                          if (Random.value > 0.5)
                        {
                            destBall = pR5.transform.position;
                            Utility.RotateObjToPoint(this.gameObject, destBall);
                            this.LoadShoot(destBall);
                            return true;

                        }
                        else
                        {
                            destBall = pr1.transform.position;
                            Utility.RotateObjToPoint(this.gameObject, destBall);
                            this.LoadShoot(destBall);
                            return true;
                        }
                    }
                    
                         else if (pR5.arrivedFlagAtt)
                    {
                        destBall = pR5.transform.position;
                        Utility.RotateObjToPoint(this.gameObject, destBall);
                        this.LoadShoot(destBall);
                        return true;
                    }
                    else if (pr1.arrivedFlagAtt)
                    {
                        destBall = pr1.transform.position;
                        Utility.RotateObjToPoint(this.gameObject, destBall);
                        this.LoadShoot(destBall);
                        return true;
                    }
                 
        
        return hasChoose;
    }
}
