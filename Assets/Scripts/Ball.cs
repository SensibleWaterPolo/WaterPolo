using System.Collections;
using System.Collections.Generic;
//using UnityEditorInternal;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class  Ball : MonoBehaviour
{
    public static Ball current; //M:la palla è un singolo oggetto per tutti quindi fa riferimento ad un Singleton 


    [Header("STAT Var")] //M:
    public float speed; //M: velocità palla

    [Header("FLAG Var")]
    public bool freeFlag; //M: TRUE se non è in possesso di un giocatore
    public bool inGameFlag; //M: TRUE se la palla è in gioco e non uscita fuori
    public bool decelerateFlag; //M: decelera la palla
    public bool motionlessFlag; //M: true se la palla è ferma
    public bool shootFlag; //M: true se è un tiro, false se è un passaggio
    public bool isShooted; //M: True se la palla è in movimento dopo un tiro/passaggio

    public float shoot; //M: forza del giocatore
    public float pas;//M: forza del giocatore passaggio
    private Rigidbody2D rb;
    public Player player; //M: Giocatore in possesso della palla
    public int idTeam; //M: 0:Yellow 1: Red

    private Vector3 finalPos; //M: posizione finale della palla dopo un tiro/pass

    public int statePos; //M: stato della posizione, 0: palla in possesso, 1: palla nel settore sx, 2: palla nel settore centrale, 3: palla nel settore dx, -1:indefinito

    public bool fieldYellow; //M; true se la palla è nella merà campo gialla

    private ParticleSystem particle;

    
    private void Awake() //M:inizializzazione variabili 
    {
        current = this;
        freeFlag = true;
        inGameFlag = true;
        decelerateFlag = true;
        rb = GetComponent<Rigidbody2D>();
        shootFlag = true;
        isShooted = false;
       
    }

    // Start is called before the first frame update
    void Start()
    {
       
       
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    private void FixedUpdate()
    {
        CheckVel(); //M: se la velocità è prossima allo zero ferma la palla
        
        if (decelerateFlag)
            DecelerateVel();
       
        if (!shootFlag) 
            CheckPositionPass();
        
        UpdateStatePos();
        
        UpdateSideBall();

             
      //  Debug.Log("SPEED "+speed+ " /shootflag " +shootFlag+ " /player "+player+ " /libera :"+freeFlag+" /ferma "+motionlessFlag+ " /shooted "+isShooted);
       


        

    }

    public void ShootBall(Vector3 finalPos, bool shootFlag) //M: prepara la palla al tiro e ne calcola la forza
    {
        this.finalPos = finalPos;
        this.shootFlag = shootFlag;
        EnableBall();
        Vector3 pos = GetComponent<Transform>().position;
        Vector3 direct = new Vector2(finalPos.x - pos.x,finalPos.y-pos.y).normalized;
        if (shootFlag)
        {
            GetComponent<Rigidbody2D>().AddForce(direct * shoot * 300);
        }
        else
        {
            GetComponent<Rigidbody2D>().AddForce(direct * pas * 500);
        }  
        
      
        isShooted = true;
        player.ballFlag = false;
        decelerateFlag = true;
        freeFlag = true;
        

    }

    public void DisableBall() 
    {
        GetComponent<Renderer>().enabled = false;
        Destroy(GetComponent<Rigidbody2D>());
    }

    public void EnableBall() 
    {
        transform.parent = null;
        GetComponent<Renderer>().enabled = true;
        this.gameObject.AddComponent<Rigidbody2D>();
        rb = GetComponent<Rigidbody2D>();

    }

    public void CheckVel()  //M: se la velocità è quasi zero(3) blocca la palla
    {
        if (rb != null)
        {
            speed = rb.velocity.magnitude;
            if (speed <= 3)
            {
                StopBall();
                
                decelerateFlag = false;
                motionlessFlag = true;
                speed = 0;
                isShooted = false;
                                                            
            }
            else
            {
                motionlessFlag = false;
                decelerateFlag = true;
                isShooted = true;
            }

        }
    }

    public void DecelerateVel() //M: Diminuisce la velocità della palla
    {
        if (rb != null)
            rb.velocity = rb.velocity - (rb.velocity * 1.4f * Time.deltaTime);
    }  
    public void SetPlayer(Player player) //M: assegna alla palla il giocatore in possesso
    {
        this.player = player;
        isShooted = false;
        idTeam = player.idTeam;
        freeFlag = false;
    }

    public void ResetBall() 
    {      
        player.ballFlag = false;
        this.player = null; 
        this.shootFlag = true;
       
    }
    
    public void CheckPositionPass() //M: in caso di passaggio controlla la posizione della palla
    {
        Vector2 pos = transform.position;
        Vector2 dest = finalPos;
        if ((pos-dest).sqrMagnitude<=1)
           if (rb != null)
                rb.velocity = Vector2.zero;
        
           
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
            rb.velocity = rb.velocity / 2;
        }
       
      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EndRed") && inGameFlag)
        {
            inGameFlag = false;
            Invoke("SetRedSideBall",1.5f);
        }
        if (collision.CompareTag("EndYellow"))
        {
            inGameFlag = false;
            Invoke("SetYellowSideBall", 1.5f);
        
        }
    }

    public string GetNamePlayer() {
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


}
