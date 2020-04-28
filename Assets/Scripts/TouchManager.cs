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
    private int layerMaskPlayer;  
    private LayerMask layerMaskFight = 1 << 12;
    private bool shootFlag;

    // Start is called before the first frame update

    // Update is called once per frame

    private void Awake()
    {
        loadShoot = false;
        okShoot = true;       
        minShoot= GameObject.Find("ShootLimitDown").transform.position.y; //M: limite inferiore e superiore che contraddistingue
        maxShoot= GameObject.Find("ShootLimitUp").transform.position.y;//un passaggio da un tiro
        layerMaskPlayer= 1 << LayerMask.NameToLayer("Player");
        shootFlag = false;
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
                        if (obj.tag == "Player")
                        {

                            player = GameObject.Find(obj.name).GetComponent<Player>();
                            if (player.keep || player.keepBoa)
                            {
                                loadShoot = true;
                                player.CreateSignalShoot();
                                player.signalOK = okShoot;
                            }

                        }
                        if (obj.tag == "Battle")
                        {
                            obj.GetComponent<Battle>().numclick++;
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
                    if (player.keep && !player.keepBoa) {

                        Utility.RotateObjToPoint(player.gameObject, Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position)); 
                    }

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

                 
                        if (minShoot < destination.y && destination.y < maxShoot)//decidiamo se è un passaggio o un tiro
                        {
                        shootFlag = false;    
                        }
                        else
                        {
                        shootFlag = true;
                           
                        }

                    if (okShoot && !player.keepBoa) 
                    {
                        player.LoadShoot(destination, shootFlag, 0);

                    }

                    

                    //IL GIOCATORE è UNA BOA BISOGNA DECIDERE IL TIPO DI TIRO A SENCONDA DI DOVE INDIRIZZIAMO IL CURSORE
                    if (okShoot && player.keepBoa && player.idTeam==0) //Boa yellow 
                    {  
                        
                        if (destination.y <= player.transform.position.y) //colonnello
                        {
                            player.LoadShoot(destination, shootFlag, 3);
                        }
                        if (destination.y >= player.transform.position.y && destination.x <= player.transform.position.x) //Rovesciata
                        {
                            player.LoadShoot(destination, shootFlag, 1);

                        }
                        if (destination.y >= player.transform.position.y && destination.x > player.transform.position.x) //sciarpa
                        {

                            player.LoadShoot(destination, shootFlag, 2);
                        }
                    }
                    if (okShoot && player.keepBoa && player.idTeam == 1) //Boa Red
                    {

                        if (destination.y >= player.transform.position.y) //colonnello
                        {
                            Debug.Log("colonnello red");
                            player.LoadShoot(destination, shootFlag, 3);
                        }
                        if (destination.y <= player.transform.position.y && destination.x >= player.transform.position.x) //Rovesciata
                        {
                            Debug.Log("rovesciata red");
                            player.LoadShoot(destination, shootFlag, 1);

                        }
                        if (destination.y <= player.transform.position.y && destination.x < player.transform.position.x) //sciarpa
                        {
                            Debug.Log("scirpa red");
                            player.LoadShoot(destination, shootFlag, 3);
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


