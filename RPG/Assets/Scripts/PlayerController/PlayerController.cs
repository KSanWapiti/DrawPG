using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.ObjectModel;


public class PlayerController : MonoBehaviour
{
    public float walkSpeed;
    private float walkSpeedDiag;
    public float diviseurDiagMouvement;

    private bool moving;


    private Animator anim;
    public Vector2 lastMove;

    private Rigidbody2D myRigidBody;

    private static bool playerExists;
    Collection<timer> timers = new Collection<timer>();
    

    public float attackSpeed;
    private float attackSpeedCounter;

    private float hurtEffectTimeCounter;
    public float hurtEffectTime;

    private SpriteRenderer spriteRenderer;

    private SpellController spellcontroller;
    public GameObject bouledefeu;
    public GameObject fireballPurple;
    public GameObject bouledeterre;
    public GameObject bouledeeau;
    public GameObject bouledevent;
    Collection<string> nomSorts = new Collection<string>();
    Collection<GameObject> sorts = new Collection<GameObject>();
    /*Collection<sort> sorts = new Collection<sort>();
    public sort fireball=new sort(bouledefeu,"bouledefeu");
    public sort fireballP = new sort(fireballPurple,"fireballPurple");
    public sort Rockball=new sort(bouledeterre,"bouledeterre");
    public sort Waterball=new sort(bouledeeau,"bouledeeau");
    public sort Windball=new sort(bouledevent,"bouledevent");*/
    //on récupère les formes dessinées
    private Mouvement dessin;
    private string élément;
    private string SortLancé;
    private string timerSortLancé;
    timer timerBoule = new timer("timerboule", 0);
    timer timerMur = new timer("timermur", 0);
    timer timerDefense = new timer("timerdefense", 0);
   

    string forme;
    string formeActuelle;
    //To show text in chat
    private DialogueManager text;
    private int z;
    /*public class sort
    {
        public GameObject Go;
        public string nom;
        public sort(GameObject game, string n)
        {
            this.Go = game;
            this.nom = n;
        }
    }*/
    public int sortAssocié(string SL)
    {
        foreach (string S in nomSorts)
        {
            if (S == SL)
            {
                return nomSorts.IndexOf(S);
            }
        }
        return 4;
    }
    class timer
    {
        string nom;
        int temps;
        public timer(string n,int t)
        {
            this.nom = n;
            this.temps = t;
        }
        public bool ifNom(string n)
        {
            if (this.nom == n)
            {
                return true;
            }
            return false;
        }
        public bool ifTemps()
        {
            if (this.temps == 0)
            {
                return true;
            }
            return false;
        }
        public void decompte()
        {
            if (this.temps != 0)
            {
                temps = temps - 1;
            }
        }
        public void setTimer(int t)
        {
            this.temps = t;
        }
        public void display()
        {
            print("nom: "+this.nom + " et timer: " + this.temps);
        }
    }

    private bool verifSort(string f)
    {
        foreach (timer chargement in timers)
        {
            if (chargement.ifNom(f))
            {
                if (chargement.ifTemps())
                {
                    chargement.setTimer(300);
                    return true;
                }
            }
        }
        return false;
    }
    // Start is called before the first frame update
    void Start()
    {
        sorts.Add(bouledefeu);sorts.Add(bouledeeau);sorts.Add(bouledeterre);sorts.Add(bouledevent); sorts.Add(fireballPurple);
        nomSorts.Add("bouledefeu"); nomSorts.Add("bouledeeau"); nomSorts.Add("bouledeterre"); nomSorts.Add("bouledevent");nomSorts.Add("fireballPurple");
        timers.Add(timerBoule);timers.Add(timerMur);timers.Add(timerDefense);
        z = 0;
        spellcontroller = FindObjectOfType<SpellController>();
        dessin = FindObjectOfType<Mouvement>();
        if (!playerExists)
        {
            playerExists = true;
            DontDestroyOnLoad(transform.gameObject);
        } else
        {
            Destroy(gameObject);


        }
        élément = "feu";
        walkSpeedDiag = walkSpeed * diviseurDiagMouvement;
        spriteRenderer = GetComponent<SpriteRenderer>();
        forme = dessin.dessinRenvoyé;
        formeActuelle = forme;
    }

    // Update is called once per frame
    void Update()

    {
        anim = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();

        
        moving = false;
        
        if (attackSpeedCounter > 0)
        {
            attackSpeedCounter -= Time.deltaTime;
            if (attackSpeedCounter <= 0)
            {
                anim.SetBool("Attacking", false);
            }
            //attackState = false;
        }

        // Move Controller

        if(Mathf.Abs(Input.GetAxisRaw("Horizontal")) >0.5f)
        {
            //transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * walkSpeed * Time.deltaTime,0f,0f));
            myRigidBody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * walkSpeed, myRigidBody.velocity.y);
            lastMove = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
            moving = true;
            
        }

        if (Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0.5f)
        {
            //transform.Translate(new Vector3(0f,Input.GetAxisRaw("Vertical") * walkSpeed * Time.deltaTime, 0f));
            myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, Input.GetAxisRaw("Vertical") * walkSpeed);
            moving = true;
            lastMove = new Vector2(0f, Input.GetAxisRaw("Vertical"));
            
        }

        if(Mathf.Abs(Input.GetAxisRaw("Horizontal")) < 0.5f)
        {
            myRigidBody.velocity = new Vector2(0f, myRigidBody.velocity.y);
        }

        if (Mathf.Abs(Input.GetAxisRaw("Vertical")) < 0.5f)
        {
            myRigidBody.velocity = new Vector2(myRigidBody.velocity.x , 0f);
        }

        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.5f && Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0.5f)
        {
            if (walkSpeed == walkSpeedDiag)
            {

            }
            else
            {
                walkSpeed = walkSpeedDiag;
            }
        }

        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) < 0.5f || Mathf.Abs(Input.GetAxisRaw("Vertical")) < 0.5f)
        {
            walkSpeed = walkSpeedDiag * (1 / diviseurDiagMouvement);
        }

        anim.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
        anim.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
        anim.SetBool("Moving", moving);
        anim.SetFloat("PositionX", lastMove.x);
        anim.SetFloat("PositionY", lastMove.y);

        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            élément = "feu";
        }
        
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            élément = "eau";
        }
        
        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            élément = "vent";
        }

        if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            élément = "terre";
        }

        // Attack Controller
        //Basic Attack



        if (Input.GetKeyUp(KeyCode.J))
        {
            //attackState = true;
            attackSpeedCounter = attackSpeed;
            anim.SetBool("Attacking", true);
            
        }
        //try { forme = dessin.dessin(); } catch (System.Exception e) { print(e); }
        //Spell 1 :

        forme = dessin.dessin();
        //print("forme récupérée");
        text = FindObjectOfType<DialogueManager>();

        foreach (timer chargement in timers)
        {
            chargement.decompte();
        }



        timerSortLancé = "timer" + forme;
        //print("timerSortLancé =" + timerSortLancé);
        //print(verifSort(timerSortLancé));
        //timerBoule.display();
        //timerMur.display();
        

        if (verifSort(timerSortLancé)) //si le sort se lance, le temps de chargement de 5 secondes est automatiquement lancé
            {
            SortLancé = forme + "de" + élément;
                float z = 0f;

                if (lastMove.x > 0.5f)
                {
                    z = 180f;
                }

                if (lastMove.x < -0.5f)
                {
                    z = 0f;
                }

                if (Mathf.Abs(lastMove.y) > 0.5f)
                {
                    z = lastMove.y * (-90f);
                }

                Vector3 EulerRotation = new Vector3(0f, 0f, z);
                //print(sortAssocié(SortLancé).ToString());
                Instantiate(sorts[sortAssocié(SortLancé)], myRigidBody.position, Quaternion.Euler(EulerRotation));
            forme = "Mauvaise forme";

            }
        /*
        if (forme == "mur" && timerMur==0)
            {
                
                float z = 0f;

                if (lastMove.x > 0.5f)
                {
                    z = 180f;
                }

                if (lastMove.x < -0.5f)
                {
                    z = 0f;
                }

                if (Mathf.Abs(lastMove.y) > 0.5f)
                {
                    z = lastMove.y * (-90f);
                }

                Vector3 EulerRotation = new Vector3(0f, 0f, z);
                Instantiate(fireball, myRigidBody.position, Quaternion.Euler(EulerRotation));
            timerMur = 300;
            forme = "Mauvaise forme";

            }

            //Spell 2 :


        if (forme == "boule" && timerBoule==0)
            {
                float z = 0f;

                if (lastMove.x > 0.5f)
                {
                    z = 180f;
                }

                if (lastMove.x < -0.5f)
                {
                    z = 0f;
                }

                if (Mathf.Abs(lastMove.y) > 0.5f)
                {
                    z = lastMove.y * (-90f);
                }

                Vector3 EulerRotation = new Vector3(0f, 0f, z);
                Instantiate(fireballPurple, myRigidBody.position, Quaternion.Euler(EulerRotation));
            timerBoule = 300;
            forme = "Mauvaise forme";
            }

        //Spell rock :

        if (forme == "boule" && timerBoule == 0 || Input.GetKeyUp(KeyCode.K))
        {
            float z = 0f;

            if (lastMove.x > 0.5f)
            {
                z = 180f;
            }

            if (lastMove.x < -0.5f)
            {
                z = 0f;
            }

            if (Mathf.Abs(lastMove.y) > 0.5f)
            {
                z = lastMove.y * (-90f);
            }

            Vector3 EulerRotation = new Vector3(0f, 0f, z);
            Instantiate(fireballRock, myRigidBody.position, Quaternion.Euler(EulerRotation));
            timerBoule = 300;
            forme = "Mauvaise forme";
        }

        //Spell water :

        if (forme == "boule" && timerBoule == 0 || Input.GetKeyUp(KeyCode.L))
        {
            float z = 0f;

            if (lastMove.x > 0.5f)
            {
                z = 180f;
            }

            if (lastMove.x < -0.5f)
            {
                z = 0f;
            }

            if (Mathf.Abs(lastMove.y) > 0.5f)
            {
                z = lastMove.y * (-90f);
            }

            Vector3 EulerRotation = new Vector3(0f, 0f, z);
            Instantiate(fireballWater, myRigidBody.position, Quaternion.Euler(EulerRotation));
            timerBoule = 300;
            forme = "Mauvaise forme";
        }

        //Spell air :

        if (forme == "boule" && timerBoule == 0 || Input.GetKeyUp(KeyCode.M))
        {
            float z = 0f;

            if (lastMove.x > 0.5f)
            {
                z = 180f;
            }

            if (lastMove.x < -0.5f)
            {
                z = 0f;
            }

            if (Mathf.Abs(lastMove.y) > 0.5f)
            {
                z = lastMove.y * (-90f);
            }

            Vector3 EulerRotation = new Vector3(0f, 0f, z);
            Instantiate(fireballAir, myRigidBody.position, Quaternion.Euler(EulerRotation));
            timerBoule = 300;
            forme = "Mauvaise forme";
        }




        if (hurtEffectTimeCounter > 0.66f * hurtEffectTime )
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0f);
        } else if (hurtEffectTimeCounter > 0.33f * hurtEffectTime)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
        } else if (hurtEffectTimeCounter > 0f )
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0f);
        } else
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
        }


        hurtEffectTimeCounter -= Time.deltaTime;

    }
            }
        */

        
        


        if (hurtEffectTimeCounter > 0.66f * hurtEffectTime )
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0f);
        } else if (hurtEffectTimeCounter > 0.33f * hurtEffectTime)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
        } else if (hurtEffectTimeCounter > 0f )
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0f);
        } else
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
        }


        hurtEffectTimeCounter -= Time.deltaTime;

    }

    public void playerHurtEffect()
    {
        hurtEffectTimeCounter = hurtEffectTime;
    }
}
