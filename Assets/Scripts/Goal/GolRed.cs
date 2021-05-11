using UnityEngine;
using UnityEngine.UI;

public class GolRed : GoalManager
{
    // Start is called before the first frame update
    private void Start()
    {
        score = 0;
        text = GameObject.Find("AwayScore").GetComponent<Text>();
    }

    // Update is called once per frame
    private void Update()
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
            Referee.current.SetArmRight();
            AudioController.current.DoFischio();
            AudioController.current.DoEsultanza();
            Invoke("ResetScene", 2f);
            GameCore.current.ShowGoalAnimation();
        }
    }

    public void ResetScene()
    {
        PosPlayerMng.curret.SetKickOff(0);
        goal = false;
        Referee.current.SetMId();
        Referee.current.SetArmFront();
        GameCore.current.DeleteGoalAnimation();
    }
}