using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerY5 : Player
{
    public FightManager fight;
    public FightManager fightPrefab;
    public override void Awake()
    {
        base.Awake();
        this.idTeam = 0;
        sectorAction = 1;
        posAtt = GameObject.Find("PosAttY5").transform.position;
        posDef= GameObject.Find("PosDefY5").transform.position;
        posGoal = GameObject.Find("GolLineRed").transform.position;
       posStart = GameObject.Find("PosStartY5").transform.position;
        posMiddle = GameObject.Find("PosBattutaY5").transform.position;

        if (CheckOpponent("PlayerR1"))
            opponent = GameObject.Find("PlayerR1").GetComponent<Player>();
        armDx = true;
        cpuFlag = false;
    }

    public override void Start()
    {
        base.Start();
        
    }

  

}
