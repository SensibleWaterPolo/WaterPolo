using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerY1 : Player
{
    public FightManager fight;
    public FightManager fightPrefab;
    public override void Awake()
    {
        base.Awake();
        this.idTeam = 0;
        sectorAction = 3;
        posAtt = GameObject.Find("PosAttY1").transform.position;
        posDef = GameObject.Find("PosDefY1").transform.position;
        posGoal = GameObject.Find("GolLineRed").transform.position;
        posMiddle = GameObject.Find("PosBattutaY1").transform.position;
        if (CheckOpponent("PlayerR5"))
            opponent = GameObject.Find("PlayerR5").GetComponent<Player>();
        armDx = false;
        cpuFlag = false;
    }
 

}
