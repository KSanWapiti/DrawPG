using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.ObjectModel;


public class PlayerController : MonoBehaviour
{
    public float walkSpeed;
    private float walkSpeedDiag;
    public float diviseurDiagMouvement;
    public string StartPoint;
    private bool moving;


    private Animator anim;
    public Vector2 lastMove;

    private Rigidbody2D myRigidBody;

    private static bool playerExists;
    

    public float attackSpeed;
    private float attackSpeedCounter;

    private float hurtEffectTimeCounter;
    public float hurtEffectTime;

    private SpriteRenderer spriteRenderer;

    
    string forme; //variable qui désigne la forme dessinée 

    // on initialise maintenant les différents sorts disponibles :
    private SpellController spellcontroller;
    public GameObject bouledefeu;
    public GameObject bouledeterre;
    public GameObject bouledeeau;
    public GameObject bouledevent;
    public GameObject None;

    // on crée deux collections qui nous permettront de simplifier le programme par la suite
    Collection<string> nomSorts = new Collection<string>(); //contient les noms des sorts utilisables
    Collection<GameObject> sorts = new Collection<GameObject>();//contient les GameObjects des sorts utilisables
    
    //on récupère les formes dessinées
    private Mouvement dessin; //on fait appel au script mouvement qui permet la reconnaissance des formes
    private string élément; //ce string permet de suivre l'élément utilisé
    private string SortLancé; //permet d'avoir le nom du sort lancé
    private string timerSortLancé; //traduit l'état du temps de chargement du sort qui est en train d'être lancé
    
    // on initialise les timer des 3 formes utilisables :
    timer timerBoule = new timer("timerboule", 0); 
    timer timerbouclier = new timer("timerbouclier", 0);
    timer timerDefense = new timer("timerdefense", 0);

    
    Collection<timer> timers = new Collection<timer>(); //permet de garder une trace de tous les timers utilisés, et de les gérer en même temps


    //on initialise les variables nécessaires au fonctionnement du sort bouclier
    public GameObject player;
    public GameObject bubble;
    public float time; //variable décrivant la durée du bouclier
    private float duréeBouclier; //variable fixe qui contient la durée prédéfinie du sort 
    private bool etatBouclier; //état du bouclier, actif correspondant à True et inactif correspondant à False
    private SpriteRenderer spriteBouclier;



    //To show text in chat
    private DialogueManager text;
    private int z;

    public int sortAssocié(string SL) //à partir du nom d'un sort donné en entrée, renvoie le GameObject du sort associé. 
    {
        foreach (string S in nomSorts)
        {
            if (S == SL)
            {
                return nomSorts.IndexOf(S);
            }
        }
        return 4; //on renvoie le rang correspondant à None, pour n'envoyer aucun sort en cas d'erreur, ou de sort introuvable.
    }
    class timer // cette classe sert à mesurer les temps de chargements des formes utilisées
    {
        string nom; 
        int temps;
        public timer(string n,int t) // la classe permet d'associer un temps entier à une forme donnée
        {
            this.nom = n;
            this.temps = t;
        }
        public bool ifNom(string n) // vérife si la forme est bien celle donnée par "n"
        {
            if (this.nom == n)
            {
                return true;
            }
            return false;
        }
        public bool ifTemps() // vérifie si le temps de chargement est nul ou pas
        {
            if (this.temps == 0)
            {
                return true;
            }
            return false;
        }
        public void decompte() // met à jour le temps de chargement
        {
            if (this.temps != 0)
            {
                temps = temps - 1;
            }
        }
        public void setTimer(int t) // initialise le temps de chargement à une certaine valeur
        {
            this.temps = t;
        }
        public void display() // affiche le nom de la forme et son temps de chargment restant (en nombre d'images restantes)
        {
            print("nom: "+this.nom + " et timer: " + this.temps);
        }
    }



    private bool verifSort(string f) //vérifie si une forme est utilisable ou pas en fonction de son temps de chargement
    {
        foreach (timer chargement in timers)
        {
            if (chargement.ifNom(f))
            {
                if (chargement.ifTemps())
                {
                    chargement.setTimer(300); //si le sort est bien lançable, initialise son temps de chargement à 300 images, ce qui correspond à 5 secondes puisque le jeu tourne à 60 images/seconde
                    return true;
                }
            }
        }
        return false;
    }


    // Start is called before the first frame update
    void Start()
    {
        sorts.Add(bouledefeu);sorts.Add(bouledeeau);sorts.Add(bouledeterre);sorts.Add(bouledevent);  sorts.Add(None); //on remplit la liste de GameObjects des sorts utilisables
        nomSorts.Add("bouledefeu"); nomSorts.Add("bouledeeau"); nomSorts.Add("bouledeterre"); nomSorts.Add("bouledevent");nomSorts.Add("None"); // on remplit la liste des noms des sorts utilsables
        timers.Add(timerBoule);timers.Add(timerbouclier);timers.Add(timerDefense); //on remplit la liste des timers avec les timers des différentes formes
        z = 0;
        spellcontroller = FindObjectOfType<SpellController>();
        dessin = FindObjectOfType<Mouvement>(); // on lance le script Mouvement
        if (!playerExists)
        {
            playerExists = true;
            DontDestroyOnLoad(transform.gameObject);
        } else
        {
            Destroy(gameObject);
        }
        élément = "feu"; // on initialise l'élément utilisé à feu, le choix est arbitraire
        walkSpeedDiag = walkSpeed * diviseurDiagMouvement;
        spriteRenderer = GetComponent<SpriteRenderer>();
        forme = dessin.dessinRenvoyé; //on récupère la variable "dessinRenvoyé" du script Mouvement
        
        duréeBouclier = time; //on initialise la variable duréeBouclier
        bubble.SetActive(false); //le bouclier est initialement désactivé
        etatBouclier = false; //la variable traduisant l'etat du bouclier est initialisée sur false pour signifier qu'il est désactivé
        spriteBouclier = bubble.GetComponent<SpriteRenderer>(); //on récupère le sprite du bouclier
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


        // les éléments feu, eau, vent et terre sont activables respectivement avec les touches 1,2,3 et 4 :
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
        if (Input.GetMouseButtonUp(1)) //le bouton droit de la souris permet d'effectuer des attaques à l'épée
        {
            //attackState = true;
            attackSpeedCounter = attackSpeed;
            anim.SetBool("Attacking", true);
        }


        text = FindObjectOfType<DialogueManager>();


        //try { forme = dessin.dessin(); } catch (System.Exception e) { print(e); }
        //Spell 1 :

        forme = dessin.dessin(); //on met à jour la variable "forme" en récupérant le résultat du script Mouvement via sa fonction dessin()
        //print("forme récupérée");


        // on met à jour les timers de toutes les formes :
        foreach (timer chargement in timers)
        {
            chargement.decompte();
        }


        //on récupère le nom du timer de la forme qui a été dessinée 
        timerSortLancé = "timer" + forme;
        //print("timerSortLancé =" + timerSortLancé);
        //print(verifSort(timerSortLancé));
        if (verifSort(timerSortLancé)) //si le sort se lance, le temps de chargement de 5 secondes est automatiquement lancé
            {
            SortLancé = forme + "de" + élément; //on récupère le nom du sort à lancer
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
                if (forme == "bouclier")// si jamais la forme lancée est le bouclier, il faut l'activer en rendant l'état true et changer sa couleur en l'élément correspondant
                {
                    if (élément=="vent")
                        {
                            bubble.SetActive(true);
                            spriteBouclier.color = new Color(1, 1, 1, 1);
                            etatBouclier = true;
                        }
                    if (élément == "terre")
                        {
                            bubble.SetActive(true);
                            spriteBouclier.color = new Color(1, 0.5f, 0, 1);
                            etatBouclier = true;
                        }
                    if (élément == "eau")
                        {
                            bubble.SetActive(true);
                            spriteBouclier.color = new Color(0, 0.7f, 1, 1);
                            etatBouclier = true;
                        }
                    if (élément == "feu")
                        {
                            bubble.SetActive(true);
                            spriteBouclier.color = new Color(1, 0, 0, 1);
                            etatBouclier = true;
                        }
                forme = "Mauvaise forme";
            }
                Vector3 EulerRotation = new Vector3(0f, 0f, z);
                //print(sortAssocié(SortLancé).ToString());
                Instantiate(sorts[sortAssocié(SortLancé)], myRigidBody.position, Quaternion.Euler(EulerRotation));
                //Le sort est lancé, en récupérant son GameObject grâce à la correspondance des deux collections sorts et nomSorts 
            forme = "Mauvaise forme"; //on réinitialise la variable forme à son état initial
            }

        bubble.transform.position = player.transform.position; //la position du bouclier est mise à jour sur la position du personnage joueur, pour qu'il le suive

        //si le bouclier est toujours actif, on vérifie si il faut le désactiver en regardant son temps d'activité restant
        if (etatBouclier)
        {
            if (time == 0)
            {
                bubble.SetActive(false);
                etatBouclier = false;
            }
            else
            {
                time -= 1;
            }
        }
        else
        {
            time = duréeBouclier;
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

    public void playerHurtEffect()
    {
        hurtEffectTimeCounter = hurtEffectTime;
    }
}
