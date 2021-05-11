using UnityEngine;

public class PlayerY6 : Player
{
    // Start is called before the first frame update
    public override void Awake()
    {
        base.Awake();
        this.idTeam = 0;
        sectorAction = 2;
        posAtt = GameObject.Find("PosAttY6").transform.position;
        posDef = GameObject.Find("PosDefY6").transform.position;
        posGoal = GameObject.Find("GolLineRed").transform.position;
        posMiddle = GameObject.Find("PosBattutaY6").transform.position;
        posStart = GameObject.Find("PosStartY6").transform.position;
        posCounter = GameObject.Find("PosCounterAttY6").transform.position;
        posBallEndAction = GameObject.Find("UpSx").transform.position;
        /*  if (CheckOpponent("PlayerR3"))
              opponent = GameObject.Find("PlayerR3").GetComponent<Player>();*/
        boaFlag = true;
        armDx = true;
        cpuFlag = false;
    }

    /* public override void FixedUpdate()
     {
         base.FixedUpdate();
     }*/

    /*  public override void OnTriggerEnter2D(Collider2D collision)
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
      }*/

    /*   public override void OnTriggerStay2D(Collider2D collision)
       {
           base.OnTriggerStay2D(collision);
       }*/
}