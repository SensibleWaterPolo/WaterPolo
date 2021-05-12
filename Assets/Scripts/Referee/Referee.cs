using System;
using TouchScript.Gestures;
using TouchScript.Gestures.TransformGestures;
using UnityEngine;

public class Referee : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 posMid = new Vector3(30f, 2.3f, 0);

    public static Referee current;

    [SerializeField]
    private RefereeAnimationController _animationController;

    [SerializeField]
    private TransformGesture _transformGesture;

    [SerializeField]
    private TapGesture _tapGesture;

    private Vector2 _startTap;

    private void Start()
    {
        _animationController.PlayAnimation(RefereeAnimationController.ERefereeAnim.Watch);

        _transformGesture.TransformStarted += HandleStartTransform;
        _transformGesture.Transformed += HandleTrasformGesture;
        _tapGesture.Tapped += HandleTapGesture;

    }

    private void HandleTapGesture(object sender, EventArgs e)
    {
        Debug.Log("TAPPED");
    }

    private void HandleTrasformGesture(object sender, EventArgs e)
    {
        transform.position = _transformGesture.GetScreenPositionHitData().Point;



    }

    private void HandleStartTransform(object sender, EventArgs e)
    {
        _startTap = _transformGesture.ScreenPosition;

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