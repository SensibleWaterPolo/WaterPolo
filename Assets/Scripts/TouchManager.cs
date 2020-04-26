using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TouchManager : MonoBehaviour
{
    private Touch touch;
    private int numTouch;
    private Vector3 endPos;
    private bool loadShoot;
    private bool okShoot;
    private Player player;
    private bool restart;
    private float minShoot;
    private float maxShoot;
    private LayerMask layerMaskPlayer = (1 << 9) ;
    private LayerMask layerMaskFight = 1 << 12;

    // Start is called before the first frame update

    // Update is called once per frame

    private void Awake()
    {
        loadShoot = false;
        okShoot = true;       
        minShoot= GameObject.Find("ShootLimitDown").transform.position.y; //M: limite inferiore e superiore che contraddistingue
        maxShoot= GameObject.Find("ShootLimitUp").transform.position.y;//un passaggio da un tiro
    }
    private void Start()
    {
        
         }
    void Update()
    {
        numTouch = Input.touchCount;

        if (numTouch > 0)
        {
            for (int i = 0; i < numTouch; i++)
            {
                touch = Input.GetTouch(i);
                if (touch.phase == TouchPhase.Began)
                {
                    RaycastHit2D hitPlayer = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position), (Input.GetTouch(i).position), layerMaskPlayer);
                    RaycastHit2D hitFight = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position), (Input.GetTouch(i).position), layerMaskFight);


                    if (hitPlayer.collider)
                    {
                        GameObject obj = hitPlayer.collider.gameObject;
                        GameObject.Find("Debug").GetComponent<Text>().text = hitPlayer.collider.gameObject.name;


                        if (obj.tag == "Player")
                        {
                            player = GameObject.Find(obj.name).GetComponent<Player>();
                            if (player.keep)
                            {
                                loadShoot = true;
                                player.CreateSignalShoot();
                                player.signalOK = okShoot;
                            }

                        }
                        if (obj.tag == "Battle")
                        {
                            obj.GetComponent<Battle>().numclick++;
                            Debug.Log("Click battaglia");
                        }
                    }
                    if (hitFight.collider != null)
                    {
                        FightManager obj = GameObject.Find(hitFight.collider.name).GetComponent<FightManager>();
                        if (obj != null)
                        {
                            obj.P1AddClickLocal();
                            Debug.Log(obj.numClickP1);
                        }

                    }
                }
                if (touch.phase == TouchPhase.Moved && loadShoot && player != null)
                {
                    Utility.RotateObjToPoint(player.gameObject, Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position));
                    if (Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position).x <= GameObject.Find("LimitLeft").transform.position.x || Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position).x >= GameObject.Find("LimitRight").transform.position.x)
                    {
                        okShoot = false;
                    }
                    else
                    {
                        okShoot = true;

                    }
                    player.signalOK = okShoot;

                }

                if (touch.phase == TouchPhase.Ended && loadShoot && player != null)
                {


                    Vector3 destination = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);

                    if (okShoot)
                    {
                        if (minShoot < destination.y && destination.y < maxShoot)
                        {
                            player.LoadShoot(destination, false);

                        }
                        else
                        {
                            player.LoadShoot(destination, true);
                        }

                    }
                    player.DestroySignalShoot();
                    loadShoot = false;
                    player = null;
                    restart = true;

                }
            }

        }
    }

    public void Exit()
    {
        Application.Quit();
    }
}


