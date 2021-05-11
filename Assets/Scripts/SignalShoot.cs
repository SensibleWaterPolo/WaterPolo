using UnityEngine;

public class SignalShoot : MonoBehaviour
{
    public Player player;
    public GoalKeeper gk;

    // Start is called before the first frame update

    private void Awake()
    {
    }

    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        /* if (player)
         { GetComponent<Animator>().SetBool("Shoot", player.signalOK); }

         if (gk)
         {
             GetComponent<Animator>().SetBool("Shoot", gk.signalOK);
         }*/
    }

    public void SetSignal(bool signal)
    {
        GetComponent<Animator>().SetBool("Shoot", signal);
    }
}