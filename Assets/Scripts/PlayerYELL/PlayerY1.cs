public class PlayerY1 : Player
{
    /*   public override void Awake()
       {
           base.Awake();
           this.idTeam = 0;
           sectorAction = 3;
           posAtt = GameObject.Find("PosAttY1").transform.position;
           posDef = GameObject.Find("PosDefY1").transform.position;
           posGoal = GameObject.Find("GolLineRed").transform.position;
           posMiddle = GameObject.Find("PosBattutaY1").transform.position;
           posStart = GameObject.Find("PosStartY1").transform.position;
           posCounter = GameObject.Find("PosCounterAttY1").transform.position;
           posBallEndAction = GameObject.Find("UppDx").transform.position;
           if (CheckOpponent("PlayerR5"))
               opponent = GameObject.Find("PlayerR5").GetComponent<Player>();
           armDx = false;
           cpuFlag = false;
       }

       private bool CheckOpponent(string v)
       {
           return true;
       }

       /*  public override void OnTriggerEnter2D(Collider2D collision)
         {
             base(collision);

             if (collision.CompareTag("Player"))
             {
                 Player player = collision.GetComponent<Player>();

                 if (player.idTeam == 1)
                 {
                     bool canFight = CanFight(player.name);
                     bool enemyCanFight = player.CanFight(name);

                     if (prefabFight == null && canFight && enemyCanFight)
                     {
                         prefabFight = Instantiate(fightManager, new Vector3((transform.position.x + player.transform.position.x) / 2, (transform.position.y + player.transform.position.y) / 2, 0), Quaternion.identity);
                         prefabFight.CreateFight(this, player);
                     }
                 }
             }
         }*/
}