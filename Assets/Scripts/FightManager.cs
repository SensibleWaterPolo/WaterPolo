using UnityEngine;

public class FightManager : MonoBehaviour
{
    private float lenghtFight;
    private float startFight;
    private bool start;
    private Player playerY, playerR;
    public int numClickP1;
    public int numClickP2;
    private int idWhoWin; //0:win Player1, 1: win Player2
    private bool solve;

    private void Awake()
    {
        lenghtFight = 3f;
        start = false;
    }

    // Start is called before the first frame update
    private void Start()
    {
        numClickP1 = 0;
        numClickP2 = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        if (start)
        {
            //    playerY.SetBicy();
            //    playerR.SetBicy();
        }
    }

    /*  public int WhoWinVSCPU()
      {
          int numClickCpu = (int)Random.Range(playerR.clickCPU.x, playerR.clickCPU.y);

          if (numClickP1 + playerY.stamina > playerR.stamina + numClickCpu)
          {
              Debug.Log("Click cpu:" + numClickCpu + "p1: " + (numClickP1 + playerY.stamina) + " p2: " + (playerR.stamina + numClickCpu));
              return 0;
          }
          else
           if (numClickP1 + playerY.stamina < playerR.stamina + numClickCpu)
          {
              Debug.Log("Click cpu:" + numClickCpu + "p1: " + (numClickP1 + playerY.stamina) + " p2: " + (playerR.stamina + numClickCpu));
              return 1;
          }
          else
          if (numClickP1 == playerR.stamina)
          {
              int id = Random.Range(0, 2);
              Debug.Log("Click cpu:" + numClickCpu + "p1: " + (numClickP1 + playerY.stamina) + " p2: " + (playerR.stamina + numClickCpu));
              return id;
          }
          return -1;
      }

      public int WhoWinVSP2()
      {
          if (numClickP1 + playerY.stamina > numClickP2 + playerR.stamina)
          {
              return 0;
          }
          else
              if (numClickP1 + playerY.stamina < numClickP2 + playerR.stamina)
          {
              return 1;
          }
          else
             if (numClickP1 + playerY.stamina == numClickP2 + playerR.stamina)
          {
              int id = Random.Range(0, 1);
              return id;
          }
          return -1;
      }

      public void SolveFightVsCPU()
      {
          solve = true;
          start = false;
          int id = WhoWinVSCPU();

          if (id == 0)
          {
              playerR.SetStun();
              Debug.Log(playerY.name + " ->WIN " + playerR + "->LOSE");
          }
          else
          {
              Debug.Log(playerR.name + " ->WIN " + playerY + "->LOSE");
              playerY.SetStun();
          }
          playerY.SetFightFlag(false);
          playerR.SetFightFlag(false);
          playerY.ShowPlayer();
          playerR.ShowPlayer();
          Destroy(this.gameObject);
      }

      public void SolveFightVsP2()
      {
          solve = true;
          int id = WhoWinVSP2();

          if (id == 0)
          {
              playerR.SetStun();
          }
          else
          {
              playerY.SetStun();
          }

          Destroy(this.gameObject);
      }

      public void P1AddClickLocal()
      {
          numClickP1++;
          Debug.Log("Click: " + numClickP1);
      }

      public void P2AddClickLocal()
      {
          numClickP2++;
      }

      public void CreateFight(Player pY, Player pR)
      {
          playerY = pY;
          playerR = pR;
          pY.HidePlayer();
          pR.HidePlayer();

          playerY.SetFightFlag(true);
          playerR.SetFightFlag(true);
          //  playerR.SetBicy();
          //  playerY.SetBicy();

          playerR.transform.position = transform.position;
          playerY.transform.position = transform.position;
          start = true;
          if (playerR.cpuFlag)
          {
              Invoke("SolveFightVsCPU", lenghtFight);
          }
          else
          {
              Invoke("SolveFightVsP2", lenghtFight);
          }
      }*/
}