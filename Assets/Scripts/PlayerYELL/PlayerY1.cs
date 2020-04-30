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
        if (CheckOpponent("PlayerR5"))
            opponent = GameObject.Find("PlayerR5").GetComponent<Player>();
        armDx = false;
    }
   /* public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (collision.name == opponent.name && opponent.swim && swim)  //Il fight avviene solo se i due giocatori stanno nuotando
        {
            Debug.Log("Collisione fight");
            if (fightPrefab == null)
            {
                fightPrefab = Instantiate(fight, new Vector3((transform.position.x + opponent.transform.position.x) / 2, (transform.position.y + opponent.transform.position.y) / 2, 0), Quaternion.identity);
                fightPrefab.CreateFight(this, opponent);
            }
        }
    }
    public override void Start()
    {
        base.Start();
    }*/

}
