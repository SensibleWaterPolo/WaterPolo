using UnityEngine;
public class Ball : MonoBehaviour
{
    public static Ball current; //M:la palla è un singolo oggetto per tutti quindi fa riferimento ad un Singleton 


    [Header("STAT Var")] //M:
    public float speed; //M: velocità palla

    [Header("FLAG Var")]
    public bool freeFlag; //M: TRUE se non è in possesso di un giocatore
    public bool inGameFlag; //M: TRUE se la palla è in gioco e non uscita fuori
    public bool deceleratePass; //M: decelera la palla dopo un passaggio 
    public bool decelerateShoot; //M:decelera in caso di tiro
    public bool motionlessFlag; //M: true se la palla è ferma
    public bool shootFlag; //M: true se è un tiro, false se è un passaggio
    public bool isShooted; //M: True se la palla è in movimento dopo un tiro/passaggio
    public bool respawn; //appena torna in gioco dopo un tiro non può entrare in possesso di un altro giocatore
    public bool isEnable;

    public float shoot; //M: forza del giocatore
    public float pas;//M: forza del giocatore passaggio

    public float throwIn; //forza del rilancio del portiere
    private Rigidbody2D rb;
    public Player player; //M: Giocatore in possesso della palla
    public int idTeam; //M: 0:Yellow 1: Red
    public GoalKeeper gk;

    private Vector3 finalPos; //M: posizione finale della palla dopo un tiro/pass

    private EStatoPalla _statoPalla; //M: stato della posizione, 0: palla in possesso, 1: palla nel settore sx, 2: palla nel settore centrale, 3: palla nel settore dx, -1:indefinito

    public bool fieldYellow; //M; true se la palla è nella merà campo gialla

    private ParticleSystem particle;

    public float distance = 0; //distanza dal punto di arrivo
    public float maxHightBall; //punto medio del lancio in cui la dimensione della palla in altezza è il massimo

    public string redNear;
    public string yellowNear;
    public string moreNear;



    private void Awake() //M:inizializzazione variabili 
    {
        current = this;
        freeFlag = true;
        inGameFlag = true;
        deceleratePass = true;
        decelerateShoot = true;
        rb = GetComponent<Rigidbody2D>();
        shootFlag = true;
        isShooted = false;
        idTeam = -1;
        respawn = false;
        isEnable = true;

    }



    private void FixedUpdate()
    {
        //CheckFreeBall();
        CheckVel(); //M: se la velocità è prossima allo zero ferma la palla

        if (deceleratePass)
        {
            DecelerateVelPass();
        }
        else if (decelerateShoot)
        {
            DecelerateVelShoot();

        }

        if (!shootFlag)
        {
            CheckPositionPass();
        }

        UpdateStatePos();

        UpdateSideBall();



        redNear = PosPlayerMng.curret.GetPlayerForTeamNearBall(1, false);
        yellowNear = PosPlayerMng.curret.GetPlayerForTeamNearBall(0, false);
        moreNear = PosPlayerMng.curret.GetPlayerNameNearBall();

        //  Debug.Log("SPEED "+speed+ " /shootflag " +shootFlag+ " /player "+player+ " /libera :"+freeFlag+" /ferma "+motionlessFlag+ " /shooted "+isShooted);





    }

    public void ShootBall(Vector3 finalPos, bool shootFlag, int id) //M: prepara la palla al tiro e ne calcola la forza, id=0 giocatore, id=1 portiere
    {
        this.finalPos = finalPos;
        this.shootFlag = shootFlag;

        EnableBall();
        distance = Vector2.Distance(transform.position, finalPos);
        maxHightBall = distance / 2f;
        Vector3 pos = GetComponent<Transform>().position;
        Vector3 direct = new Vector2(finalPos.x - pos.x, finalPos.y - pos.y).normalized;
        if (id == 0)
        {
            if (shootFlag)
            {
                if (!GameCore.current.secExpired)
                {
                    GetComponent<Rigidbody2D>().AddForce(direct * shoot * 450);
                }
                else
                {
                    GetComponent<Rigidbody2D>().AddForce(direct * shoot * 600);
                }
                decelerateShoot = true;
                deceleratePass = false;
            }
            else
            {
                GetComponent<Rigidbody2D>().AddForce(direct * pas * 500);
                deceleratePass = true;
                decelerateShoot = false;
            }
        }
        else if (id == 1)
        {
            if (!GameCore.current.secExpired)
            {
                GetComponent<Rigidbody2D>().AddForce(direct * throwIn * 500);
            }
            else { GetComponent<Rigidbody2D>().AddForce(direct * throwIn * 800); }
            decelerateShoot = false;
            deceleratePass = true;
        }

        if (gk != null)
        {
            gk = null;
        }
        if (player != null)
        {
            player.ballFlag = false;
            player = null;
        }
        isShooted = true;


    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Debug.Log(collision.gameObject.name);
    }



    public void EnableBall()
    {
        freeFlag = true;
        transform.parent = null;
        GetComponent<Renderer>().enabled = true;
        this.gameObject.AddComponent<Rigidbody2D>();
        rb = GetComponent<Rigidbody2D>();
        respawn = true;
        isEnable = true;

    }

    public void CheckVel()  //M: se la velocità è quasi zero(3) blocca la palla
    {
        if (rb != null)
        {


            speed = rb.velocity.magnitude;
            if (speed > 0 && respawn)
            {
                respawn = false;
            }

            if (speed <= 3 && !respawn && freeFlag)
            {

                StopBall();

                deceleratePass = false;
                decelerateShoot = false;
                motionlessFlag = true;
                speed = 0;
                isShooted = false;
                /*  if (player != null)
                  {
                      player = null;
                  }*/

            }
            else
            {
                motionlessFlag = false;
                deceleratePass = true;
                decelerateShoot = true;

            }

        }
    }

    public void DecelerateVelPass() //M: Diminuisce la velocità della palla
    {
        if (rb != null)
            rb.velocity = rb.velocity - (rb.velocity * 1.4f * Time.deltaTime);
    }
    public void DecelerateVelShoot() //M: Diminuisce la velocità della palla
    {
        if (rb != null)
            rb.velocity = rb.velocity - (rb.velocity * 0.4f * Time.deltaTime);
    }
    public void SetPlayer(Player player) //M: assegna alla palla il giocatore in possesso
    {
        DisableBall();
        this.player = player;
        if (player.idTeam != idTeam)
        {
            /* GameCore.current.RestartTimeAction();
             GameCore.current.startSec = true;*/
            idTeam = player.idTeam;
            isShooted = false;
        }

    }
    public void SetGK(GoalKeeper _gk)
    {
        DisableBall();
        isShooted = false;
        this.gk = _gk;
        idTeam = _gk.idTeam;


    }
    public void DisableBall()
    {
        GetComponent<Renderer>().enabled = false;
        if (GetComponent<Rigidbody2D>() != null)
        {
            Destroy(GetComponent<Rigidbody2D>());
        }
        speed = -1;
        isEnable = false;
        //CheckFreeBall();
    }


    public void CheckPositionPass() //M: in caso di passaggio controlla la posizione della palla
    {
        Vector2 pos = transform.position;
        Vector2 dest = finalPos;

        if ((pos - dest).sqrMagnitude <= 1)
        {
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
            }
        }

    }

    public void StopBall() //M:ferma immediatamente la palla
    {
        rb.velocity = Vector2.zero;
        //  rb.angularVelocity = 0;

    }


    public void UpdateStatePos()  //M: determina in quale settore di campo si trova  la palla
    {
        if (!freeFlag)
            statePos = 0;
        else if (GameObject.Find("LimitLeft").transform.position.x < transform.position.x && GameObject.Find("LimitMidLeft").transform.position.x >= transform.position.x)
            statePos = 1;
        else if (GameObject.Find("LimitMidLeft").transform.position.x < transform.position.x && GameObject.Find("LimitMidRight ").transform.position.x >= transform.position.x)
            statePos = 2;
        else if (GameObject.Find("LimitMidRight ").transform.position.x < transform.position.x && GameObject.Find("LimitRight").transform.position.x >= transform.position.x)
            statePos = 3;
        else statePos = -1;

    }

    public void UpdateSideBall()  //M:aggiorna la posione della palla rispetto la metà campo
    {
        if (transform.position.y < GameObject.Find("MiddleY").transform.position.y)
            fieldYellow = true;
        else fieldYellow = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)

    {

        isShooted = false;
        if (collision.gameObject.CompareTag("Side"))
        {
            rb.velocity = rb.velocity / 3;
        }

        if (collision.gameObject.CompareTag("Arm"))
        {
            rb.velocity = rb.velocity / 2;
            GameCore.current.RestartTimeAction();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.CompareTag("EndRed") && inGameFlag)
        {
            inGameFlag = false;
            Invoke("SetRedSideBall", 1.5f);
        }
        if (collision.CompareTag("EndYellow"))
        {
            inGameFlag = false;
            Invoke("SetYellowSideBall", 1.5f);

        }
    }

    public string GetNamePlayer()
    {
        if (player)
            return player.name;
        else return null;
    }

    public void SetMidPos()
    {
        transform.position = GameObject.Find("MidBall").transform.position;
        inGameFlag = true;
    }

    public void SetRedSideBall()
    {
        transform.position = GameObject.Find("RedSideBall").transform.position;
        inGameFlag = true;
    }
    public void SetYellowSideBall()
    {
        transform.position = GameObject.Find("YellowSideBall").transform.position;
        inGameFlag = true;
    }


    public bool CheckBallIsPlayable(float limitVel) //controlla se la palla è giocabile sotto un limite di velocità
    {
        if (inGameFlag)
        {
            if ((isShooted && speed <= limitVel && speed >= 0 && freeFlag && !respawn) || !isShooted && speed == 0 && freeFlag && !respawn)
            {
                return true;
            }
            else
                return false;
        }
        else
            return false;

    }


    public enum EStatoPalla
    {
        LiberaSx,
        LiberaDx,
        LiberaCentro,
        InPossesso,
        Fuori

    }





}
