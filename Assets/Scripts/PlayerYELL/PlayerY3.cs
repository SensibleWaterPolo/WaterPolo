using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerY3 : Player
{
    public Battle battle;
    public Battle battlePrefab;
    
    // Start is called before the first frame update
    public override void Awake()
    {
        base.Awake();
        this.idTeam = 0;
        sectorAction = 2;
        posAtt = GameObject.Find("PosAttY3").transform.position;
        posDef = GameObject.Find("PosDefY3").transform.position;
        posGoal = GameObject.Find("GolLineRed").transform.position;
        posMiddle = GameObject.Find("PosBattutaY3").transform.position;
        posStart = GameObject.Find("PosStartY3").transform.position;
        posCounter = Vector3.zero;
        posBallEndAction = GameObject.Find("UppDx").transform.position;
        if (CheckOpponent("PlayerR6"))
            opponent = GameObject.Find("PlayerR6").GetComponent<Player>();
         armDx = false;
        cpuFlag = false;
    }

    public override void OnTriggerStay2D(Collider2D collision)
    {
        base.OnTriggerStay2D(collision);
        if (collision.gameObject.name == opponent.name && opponent.arrivedFlagAtt && arrivedFlagDef && !marcaFlag && !keep && !opponent.keep && !opponent.swimKeep && idBall==3)
        {
           
            battlePrefab = Instantiate(battle, opponent.transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
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

        if (collision.CompareTag("Ball") && !keep && !swimKeep && !loadShoot && Ball.current.CheckBallIsPlayable(60) && !Ball.current.shootFlag && marcaFlag)
        {
            if (Ball.current.idTeam != idTeam)
            {

                GameCore.current.RestartTimeAction();
            }
            Ball.current.freeFlag = false;
            SetKeep();

        }
        
    }
}
