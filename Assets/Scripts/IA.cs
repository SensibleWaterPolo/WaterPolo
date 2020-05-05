using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Policy;
using UnityEngine;

public class IA : MonoBehaviour
{
    private float limitSx, poleSx, perfectSx, perfectDx, poleDx, limitDx;

    private float y = -38f;

    public static  IA current;


    // Start is called before the first frame update

    private void Awake()
    {
        current = this;
        limitSx= GameObject.Find("LimitSx").transform.position.x;
        poleSx = GameObject.Find("LimitPoleSx").transform.position.x;
        perfectSx = GameObject.Find("LimitPerfectSx").transform.position.x;
        perfectDx =  GameObject.Find("LimitPerfectDx").transform.position.x;
        poleDx = GameObject.Find("LimitPoleDx").transform.position.x;
        limitDx = GameObject.Find("LimitDx").transform.position.x;


    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown) 
        {
            if (GameObject.Find("PlayerR6").GetComponent<Player>().marcaFlag)
            {
                PingBoaIsFree("PlayerR1");            
            }
        }
    }

    private Vector3 GetBtwLimitSxPole() 
    {
        return new Vector3(Random.Range(limitSx, poleSx), y, 0);
    }

    private Vector3 GetBtwPoleSxPerfect() 
    { 
       return new Vector3(Random.Range(poleSx,perfectSx), y, 0);
    }
    private Vector3 GetBtwCenter()
    {
        return new Vector3(Random.Range(perfectSx, perfectDx), y, 0);
    }
    private Vector3 GetBtwLimitPerfectDxPole()
    {
        return new Vector3(Random.Range(perfectDx, poleDx), y, 0);
    }
    private Vector3 GetBtwLimitPoleDxLimit()
    {
        return new Vector3(Random.Range(poleDx, limitDx), y, 0);
    }

    public Vector3 ShootNormalR3() 
    {
        float zoneId= Random.Range(0, 100);
        Vector3 posShoot= Vector3.zero;
        if (0 <= zoneId && zoneId < 15) 
        {
            posShoot= GetBtwLimitSxPole();
        }
        else if(15 <= zoneId && zoneId < 30)
        {

            posShoot = GetBtwPoleSxPerfect();
        }
        else if (30 <= zoneId && zoneId < 70)
        {

            posShoot = GetBtwCenter();
        }
        else if (70 <= zoneId && zoneId < 85)
        {

            posShoot = GetBtwLimitPerfectDxPole();
        }
        else if (85 <= zoneId && zoneId < 100)
        {

            posShoot = GetBtwLimitPoleDxLimit();
        }

        return posShoot;
    }

    public Vector3 ShootHardR3()
    {
        float zoneId = Random.Range(0, 100);
        Vector3 posShoot = Vector3.zero;
        if (0 <= zoneId && zoneId < 15)
        {
            posShoot = GetBtwLimitSxPole();
        }
        else if (5 <= zoneId && zoneId < 35)
        {

            posShoot = GetBtwPoleSxPerfect();
        }
        else if (35 <= zoneId && zoneId < 65)
        {

            posShoot = GetBtwCenter();
        }
        else if (65 <= zoneId && zoneId < 95)
        {

            posShoot = GetBtwLimitPerfectDxPole();
        }
        else if (95 <= zoneId && zoneId < 100)
        {

            posShoot = GetBtwLimitPoleDxLimit();
        }

        return posShoot;
    }
    public Vector3 ShootNormalR1()
    {
        float zoneId = Random.Range(0, 100);
        Vector3 posShoot = Vector3.zero;
        if (0 <= zoneId && zoneId < 10)
        {
            posShoot = GetBtwLimitSxPole();
        }
        else if (10 <= zoneId && zoneId < 20)
        {

            posShoot = GetBtwPoleSxPerfect();
        }
        else if (20 <= zoneId && zoneId < 60)
        {

            posShoot = GetBtwCenter();
        }
        else if (60 <= zoneId && zoneId < 80)
        {

            posShoot = GetBtwLimitPerfectDxPole();
        }
        else if (80 <= zoneId && zoneId < 100)
        {

            posShoot = GetBtwLimitPoleDxLimit();
        }

        return posShoot;

    }
    public Vector3 ShootHardR1()
    {
        float zoneId = Random.Range(0, 100);
        Vector3 posShoot = Vector3.zero;
        if (0 <= zoneId && zoneId < 5)
        {
            posShoot = GetBtwLimitSxPole();
        }
        else if (5 <= zoneId && zoneId < 25)
        {

            posShoot = GetBtwPoleSxPerfect();
        }
        else if (25 <= zoneId && zoneId < 65)
        {

            posShoot = GetBtwCenter();
        }
        else if (65 <= zoneId && zoneId < 95)
        {

            posShoot = GetBtwLimitPerfectDxPole();
        }
        else if (95 <= zoneId && zoneId < 100)
        {

            posShoot = GetBtwLimitPoleDxLimit();
        }

        return posShoot;
    }

    public Vector3 ShootNormalR5()
    {
        float zoneId = Random.Range(0, 100);
        Vector3 posShoot = Vector3.zero;
        if (0 <= zoneId && zoneId < 20)
        {
            posShoot = GetBtwLimitSxPole();
        }
        else if (20 <= zoneId && zoneId < 40)
        {

            posShoot = GetBtwPoleSxPerfect();
        }
        else if (40 <= zoneId && zoneId < 80)
        {

            posShoot = GetBtwCenter();
        }
        else if (80 <= zoneId && zoneId < 90)
        {

            posShoot = GetBtwLimitPerfectDxPole();
        }
        else if (90 <= zoneId && zoneId < 100)
        {

            posShoot = GetBtwLimitPoleDxLimit();
        }

        return posShoot;
    }

    public Vector3 ShootHardR5()
    {
        float zoneId = Random.Range(0, 100);
        Vector3 posShoot = Vector3.zero;
        if (0 <= zoneId && zoneId < 5)
        {
            posShoot = GetBtwLimitSxPole();
        }
        else if (5 <= zoneId && zoneId < 35)
        {

            posShoot = GetBtwPoleSxPerfect();
        }
        else if (35 <= zoneId && zoneId < 75)
        {

            posShoot = GetBtwCenter();
        }
        else if (75 <= zoneId && zoneId < 95)
        {

            posShoot = GetBtwLimitPerfectDxPole();
        }
        else if (95 <= zoneId && zoneId < 100)
        {

            posShoot = GetBtwLimitPoleDxLimit();
        }

        return posShoot;
    }

    
  //Decisione di passare o tirare
    public bool DecisionShoot(Player player) // prende la decisione di tirare o passare
    {
        float prob = Random.Range(0, 100);
         bool shoot = false;
        float posY = Ball.current.transform.position.y;
        if (posY > 17.5) //Palla nella zona di difesa 
        {
            if (prob < 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        else if (17.5 >= posY && posY >= -12.5)
        {
            if (player.name== "PlayerR3") 
            {
               
                if (prob < 50)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
           

            if (prob < 10)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (posY < -12.5) 
        {
            if (!player.boaFlag && !player.marcaFlag)
            {
                if (prob < 60)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else 
            {
                return true;
            
            }
        }
        
        return shoot;
    
    }

    //Pinga la boa per controllare se è libera verso il giocatore
    public bool PingBoaIsFree(string name)
    {
        bool isFree = false;
        LayerMask mask = 1 << 4;
        RaycastHit2D hitBoa = Physics2D.Raycast(GameObject.Find("PlayerR6").transform.position, (GameObject.Find("PlayerR6").transform.GetChild(6).transform.position - GameObject.Find("PlayerR6").transform.position).normalized, 20, mask);
        Debug.DrawRay(GameObject.Find("PlayerR6").transform.position, (GameObject.Find("PlayerR6").transform.GetChild(6).transform.position - GameObject.Find("PlayerR6").transform.position).normalized * 15, Color.cyan, 5);
        if (hitBoa.collider != null)
        {
           
            if (hitBoa.collider.CompareTag("ShootLine"))
            {
                Debug.Log("Boa non libera");
                return false;
            }

            else if (hitBoa.collider.CompareTag("Rovesciata"))

            {
                if (name == "PlayerR1")
                {
                    Debug.Log("posizione rovesciata");
                    return false;
                }
                else if (name == "PlayerR3")
                {
                    Debug.Log("posizione rovesciata");
                    return false;
                }
                else if (name == "PlayerR5")
                {
                    Debug.Log("posizione rovesciata");
                    return true;
                }

            }

            else if (hitBoa.collider.CompareTag("Sciarpa"))
            {
                if (name == "PlayerR1")
                {
                    Debug.Log("posizione sciarpa");
                    return true;
                }
                else if (name == "PlayerR3")
                {
                    Debug.Log("posizione sciarpa");
                    return false;
                }
                else if (name == "PlayerR5")
                {
                    Debug.Log("posizione sciarpa");
                    return false;
                }

            }
        }
        else if (hitBoa.collider == null)
        {
            
            if (name == "PlayerR1")
            {
                Debug.Log("posizione di spalle");
                return true;
            }
            else if (name == "PlayerR3")
            {
                Debug.Log("posizione di spalle");

                return true;
            }
            else if (name == "PlayerR5")
            {
                Debug.Log("posizione di spalle");

                return true;
            }
        }

             return isFree;
    }

    //Rirorna l'id che identifica la zona di possesso
    public int ZoneBall(Player player) //0: zona difesa, 1: zona centrocampo, 2:zona attacco
    {
        int id = -1;
        float posY = player.transform.position.y;
        if (posY > 17.5) //Palla nella zona di difesa 
        {
            return 0;
        }
        else if (17.5 >= posY && posY >= -12.5)
        {
            return 1;
        }
        else if (posY < -12.5)
        {
            return 2;
        }

       return id;
    }

  
    //Riferimento della posizione della boa 0: spalle alla porta, 1: faccia alla porta, 2: verso 5, 3: verso 1
    public int IdPosBoa() 
    {
        int id = -1;
        LayerMask mask = 1 << 4;
        RaycastHit2D hitBoa = Physics2D.Raycast(GameObject.Find("PlayerR6").transform.parent.transform.position, (Ball.current.transform.position - GameObject.Find("PlayerR6").transform.position).normalized, 20, mask);
        if (hitBoa.collider != null)
        {
            if (hitBoa.collider.CompareTag("ShootLine"))
            {
                return 1;
            }

            else if (hitBoa.collider.CompareTag("Rovesciata"))

            {
                return 2;
            }

            else if (hitBoa.collider.CompareTag("Sciarpa"))
            {
                return 3;
            }
        }
        else if (hitBoa.collider == null)
        {
            return 0;
        }

        return id;
    
    
    
    }
}
