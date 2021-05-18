using UnityEngine;

public class IA : MonoBehaviour
{
    /* public float limitSx, poleSx, perfectSx, perfectDx, poleDx, limitDx;

     public float y;

     public static IA current;

     // Start is called before the first frame update

     private void Awake()
     {
         current = this;
         /* limitSx = GameObject.Find("LimitSx").transform.position.x;
          poleSx = GameObject.Find("LimitPoleSx").transform.position.x;
          perfectSx = GameObject.Find("LimitPerfectSx").transform.position.x;
          perfectDx = GameObject.Find("LimitPerfectDx").transform.position.x;
          poleDx = GameObject.Find("LimitPoleDx").transform.position.x;
          limitDx = GameObject.Find("LimitDx").transform.position.x;
          y = limitDx = GameObject.Find("LimitDx").transform.position.y;*/
}

/* private void Start()
 {
 }

 // Update is called once per frame
 private void Update()
 {
 }

 private Vector3 GetBtwLimitSxPole()
 {
     // Debug.Log("Tiro limite sx palo");
     return new Vector3(Random.Range(limitSx, poleSx), y, 0);
 }

 private Vector3 GetBtwPoleSxPerfect()
 {
     //  Debug.Log("tiro quadrella sx");
     return new Vector3(Random.Range(poleSx, perfectSx), y, 0);
 }

 private Vector3 GetBtwCenter()
 {
     //  Debug.Log("tiro centrale");
     return new Vector3(Random.Range(perfectSx, perfectDx), y, 0);
 }

 private Vector3 GetBtwLimitPerfectDxPole()
 {
     //  Debug.Log("tiro quadrella destra");
     return new Vector3(Random.Range(perfectDx, poleDx), y, 0);
 }

 private Vector3 GetBtwLimitPoleDxLimit()
 {
     //  Debug.Log("tiro limite destro");
     return new Vector3(Random.Range(poleDx, limitDx), y, 0);
 }

 /* public Vector3 ShootNormalR3()
  {
      float zoneId= GetRandom();
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
  }*/

/*  public Vector3 ShootHardR3()
  {
      float zoneId = GetRandom();
      Vector3 posShoot = Vector3.zero;
      if (0 <= zoneId && zoneId < 5)
      {
          posShoot = GetBtwLimitSxPole();
      }
      else if (5 <= zoneId && zoneId < 25)
      {
          posShoot = GetBtwPoleSxPerfect();
      }
      else if (25 <= zoneId && zoneId < 75)
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

  /* public Vector3 ShootNormalR1()
   {
       float zoneId = GetRandom();
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
   }*/

/* public Vector3 ShootHardR1()
 {
     float zoneId = GetRandom();
     Vector3 posShoot = Vector3.zero;
     if (0 <= zoneId && zoneId < 5)
     {
         posShoot = GetBtwLimitSxPole();
     }
     else if (5 <= zoneId && zoneId < 20)
     {
         posShoot = GetBtwPoleSxPerfect();
     }
     else if (20 <= zoneId && zoneId < 60)
     {
         posShoot = GetBtwCenter();
     }
     else if (60 <= zoneId && zoneId < 95)
     {
         posShoot = GetBtwLimitPerfectDxPole();
     }
     else if (95 <= zoneId && zoneId < 100)
     {
         posShoot = GetBtwLimitPoleDxLimit();
     }

     return posShoot;
 }

 /*  public Vector3 ShootNormalR5()
   {
       float zoneId = GetRandom();
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
   }*/

/*   public Vector3 ShootHardR5()
   {
       float zoneId = GetRandom();
       Vector3 posShoot = Vector3.zero;
       if (0 <= zoneId && zoneId < 5)
       {
           posShoot = GetBtwLimitSxPole();
       }
       else if (5 <= zoneId && zoneId < 30)
       {
           posShoot = GetBtwPoleSxPerfect();
       }
       else if (30 <= zoneId && zoneId < 80)
       {
           posShoot = GetBtwCenter();
       }
       else if (80 <= zoneId && zoneId < 95)
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
       float posY = player.transform.position.y;
       if (GameCore.current.secCurrent < 3)
       {
           Debug.Log("Tiro che è finito il tempo");
           if (Random.value > 0.5)
           {
               return true;
           }
       }
       else if (player.arrivedFlagCounterAtt)
       {
           return true;
       }
       else

       if (posY > 17.5) //Palla nella zona di difesa
       {
           //  Debug.Log(player.name+" Sono nella zona di difesa->"+prob);

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
           // Debug.Log(player.name + " Sono nella zona di Centrocampo->" + prob);
           if (GameCore.current.secCurrent < 3)
           {
               if (Random.value > 0.5)
               {
                   return true;
               }
           }

           if (prob < 10 && player.transform.position.y < -10)
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
           //   Debug.Log(player.name + " Sono nella zona di attacco->" + prob);
           if (GameCore.current.secCurrent < 3)
           {
               if (Random.value > 0.5)
               {
                   return true;
               }
           }
           if (!player.boaFlag && !player.marcaFlag)
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
               return false;
           }
           else if (hitBoa.collider.CompareTag("Rovesciata"))

           {
               if (name == "PlayerR1")
               {
                   return false;
               }
               else if (name == "PlayerR3")
               {
                   return false;
               }
               else if (name == "PlayerR5")
               {
                   return true;
               }
           }
           else if (hitBoa.collider.CompareTag("Sciarpa"))
           {
               if (name == "PlayerR1")
               {
                   return true;
               }
               else if (name == "PlayerR3")
               {
                   return false;
               }
               else if (name == "PlayerR5")
               {
                   return false;
               }
           }
       }
       else if (hitBoa.collider == null)
       {
           if (name == "PlayerR1")
           {
               return true;
           }
           else if (name == "PlayerR3")
           {
               return true;
           }
           else if (name == "PlayerR5")
           {
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
       if (posY > 0) //Palla nella zona di difesa
       {
           return 0;
       }
       else if (0 >= posY && posY >= -12.5)
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

   public bool BoaWatchGk(int idTeam)
   {
       LayerMask mask = 1 << 4;
       RaycastHit2D hitBoa;
       if (idTeam == 1)
       {
           hitBoa = Physics2D.Raycast(GameObject.Find("PlayerR6").transform.parent.transform.position, (Ball.current.transform.position - GameObject.Find("PlayerR6").transform.position).normalized, 20, mask);
       }
       else
       {
           hitBoa = Physics2D.Raycast(GameObject.Find("PlayerY6").transform.parent.transform.position, (Ball.current.transform.position - GameObject.Find("PlayerY6").transform.position).normalized, 20, mask);
       }

       if (hitBoa.collider != null)
       {
           if (hitBoa.collider.CompareTag("ShootLine"))
           {
               return true;
           }
           else { return false; }
       }
       else
       {
           return false;
       }
   }

   public int GetRandom()
   {
       int prob = Random.Range(0, 100);
       // Debug.Log("Valore estratto -> "+prob);
       return prob;
   }
}*/