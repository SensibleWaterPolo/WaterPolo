using UnityEngine;

public class TapZone : MonoBehaviour
{
    // Start is called before the first frame update
    private Player player;

    private void Awake()
    {
        player = transform.parent.GetComponent<Player>();
    }

    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        /* if (player.keep)
             GetComponent<CircleCollider2D>().enabled = true;
         else
             GetComponent<CircleCollider2D>().enabled = false;*/
    }
}