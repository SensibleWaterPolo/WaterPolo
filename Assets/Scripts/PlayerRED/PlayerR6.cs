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
      //  posMiddle = GameObject.Find("PosBattutaR6").transform.position;
       // posStart = GameObject.Find("PosStartR6").transform.position;
        posGoal = GameObject.Find("GolLineYellow").transform.position;
        opponent = GameObject.Find("PlayerY3").GetComponent<Player>();
        boaFlag = true;
        armDx = true;
        cpuFlag = true;
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;

        if (obj.tag == "Ball" && !keep && !keepBoa && Ball.current.freeFlag && Ball.current.speed < 5f && !Ball.current.respawn && !marcaFlag)
        {
            SetKeep();
            SetBall();
        }
    }

}
