using UnityEngine;
using UnityEngine.UI;

public class PosPlayerMng : MonoBehaviour
{
    public Player[] allPlayer = new Player[8];

    public GameObject panelStat;
    private int counterCLick;
    private int nClickRep;

    public GoalKeeper gKRed;
    public GoalKeeper gkYelloW;
    public static PosPlayerMng curret;
    private bool showStat;
    private bool showRep;

    // Start is called before the first frame update
    private void Start()
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

        for (int i = 0; i < 8; i++)
        {
            if (allPlayer[i].distaceBall < distance)
            {
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
        for (int i = 0; i < allPlayer.Length; i++)
        {
            // allPlayer[i].SetBicy();
            allPlayer[i].transform.position = allPlayer[i].posMiddle;
        }

        if (id == 0)
        { Ball.current.SetYellowSideBall(); }
        else
        {
            Ball.current.SetRedSideBall();
        }

        Invoke("Batti", 1.5f);
    }

    public void SetAllBicy()
    {
        /* for (int i = 0; i < allPlayer.Length; i++)
         {
             if (!allPlayer[i].keep && !allPlayer[i].keepBoa)
             {
                 allPlayer[i].SetBicy();
             }
         }*/
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
        if (counterCLick % 2 == 1 && !showRep)
        {
            showStat = true;
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
            GoalKeeper gky = GameObject.Find("YellowGK").GetComponent<GoalKeeper>();

            GameObject.Find("StatGKR").GetComponent<Text>().text = " VEL: " + gkr.vel + " PARATA: " + gkr.block + " RILANCIO: " + gkr.throwin;
            GameObject.Find("StatGKY").GetComponent<Text>().text = " VEL: " + gky.vel + " PARATA: " + gky.block + " RILANCIO: " + gky.throwin;
        }
        else
        {
            HideStatPanel();
            showStat = false;
            showRep = false;
            Time.timeScale = 1;
        }
    }

    public void HideStatPanel()
    {
        panelStat.SetActive(false);
    }

    public void ShowReportPanel()
    {
        nClickRep++;
        if (nClickRep % 2 == 1 && !showStat)
        {
            showRep = true;
            panelStat.SetActive(true);
            Time.timeScale = 0;
            /*  GameObject.Find("StatY1").GetComponent<Text>().text = "idDec:" + allPlayer[0].idDecisionMaking + "/idBall:" + allPlayer[0].idBall + "/Ball:" + allPlayer[0].ballFlag;
              GameObject.Find("StatY3").GetComponent<Text>().text = "idDec:" + allPlayer[1].idDecisionMaking + "/idBall:" + allPlayer[1].idBall + "/Ball:" + allPlayer[1].ballFlag;
              GameObject.Find("StatY5").GetComponent<Text>().text = "idDec:" + allPlayer[2].idDecisionMaking + "/idBall:" + allPlayer[2].idBall + "/Ball:" + allPlayer[2].ballFlag;
              GameObject.Find("StatY6").GetComponent<Text>().text = "idDec:" + allPlayer[3].idDecisionMaking + "/idBall:" + allPlayer[3].idBall + "/Ball:" + allPlayer[3].ballFlag;
              GameObject.Find("StatR1").GetComponent<Text>().text = "idDec:" + allPlayer[4].idDecisionMaking + "/idCPU" + allPlayer[4].idDecisionCPU + "/idBall:" + allPlayer[4].idBall + "/Ball:" + allPlayer[4].ballFlag;
              GameObject.Find("StatR3").GetComponent<Text>().text = "idDec:" + allPlayer[5].idDecisionMaking + "/idCPU" + allPlayer[5].idDecisionCPU + "/idBall:" + allPlayer[5].idBall + "/Ball:" + allPlayer[5].ballFlag;
              GameObject.Find("StatR5").GetComponent<Text>().text = "idDec:" + allPlayer[6].idDecisionMaking + "/idCPU" + allPlayer[6].idDecisionCPU + "/idBall:" + allPlayer[6].idBall + "/Ball:" + allPlayer[6].ballFlag;
              GameObject.Find("StatR6").GetComponent<Text>().text = "idDec:" + allPlayer[7].idDecisionMaking + "/idCPU" + allPlayer[7].idDecisionCPU + "/idBall:" + allPlayer[7].idBall + "/Ball:" + allPlayer[7].ballFlag;
              GameObject.Find("StatGKR").GetComponent<Text>().text = " ";
              GameObject.Find("StatGKY").GetComponent<Text>().text = " ";
              GameObject.Find("StatBall").GetComponent<Text>().text = "vel:" + Ball.current.speed + "/inGame:" + Ball.current.inGameFlag + "/shoot:" + Ball.current.isShooted + "/free:" + Ball.current.freeFlag + "/near:" + Ball.current.moreNear;*/
        }
        else
        {
            showRep = false;
            showStat = false;
            HideReportPanel();
            Time.timeScale = 1;
        }
    }

    public void HideReportPanel()
    {
        panelStat.SetActive(false);
    }
}