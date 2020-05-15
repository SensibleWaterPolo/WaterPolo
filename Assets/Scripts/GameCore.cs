using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.UI;

public class GameCore : MonoBehaviour
{
    public static GameCore current;
    public bool isPlay = false;
    public float timeMatch; //Durata del match
    public float time = 0;
    public float timeStart;
    public float secAction;
    public float secCurrent;
    public float timeCurrentMatch;
    public float secStart;
    public GameObject finalMenu;
    public GameObject goalAnimation;
    public bool levelCPUHard; //true se livello CPU hard, false se normal
    public bool startSec;
    public bool secExpired;
    public bool stopShoot;
    private void Awake()
    {
        current = this;
        levelCPUHard = true;
        secAction = 5;
        finalMenu = GameObject.Find("FinalMatchPanel");
        goalAnimation = GameObject.Find("GoalPanel");
        timeMatch = 180;
        stopShoot = false;
    }
    void Start()
    {
        Application.targetFrameRate = 60;
        timeCurrentMatch = timeMatch;
        secCurrent = secAction;
        UpdateTimeGame();
        finalMenu.SetActive(false);
        goalAnimation.SetActive(false);
        Time.timeScale = 1;
        startSec = false;
        Invoke("Play", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlay)
        {    
            if (timeCurrentMatch > 1)
            {
                time = Time.time - timeStart;

                timeCurrentMatch = timeMatch - time;


                if (secCurrent < 1.5)
                {
                    stopShoot = true;

                }
                else 
                {
                    stopShoot = false;
                }
                if (secCurrent > 0.5 && startSec)
                {
                    UpdateSecond();
                }
                else if (startSec && secCurrent < 0.5)
                {

                    secExpired = true;

                    startSec = false;
                  /*  if (!Ball.current.isShooted)
                    {
                        Fischia();
                    }*/

                }

                UpdateTimeGame();
            }
            else
            {

                //Partita finita
                Stop();
                Time.timeScale = 0;

            }
        } else if (!isPlay && timeCurrentMatch <= 1) 
        {         
          
                ShowFinalPanel();
            
        }


    }

    public void Play()
    {
        isPlay = true;
        timeStart = Time.time;
        Referee.current.SetArmFront();
        AudioController.current.DoFischio();
        

    }
    public void Stop()
    {
        isPlay = false;
        Referee.current.SetArmFront();
        AudioController.current.DoFischio();
        PosPlayerMng.curret.SetAllBicy();
    }

    public int GetMin(float time)
    {
        return Mathf.FloorToInt (time / 60);
        
    }
    public int GetSec(float time) 
    {
        
    return Mathf.FloorToInt(time % 60);
    
    }

    public void RestartTimeAction() 
    {
        secCurrent = secAction;
        StartSecond();
       
    }

    public void UpdateTimeGame() 
    {
        int min = GetMin(timeCurrentMatch);
        int sec = GetSec(timeCurrentMatch);
        if (sec <10 )
        {
            GameObject.Find("Time").GetComponent<Text>().text = min + ":0"+sec; 
        }
        else
        {
            GameObject.Find("Time").GetComponent<Text>().text = min + ":" + sec;
        }
        GameObject.Find("Seconds").GetComponent<Text>().text = Mathf.FloorToInt(secCurrent) +"' ";
        
    }

    public void ShowFinalPanel()
    {
        finalMenu.SetActive(true);
        int golY = int.Parse( GameObject.Find("HomeScore").GetComponent<Text>().text);
        int golR = int.Parse(GameObject.Find("AwayScore").GetComponent<Text>().text);

        if (golY > golR)
        {

            GameObject.Find("TestoFinale").GetComponent<Text>().text = " Easy win vs under 15 ";

        }
        else if (golY < golR)
        {

            GameObject.Find("TestoFinale").GetComponent<Text>().text = " You Suck!!! ";

        }
        else 
        {
            GameObject.Find("TestoFinale").GetComponent<Text>().text = " Bored Match ";
        }

            
    }
    public void ShowGoalAnimation()
    {
        goalAnimation.SetActive(true);
 
    }
    public void DeleteGoalAnimation()
    {
        goalAnimation.SetActive(false);

    }
    public void UpdateSecond()
    {
        
       
        secCurrent = secAction - (Time.time - secStart);
        
    }

    public void StartSecond()
    {
        secStart= Time.time;
        startSec = true;
        secExpired = false;
       
    }

    public void ShootPlayer()
    {
        Player player = Ball.current.player;
        if (player != null)
        {
            if (player.keep && !player.loadShoot)
            {

                player.LoadShoot(player.posBallEndAction, false, 0);

            }
            else if (player.keepBoa && !player.loadShoot)
            {

                if (IA.current.BoaWatchGk(player.idTeam))

                {
                    player.LoadShoot(player.posBallEndAction, false, 3);
                }
                else
                {
                    player.LoadShoot(player.posBallEndAction, false, 1);

                }

            }
        }
        startSec = false;
    }
    public void ShootGk() 
    {
        if (Ball.current.gk != null)
        {
            GoalKeeper gk = Ball.current.gk;

            if (gk.keep)
            {
                gk.LoadShoot(gk.posBallEndAction);


            }
        }
            startSec = false;
        

    }
    public void Fischia()
    {
        AudioController.current.DoFischio();
      
        PosPlayerMng.curret.SetAllBicy();
         Ball.current.inGameFlag = false;
        if (Ball.current.idTeam == 1)
        {
            Referee.current.SetArmRight();
        }
        else
        {
            Referee.current.SetArmLeft();
        }

        Invoke("KeepToGk", 1);

    }

    public void KeepToGk() 
    {
        if (Ball.current.idTeam == 1)
        {
            GameObject.Find("YellowGK").GetComponent<GoalKeeper>().SetKeep();


        }
        else {

            GameObject.Find("RedGK").GetComponent<GoalKeeper>().SetKeep();
        }
        Ball.current.inGameFlag = true;
    
    }

}
