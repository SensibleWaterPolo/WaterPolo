using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalYellow : GoalManager
{
    
    
    // Start is called before the first frame update
    
    
    void Start()
    {
        score = 0;
        text = GameObject.Find("HomeScore").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = score.ToString();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball") && !goal)
        {
            goal = true;
            score = int.Parse(text.text);
            score++;
            Ball.current.inGameFlag = false;
            Referee.current.SetArmLeft();
            Invoke("ResetScene", 2f);

            
        } 
    }

    public  void ResetScene()
    {


        PosPlayerMng.curret.SetKickOff(1);
        Referee.current.SetMId();
        Referee.current.SetArmFront();
        goal = false;
        

    }
}
