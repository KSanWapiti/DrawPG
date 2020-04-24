using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
    private int timerBoule;
    private int timerMur;

    public float attackSpeed;
    private float attackSpeedCounter;

    private float hurtEffectTimeCounter;
    public float hurtEffectTime;

    private SpriteRenderer spriteRenderer;

    private SpellController spellcontroller;
    public GameObject fireball;
    public GameObject fireballPurple;
    
    //on récupère les formes dessinées
    private Mouvement dessin;
    string forme;
    string formeActuelle;
    //To show text in chat
    private DialogueManager text;
    private int z;
    public int decompte(int t)
    {
        if (t != 0)
        {
            t = t - 1;
            return (t);
        }
        return (t);
    }
    // Start is called before the first frame update
    void Start()
    {
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

        walkSpeedDiag = walkSpeed * diviseurDiagMouvement;
        timerBoule = 0;
        timerMur = 0;
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

        timerMur=decompte(timerMur);
        timerBoule=decompte(timerBoule);
        //print("timerMur =" + timerMur);
        //print("timerBoule =" + timerBoule);
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
