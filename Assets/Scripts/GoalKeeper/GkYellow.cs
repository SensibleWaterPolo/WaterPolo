﻿using UnityEngine;

public class GkYellow : GoalKeeper
{
    // Start is called before the first frame update

    public override void Awake()
    {
        base.Awake();
        posMid = GameObject.Find("PosMidYellow").transform.position;
        posLeft = GameObject.Find("PosLeftYellow").transform.position;
        posRight = GameObject.Find("PosRightYellow").transform.position;
        posThrowIn = GameObject.Find("PosThrowInYellow").transform.position;
        posBallEndAction = GameObject.Find("UppDx").transform.position;
        transform.position = posMid;
        // DisableSave();
        idTeam = 0;
        cpuFlag = false;
    }

    /*  private void Update()
      {
      }

      public override void SaveUP(bool save)
      {
          base.SaveUP(save);
      }

      public override void SaveLeft(bool save)
      {
          base.SaveLeft(save);
          rb.MovePosition(new Vector3(transform.position.x - agility, transform.position.y, transform.position.z));
          Invoke("DisableSave", 1f);
      }

      public override void SaveRight(bool save)
      {
          base.SaveRight(save);
          rb.MovePosition(new Vector3(transform.position.x + agility, transform.position.y, transform.position.z));
          Invoke("DisableSave", 1f);
      }

      public override void UpdateFinalPos()
      {
          UpdateDist_PosBall();
          if (transform.position != finalPos)
              arrived = false;
          if (Ball.current.fieldYellow && Ball.current.idTeam != idTeam && Ball.current.idTeam > -1)
          {
              if (-8 <= posXBall && posXBall <= 8)
                  finalPos = posMid;
              if (posXBall < -8)
                  finalPos = posLeft;
              if (posXBall > 8)
                  finalPos = posRight;
          }
          else finalPos = posMid;
      }*/
}