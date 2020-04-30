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
      //  posMiddle = GameObject.Find("PosBattutaR3").transform.position;
       // posStart = GameObject.Find("PosStartR3").transform.position;
        posGoal = GameObject.Find("GolLineYellow").transform.position;
        opponent = GameObject.Find("PlayerY6").GetComponent<Player>();
         armDx = true;
        cpuFlag = true;
    }
    public override void OnTriggerStay2D(Collider2D collision)
    {
        base.OnTriggerStay2D(collision);
        if (collision.gameObject.name == opponent.name && opponent.arrivedFlagAtt && arrivedFlagDef && !marcaFlag && !keep && !opponent.keep && idBall==3)
        {            
            marcaFlag = true;
            opponent.marcaFlag = true;
            battlePrefab = Instantiate(battle, opponent.transform.position, Quaternion.Euler(new Vector3(0, 0, Utility.GetAngleBetweenObjAB(opponent.gameObject, Ball.current.gameObject))));
            battlePrefab.CreateBattle(opponent, this);
        }
        if (collision.gameObject.name == "Ball" && !keep && Ball.current.CheckBallIsPlayable(5) && !opponent.keepBoa && !loadShoot)
        {
            
            SetKeep();
            SetBall();
        }
        
    }
 

}
