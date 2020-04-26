using System.Collections;
using System.Collections.Generic;
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
        lenghtFight = 4f;
        start = false;
    }


    // Start is called before the first frame update
    void Start()
    {
        
        numClickP1 = 0;
        numClickP2 = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int WhoWinVSCPU()
    {
        int numClickCpu =(int) Random.Range(playerR.clickCPU.x,playerR.clickCPU.y);

        if (numClickP1 + playerY.stamina> playerR.stamina + numClickCpu)
        {
            return 0;

        }
        else
         if (numClickP1 +playerY.stamina < playerR.stamina + numClickCpu)
        {
            return 1;

        }
        else
        if (numClickP1 == playerR.stamina)
        {
            int id = Random.Range(0, 1);
            return id;
        }
        return -1;
    }

    public int WhoWinVSP2() 
    {
        if (numClickP1+playerY.stamina > numClickP2+playerR.stamina)
        {
            return 0;

        }
        else
            if (numClickP1+playerY.stamina < numClickP2+playerR.stamina)
        {
            return 1;

        }
        else
           if (numClickP1+playerY.stamina == numClickP2+playerR.stamina)
        {
            int id = Random.Range(0, 1);
            return id;
        }
        return -1;
    }

    public void SolveFightVsCPU() 
    {
        solve = true;
        int id = WhoWinVSCPU();

        if (id == 0)
        {
            playerR.SetStun();
            playerY.SetFightFlag(false);
            playerR.SetFightFlag(false);

        }
        else 
        {
            playerY.SetStun();
            playerY.SetFightFlag(false);
            playerR.SetFightFlag(false);
        }
        Destroy(this.gameObject);    
    }
    public void SolveFightVsP2()
    {
        solve = true;
        int id = WhoWinVSP2();
        
        if (id == 0)
        {
            playerR.SetStun();
            playerY.SetFightFlag(false);
            playerR.SetFightFlag(false);
        }
        else
        {
            playerY.SetStun();
            playerY.SetFightFlag(false);
            playerR.SetFightFlag(false);
        }
        Destroy(this.gameObject);
    }

    public void P1AddClickLocal() 
    {
        numClickP1++;
        Debug.Log("Click: "+numClickP1);
    }
    public void P2AddClickLocal()
    {
        numClickP2++;
    }

    public void CreateFight(Player pY, Player pR)
    {   playerY=pY;
        playerR = pR;
        playerY.SetFightFlag(true);
        playerR.SetFightFlag(true);
        playerR.SetBicy();
        playerY.SetBicy();
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

    }
        


}
