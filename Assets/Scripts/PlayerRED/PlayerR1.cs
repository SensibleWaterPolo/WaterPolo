using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerR1 : Player
{   
    
    
    // Start is called before the first frame update
    public override void Awake()
    {
        base.Awake();
        this.idTeam = 1;
        sectorAction = 1;
        posAtt = GameObject.Find("PosAttR1").transform.position;
        posDef = GameObject.Find("PosDefR1").transform.position;
      //  posMiddle = GameObject.Find("PosBattutaR1").transform.position;
        //posStart = GameObject.Find("PosStartR1").transform.position;
        posGoal = GameObject.Find("GolLineYellow").transform.position;
        opponent = GameObject.Find("PlayerY5").GetComponent<Player>();
        armDx = false;
        cpuFlag = true;
    }
    
}
