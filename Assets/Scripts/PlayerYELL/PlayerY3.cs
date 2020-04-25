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
        if (CheckOpponent("PlayerR6"))
            opponent = GameObject.Find("PlayerR6").GetComponent<Player>();
        boaFlag = true;
        armDx = true;
    }

    public override void OnTriggerStay2D(Collider2D collision)
    {
        base.OnTriggerStay2D(collision);
        if (collision.gameObject.name == opponent.name && opponent.arrivedFlagAtt && arrivedFlagDef && !marcaFlag && !keep && !opponent.keep)
        {
             marcaFlag = true;
            boaFlag = true;
            battlePrefab = Instantiate(battle, opponent.transform.position, Quaternion.Euler(new Vector3(0, 0, Utility.GetAngleBetweenObjAB(opponent.gameObject, Ball.current.gameObject))));
            battlePrefab.CreateBattle(opponent, this);
        }
    }
}
