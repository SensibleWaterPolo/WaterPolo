using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PosPlayerMng : MonoBehaviour
{
    public Player[] allPlayer = new Player[8];

    public GameObject panelStat;
    private int counterCLick;

    public GoalKeeper gKRed;
    public GoalKeeper gkYelloW;
    public static  PosPlayerMng curret;
   
    
    // Start is called before the first frame update
    void Start()
    {
        HideStatPanel();
        curret = this;
        allPlayer[0] = GameObject.Find("PlayerY1").GetComponent<Player>();
        allPlayer[1] = GameObject.Find("PlayerY3").GetComponent<Player>();
        allPlayer[2] = GameObject.Find("PlayerY5").GetComponent<Player>();
        allPlayer[3] = GameObject.Find("PlayerY6").GetComponent<Player>();
        allPlayer[4] = GameObject.Find("PlayerR1").GetComponent<Player>();
        allPlayer[5] = GameObject.Find("PlayerR3").GetComponent<Player>();
        allPlayer[6] = GameObject.Find("PlayerR5").GetComponent<Player>();
        allPlayer[7] = GameObject.Find("PlayerR6").GetComponent<Player>();
        
    }

    public string GetPlayerNameNearBall() 
    {
        float distance = 100;
        string name = "";

        for(int i=0; i<8;i++) {
            if (allPlayer[i].distaceBall < distance) {
                distance = allPlayer[i].distaceBall;
                name = allPlayer[i].name;
            }
        }
        return name;
    }

    public string GetPlayerForTeamNearBall(int idTeam, bool boaFlag) 
    {
        float distance = 100;
        string name = "";

        if (idTeam == 0)
        {
            for (int i = 0; i < 4; i++)
            {
                if (allPlayer[i].distaceBall < distance)
                {
                    if ((boaFlag && !allPlayer[i].marcaFlag) || !boaFlag)
                    {
                        distance = allPlayer[i].distaceBall;
                        name = allPlayer[i].name;
                    }
                }
            }
        }
        else
        {
            for (int i = 4; i < 8; i++)
            {
                if (allPlayer[i].distaceBall < distance)
                {
                    if ((boaFlag && !allPlayer[i].marcaFlag || !boaFlag))
                    {
                        distance = allPlayer[i].distaceBall;
                        name = allPlayer[i].name;
                    }
                    
                }
            }
        }
        return name;

    }

    public void SetKickOff(int id) //0: palla a Yellow, 1: palla Red
    {
        for(int i=0; i < allPlayer.Length; i++)
        {
            allPlayer[i].SetBicy();
            allPlayer[i].transform.position = allPlayer[i].posMiddle;
        }

        if (id == 0)
        { Ball.current.SetYellowSideBall(); }
        else {
            Ball.current.SetRedSideBall();
        }

        Invoke("Batti",1.5f);
        
    }

    public void SetAllBicy() 
    {

        for (int i = 0; i < allPlayer.Length; i++)
        {
            if (!allPlayer[i].keep && !allPlayer[i].keepBoa)
            {
                allPlayer[i].SetBicy();
            }
            
        }
    }
    public void Batti()
    {
        AudioController.current.DoFischio();
        Referee.current.SetArmFront();
        Ball.current.inGameFlag = true;
    }

    public void ShowStatPanel() 
    {
        counterCLick++;
        if (counterCLick % 2 == 1)
        {
            panelStat.SetActive(true);
            Time.timeScale = 0;
            GameObject.Find("StatY1").GetComponent<Text>().text = " VEL: " + allPlayer[0].speed + " FOR: " + allPlayer[0].stamina + " TIR: " + allPlayer[0].shoot + "  PASS: " + allPlayer[0].pass;
            GameObject.Find("StatY3").GetComponent<Text>().text = " VEL: " + allPlayer[1].speed + " FOR: " + allPlayer[1].stamina + " TIR: " + allPlayer[1].shoot + " PASS: " + allPlayer[1].pass;
            GameObject.Find("StatY5").GetComponent<Text>().text = " VEL: " + allPlayer[2].speed + " FOR: " + allPlayer[2].stamina + " TIR: " + allPlayer[2].shoot + " PASS: " + allPlayer[2].pass;
            GameObject.Find("StatY6").GetComponent<Text>().text = " VEL: " + allPlayer[3].speed + " FOR: " + allPlayer[3].stamina + " TIR: " + allPlayer[3].shoot + " PASS: " + allPlayer[3].pass;
            GameObject.Find("StatR1").GetComponent<Text>().text = " VEL: " + allPlayer[4].speed + " FOR: " + allPlayer[4].stamina + " TIR: " + allPlayer[4].shoot + " PASS: " + allPlayer[4].pass;
            GameObject.Find("StatR3").GetComponent<Text>().text = " VEL: " + allPlayer[5].speed + " FOR: " + allPlayer[5].stamina + " TIR: " + allPlayer[5].shoot + " PASS: " + allPlayer[5].pass;
            GameObject.Find("StatR5").GetComponent<Text>().text = " VEL: " + allPlayer[6].speed + " FOR: " + allPlayer[6].stamina + " TIR: " + allPlayer[6].shoot + " PASS: " + allPlayer[6].pass;
            GameObject.Find("StatR6").GetComponent<Text>().text = " VEL: " + allPlayer[7].speed + " FOR: " + allPlayer[7].stamina + " TIR: " + allPlayer[7].shoot + " PASS: " + allPlayer[7].pass;
            GoalKeeper gkr = GameObject.Find("RedGK").GetComponent<GoalKeeper>();
            GoalKeeper gky= GameObject.Find("YellowGK").GetComponent<GoalKeeper>();

            GameObject.Find("StatGKR").GetComponent<Text>().text = " VEL: " + gkr.vel + " PARATA: " + gkr.block + " RILANCIO: "+gkr.throwin;
            GameObject.Find("StatGKY").GetComponent<Text>().text = " VEL: " + gky.vel + " PARATA: " + gky.block + " RILANCIO: " + gky.throwin;
        }
        else { 
            
            HideStatPanel(); 
            Time.timeScale=1;
        }
      
    }


    public void HideStatPanel() 
    {
        panelStat.SetActive(false);
    }

}
