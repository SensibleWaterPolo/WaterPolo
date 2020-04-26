using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerR5 : Player
{
    // Start is called before the first frame update
    public override void Awake()
    {
        base.Awake();
        this.idTeam = 1;
        sectorAction = 3;
        posAtt = GameObject.Find("PosAttR5").transform.position;
        posDef = GameObject.Find("PosDefR5").transform.position;
      //  posMiddle = GameObject.Find("PosBattutaR5").transform.position;
       // posStart = GameObject.Find("PosStartR5").transform.position;
        posGoal = GameObject.Find("GolLineYellow").transform.position;
        opponent = GameObject.Find("PlayerY1").GetComponent<Player>();
        armDx = true;
        cpuFlag = true;
    }
}
