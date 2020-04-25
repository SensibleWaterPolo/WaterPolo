using System.Collections;
using System.Collections.Generic;
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
        
        if(CheckOpponent("PlayerR3"))
        opponent = GameObject.Find("PlayerR3").GetComponent<Player>();
        boaFlag = true;
        armDx = true;
    }



}