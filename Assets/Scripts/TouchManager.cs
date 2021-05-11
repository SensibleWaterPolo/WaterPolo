using UnityEngine;

public class TouchManager : MonoBehaviour
{
    private Touch touch;
    private int numTouch;
    private Vector3 endPos;
    private bool loadShoot;
    private bool okShoot;
    private Player player;
    private GoalKeeper gk;
    private bool swimKeep;

    private float minShoot;
    private float maxShoot;
    private int layerMaskPlayer;
    private int layerMaskFight = 1 << 12;
    private int layerMaskGk;
    private bool shootFlag;

    //Segnale di tiro
    public SignalShoot shootSignalPrefab;

    public SignalShoot shootSignal;
    public Vector3 startPos;
    public Vector3 finalPos;

    // Start is called before the first frame update

    // Update is called once per frame

    private void Awake()
    {
        loadShoot = false;
        okShoot = true;
        minShoot = GameObject.Find("ShootLimitDown").transform.position.y; //M: limite inferiore e superiore che contraddistingue
        maxShoot = GameObject.Find("ShootLimitUp").transform.position.y;//un passaggio da un tiro
        layerMaskPlayer = 1 << LayerMask.NameToLayer("Player");
        layerMaskGk = 1 << LayerMask.NameToLayer("GK");
        shootFlag = false;
        swimKeep = false;
    }

    private void Start()
    {
    }

    /*void Update()
    {
        numTouch = Input.touchCount;

        if (numTouch > 0)
        {
           //for (int i = 0; i < numTouch; i++)
                for (int i = 0; i < 1; i++)
                {
                touch = Input.GetTouch(i);

                ///////////// FASE BEGAN
                if (touch.phase == TouchPhase.Began)
                {   startPos= Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);
                    Vector3 posTouch = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);

                    //RaycastHit2D hitPlayer = Physics2D.Raycast(posTouch, (Input.GetTouch(i).position), layerMaskPlayer);

                    //RaycastHit2D hitFight = Physics2D.Raycast(posTouch, (Input.GetTouch(i).position), layerMaskFight);

                    //RaycastHit2D hitGk = Physics2D.Raycast(posTouch, (Input.GetTouch(i).position), layerMaskGk);

                    RaycastHit2D hit = Physics2D.Raycast(posTouch, (Input.GetTouch(i).position), layerMaskGk);
                    okShoot = false;

                    if (hit.collider)
                    {
                        if (hit.collider.CompareTag("GK"))
                        {
                         gk = GameObject.Find(hit.collider.gameObject.name).GetComponent<GoalKeeper>();
                            if (gk.keep && !gk.cpuFlag)
                            {
                                startPos = gk.transform.position;
                                loadShoot = true;

                                SpawnSignal(gk.gameObject);
                            }
                        }
                    }
                    hit = Physics2D.Raycast(posTouch, (Input.GetTouch(i).position), layerMaskPlayer);
                    if (hit.collider)
                    {
                        GameObject obj = hit.collider.gameObject;

                        if (obj.tag == "Player")
                        {
                            player = GameObject.Find(obj.name).GetComponent<Player>();

                            if (!player.cpuFlag)
                            {
                                if (player.keep || player.keepBoa || player.swimKeep )
                                {
                                    loadShoot = true;

                                  if (player.swimKeep)
                                    {
                                        player.selected = true;
                                        player.SetKeep();
                                  }

                                    SpawnSignal(player.gameObject);
                                }
                            }
                        }
                    }
                    hit = Physics2D.Raycast(posTouch, (Input.GetTouch(i).position), layerMaskPlayer);
                    if (hit.collider)
                    {
                        if (hit.collider.CompareTag("Fight"))
                        {
                            hit.collider.GetComponent<FightManager>().P1AddClickLocal();
                        }
                    }
                }

                ////// FASE MOVED
                if (touch.phase == TouchPhase.Moved && loadShoot )
                {
                    endPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);

                    if (player != null)
                    {
                        //player.selected = true;

                        // Debug.Log(Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position), player.transform.position) + " : distanza del passaggio o tiro");
                        if (player.keep)
                        {
                            Utility.RotateObjToPoint(player.gameObject, Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position));
                        }

                        if (Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position).x <= GameObject.Find("LimitLeft").transform.position.x || Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position).x >= GameObject.Find("LimitRight").transform.position.x)
                        {
                            okShoot = false;
                        }
                        else
                        {
                            if (Vector3.Distance(startPos,endPos) <9)
                            {
                                okShoot = false;
                              //  swimKeep = true;
                            }
                            else
                            {
                                okShoot = true;
                              //  swimKeep = false;
                            }
                        }

                        shootSignalPrefab.SetSignal(okShoot); //Aggiorna colore segnale
                    }
                    if (gk != null)
                    {
                        if (gk.keep)
                        {
                            Utility.RotateObjToPoint(gk.gameObject, Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position));
                        }
                        if (Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position).x <= GameObject.Find("LimitLeft").transform.position.x || Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position).x >= GameObject.Find("LimitRight").transform.position.x)
                        {
                            okShoot = false;
                        }
                        else if (gk.idTeam == 0 && Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position).y < gk.transform.position.y && Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position), gk.transform.position) < 9)
                        {
                            okShoot = false;
                        }
                        else if (gk.idTeam == 1 && Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position).y > gk.transform.position.y && Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position), gk.transform.position) < 9)
                        {
                            okShoot = false;
                        }
                        else
                        {
                            okShoot = true;
                        }

                        shootSignalPrefab.SetSignal(okShoot);
                    }
                }

                //FASE ENDED
                if (touch.phase == TouchPhase.Ended && loadShoot )
                {
                    endPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);
                   if (player != null && loadShoot)
                    {
                        if (Vector3.Distance(startPos, endPos) < 9)
                        {
                            okShoot = false;
                            swimKeep = true;
                        }
                        else
                        {
                            okShoot = true;
                            swimKeep = false;
                        }

                            if (minShoot < endPos.y && endPos.y < maxShoot)//decidiamo se è un passaggio o un tiro
                        {
                            shootFlag = false;
                        }
                        else
                        {
                            shootFlag = true;
                        }

                        if (okShoot && !player.keepBoa && !GameCore.current.stopShoot)
                        {
                            player.LoadShoot(endPos, shootFlag, 0);  //se non è una boa in possesso di palla
                            //player.selected = false;
                        }
                        else                //IL GIOCATORE è UNA BOA BISOGNA DECIDERE IL TIPO DI TIRO A SENCONDA DI DOVE INDIRIZZIAMO IL CURSORE
                        if (okShoot && player.keepBoa && !GameCore.current.stopShoot)
                        {
                            LayerMask mask = 1 << 4; //strato water
                            RaycastHit2D hitBoa = Physics2D.Raycast(player.transform.parent.transform.position, (Ball.current.transform.position - player.transform.position).normalized, 20, mask);
                            Debug.DrawRay(player.transform.parent.transform.position, (Ball.current.transform.position - player.transform.position).normalized * 15, Color.black, 3);

                            if (hitBoa.collider != null)
                            {
                                 Debug.Log(player.name + " sono girato verso->" + hitBoa.collider.tag);

                                if (hitBoa.collider.CompareTag("ShootLine")) //Tiro a colonnello per entrambi
                                {
                                    if ((player.idTeam == 0 && endPos.y > player.transform.position.y) || (player.idTeam == 1 && endPos.y < player.transform.position.y))
                                    {
                                        player.LoadShoot(endPos, shootFlag, 3);  // controllo se  davanti al portiere non può fare un colonnello all'indietro
                                    }
                                }
                                else if (hitBoa.collider.CompareTag("Rovesciata"))

                                {
                                    if (player.idTeam == 0 && endPos.x <= player.transform.position.x && endPos.y <= player.transform.position.y)
                                    {   //il giocatore Y effettua un colonnello
                                          Debug.Log(name + "--->rovesciata");
                                        player.LoadShoot(endPos, shootFlag, 3);
                                    }
                                    else if (player.idTeam== 1 && endPos.x >= player.transform.position.x && endPos.y >= player.transform.position.y)
                                    {
                                            Debug.Log(name + "--->rovesciata");
                                        player.LoadShoot(endPos, shootFlag, 3);
                                    }
                                    else if (player.idTeam == 0 && endPos.y > player.transform.position.y || player.idTeam == 1 && endPos.y < transform.position.y)
                                    {
                                        player.LoadShoot(endPos, shootFlag, 1); //Rovesciata per entrambi
                                    }
                                }
                                else if (hitBoa.collider.CompareTag("Sciarpa")) //sciarpa per entrambi a meno che la palla non è indirizzata in porta
                                {
                                    if (player.idTeam == 0 && endPos.x >= player.transform.position.x && endPos.y <= player.transform.position.y)
                                    {   //il giocatore Y effettua un colonnello
                                         Debug.Log(name + "--->sciarpa");
                                        player.LoadShoot(endPos, shootFlag, 3);
                                    }   //il giocatore R effettua un colonnello
                                    else if (player.idTeam == 1 && endPos.x <= player.transform.position.x && endPos.y >= player.transform.position.y)
                                    {
                                         Debug.Log(name + "--->sciarpa");
                                        player.LoadShoot(endPos, shootFlag, 3);
                                    }
                                    else if (player.idTeam == 0 && endPos.y > player.transform.position.y || player.idTeam == 1 && endPos.y < transform.position.y)
                                    {
                                        player.LoadShoot(endPos, shootFlag, 2);
                                    }
                                }
                            }
                            else if (hitBoa.collider == null && player.idTeam == 0)
                            {
                                if (endPos.y <= player.transform.position.y) //colonnello
                                {
                                    player.LoadShoot(endPos, shootFlag, 3);
                                }
                                else if (endPos.y >= player.transform.position.y && endPos.x <= player.transform.position.x) //Rovesciata
                                {
                                    player.LoadShoot(endPos, shootFlag, 1);
                                }
                                else if (endPos.y >= player.transform.position.y && endPos.x > player.transform.position.x) //sciarpa
                                {
                                    player.LoadShoot(endPos, shootFlag, 2);
                                }
                            }
                            else if (hitBoa.collider == null && player.idTeam == 1) //Boa Red
                            {
                                if (endPos.y >= player.transform.position.y) //colonnello
                                {
                                    player.LoadShoot(endPos, shootFlag, 3);
                                }
                                if (endPos.y <= player.transform.position.y && endPos.x >= player.transform.position.x) //Rovesciata
                                {
                                    player.LoadShoot(endPos, shootFlag, 1);
                                }
                                if (endPos.y <= player.transform.position.y && endPos.x < player.transform.position.x) //sciarpa
                                {
                                    player.LoadShoot(endPos, shootFlag, 2);
                                }
                            }
                        }

                        if (!okShoot && swimKeep)
                        {
                            if (player.keep && !player.isInContact)
                            {
                                player.SetSwimKeep();
                            }
                        }

                        if (shootSignalPrefab != null)
                        {
                            DestroySignal();
                        }
                        loadShoot = false;
                        player.selected = false;

                        player = null;
                    }

                    if (gk != null)
                    {
                        if (okShoot && gk.keep && !GameCore.current.stopShoot)
                        {
                            gk.LoadShoot(endPos);
                        }

                        if (shootSignalPrefab != null)
                        {
                            DestroySignal();
                        }
                        loadShoot = false;
                        gk = null;
                    }
                }
                else { }
            }
        }
    }*/

    public void SpawnSignal(GameObject obj)
    {
        shootSignalPrefab = Instantiate(shootSignal, obj.transform.position, Quaternion.identity);
        shootSignalPrefab.transform.parent = obj.transform;
    }

    public void DestroySignal()
    {
        Destroy(shootSignalPrefab.gameObject);
    }

    public void Exit()
    {
        Application.Quit();
    }
}