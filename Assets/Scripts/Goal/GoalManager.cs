using UnityEngine;
using UnityEngine.UI;

public class GoalManager : MonoBehaviour
{
    public Text text;
    public int score;
    public bool goal;

    // Start is called before the first frame update
    private void Awake()
    {
        goal = false;
    }

    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }
}