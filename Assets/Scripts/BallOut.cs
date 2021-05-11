using UnityEngine;

public class BallOut : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball") && Ball.current.inGameFlag)
        {
            Fischia();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
    }

    public void KeepToGk()
    {
        if (Ball.current.fieldYellow && Ball.current.idTeam == 1)
        {
            GameObject.Find("YellowGK").GetComponent<GoalKeeper>().SetKeep();
        }
        else if (Ball.current.fieldYellow && Ball.current.idTeam == 0)
        {
            GameObject.Find("RedGK").GetComponent<GoalKeeper>().SetKeep();
        }
        else if (!Ball.current.fieldYellow && Ball.current.idTeam == 0)
        {
            GameObject.Find("RedGK").GetComponent<GoalKeeper>().SetKeep();
        }
        else if (Ball.current.fieldYellow && Ball.current.idTeam == 1)
        {
            GameObject.Find("YellowGK").GetComponent<GoalKeeper>().SetKeep();
        }
        Ball.current.inGameFlag = true;
    }

    public void Fischia()
    {
        AudioController.current.DoFischio();

        PosPlayerMng.curret.SetAllBicy();
        Ball.current.inGameFlag = false;
        /*  if (Ball.current.fieldYellow)
          {
             // Referee.current.SetArmRight();
          }
          else
              Referee.current.SetArmLeft();*/

        Invoke("KeepToGk", 1);
    }
}