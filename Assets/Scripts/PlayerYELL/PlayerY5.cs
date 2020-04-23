using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerY5 : Player
{
    public override void Awake()
    {
        base.Awake();
        this.idTeam = 0;
        sectorAction = 1;
        posAtt = GameObject.Find("PosAttY5").transform.position;
        posDef= GameObject.Find("PosDefY5").transform.position;
        posGoal = GameObject.Find("GolLineRed").transform.position;
        if (CheckOpponent("PlayerR1"))
            opponent = GameObject.Find("PlayerR1").GetComponent<Player>();
    }

    

}
