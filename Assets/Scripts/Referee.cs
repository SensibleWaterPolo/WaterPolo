using UnityEngine;

public class Referee : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 posMid = new Vector3(30f, 2.3f, 0);

    public Animator anim;

    public static Referee current;

    private void Start()
    {
        anim = GetComponent<Animator>();
        current = this;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        UpdatePos();
    }

    public void SetWatch()
    {
        anim.SetInteger("IdAnim", 0);
    }

    public void SetWalkDown()
    {
        anim.SetInteger("IdAnim", 2);
    }

    public void SetWalkUp()
    {
        anim.SetInteger("IdAnim", 1);
    }

    public void SetArmLeft()
    {
        anim.SetInteger("IdAnim", 3);
    }

    public void SetArmRight()
    {
        anim.SetInteger("IdAnim", 4);
    }

    public void SetArmFront()
    {
        anim.SetInteger("IdAnim", 5);
    }

    public void SetMId()
    {
        transform.position = posMid;
        SetArmFront();
    }

    public void UpdatePos()
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
    }
}