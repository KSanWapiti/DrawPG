using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SlimeController : MonoBehaviour
{
    public float moveSpeed;
    public float timeBtwMove;
    private float timeToMove;
    private float timeCounterMove;

    private bool moving;
    private Rigidbody2D myRigidBody;

    private Vector2 movingDirection;



    private GameObject thePlayer;
    private int enemyHealth;



    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();


    }

    // Update is called once per frame
    void Update()
    {
        

        if (moving)
        {
            timeToMove -= Time.deltaTime;
            myRigidBody.velocity = movingDirection;

            if(timeToMove < 0f)
            {
                timeCounterMove = timeBtwMove;
                moving = false;
            }
        }
        else
        {
            timeCounterMove -= Time.deltaTime;
            myRigidBody.velocity = Vector2.zero;
            if(timeCounterMove < 0f)
            {
                moving = true;
                movingDirection = new Vector2(Random.Range(-1f, 1f) * moveSpeed , Random.Range(-1f,1f) * moveSpeed );
                timeToMove = Random.Range(1f, 4f);
            }
        }



        

    }
    // si l'ennemi se fait toucher, alors stun time
    public void stunEnemy()
    {
        moving = false;
    }


   
}
