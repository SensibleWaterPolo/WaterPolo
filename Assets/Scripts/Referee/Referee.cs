using UnityEngine;

public class Referee : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 posMid = new Vector3(30f, 2.3f, 0);

    public static Referee current;

    private RefereeAnimationController _animationController;

    private void Start()
    {
        _animationController = GetComponent<RefereeAnimationController>();
        _animationController.PlayAnimation(RefereeAnimationController.ERefereeAnim.Watch);

    }

    // Update is called once per frame
    private void Update()
    {

    }

    /* public void UpdatePos()
     {
         if (Ball.current.inGameFlag && !GameObject.Find("RedGK").GetComponent<GoalKeeper>().keep && !GameObject.Find("YellowGK").GetComponent<GoalKeeper>().keep)

         {
             float Y = Ball.current.transform.position.y;

             if (Y - transform.position.y > 0 && Y - transform.position.y > 15)
             {
                 SetWalkUp();
                 transform.Translate(Vector3.up * 3 * Time.deltaTime);
             }
             else if (Y - transform.position.y < 0 && Y - transform.position.y < -15)
             {
                 transform.Translate(Vector3.down * 3 * Time.deltaTime);
                 SetWalkDown();
             }
             else
             {
                 SetWatch();
             }
         }
     }*/
}