public class PlayerY5 : Player
{
    /* public override void Awake()
     {
         base.Awake();
         this.idTeam = 0;
         sectorAction = 1;
         posAtt = GameObject.Find("PosAttY5").transform.position;
         posDef = GameObject.Find("PosDefY5").transform.position;
         posGoal = GameObject.Find("GolLineRed").transform.position;
         posStart = GameObject.Find("PosStartY5").transform.position;
         posMiddle = GameObject.Find("PosBattutaY5").transform.position;
         posCounter = GameObject.Find("PosCounterAttY5").transform.position;
         posBallEndAction = GameObject.Find("UpSx").transform.position;
         if (CheckOpponent("PlayerR1"))
             opponent = GameObject.Find("PlayerR1").GetComponent<Player>();
         armDx = true;
         cpuFlag = false;
     }

     private bool CheckOpponent(string v)
     {
         return true;
     }

     public override void Start()
     {
         base.Start();
     }

     /* public override void OnTriggerEnter2D(Collider2D collision)
      {
          base.OnTriggerEnter2D(collision);
          if (collision.CompareTag("Player"))
          {
              Player player = collision.GetComponent<Player>();
              Debug.Log(name + " CONTROLLO INIZIO FIGHT");
              if (player.idTeam == 1 && CanFight(player.name) && player.CanFight(name))
              {
                  if (prefabFight == null)
                  {
                      prefabFight = Instantiate(fightManager, new Vector3((transform.position.x + player.transform.position.x) / 2, (transform.position.y + player.transform.position.y) / 2, 0), Quaternion.identity);
                      prefabFight.CreateFight(this, player);
                  }
              }
          }
 }*/
}