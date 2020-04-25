using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour
{
    private float lenghtFight = 4f;
    private Player playerY, playerR;
    public int numClickP1;
    public int numClickP2;
    private int idWhoWin; //0:win Player1, 1: win Player2
    private bool solve;


    
    
    // Start is called before the first frame update
    void Start()
    {
        
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
            //vince il Y, playerR viene messo nello stato di affogato
        }
        else 
        { 
         // vince R, player Y viene messo nello stato di affofato
        }
        Destroy(this.gameObject);    
    }
    public void SolveFightVsP2()
    {
        solve = true;
        int id = WhoWinVSP2();

        if (id == 0)
        {
            //Vince Y
        }
        else
        {
            //Vince R
        }
        Destroy(this.gameObject);
    }


}
