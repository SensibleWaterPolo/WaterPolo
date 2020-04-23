using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosPlayerMng : MonoBehaviour
{
    public Player[] allPlayer = new Player[8];
    public static  PosPlayerMng curret;
   
    
    // Start is called before the first frame update
    void Start()
    {
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

    public string GetPlayerForTeamNearBall(int idTeam) 
    {
        float distance = 100;
        string name = "";

        if (idTeam == 0)
        {
            for (int i = 0; i < 4; i++)
            {
                if (allPlayer[i].distaceBall < distance)
                {
                    distance = allPlayer[i].distaceBall;
                    name = allPlayer[i].name;
                }
            }
        }
        else
        {
            for (int i = 4; i < 8; i++)
            {
                if (allPlayer[i].distaceBall < distance)
                {
                    distance = allPlayer[i].distaceBall;
                    name = allPlayer[i].name;
                }
            }
        }
        return name;

    }
   


    // Update is called once per frame

}
