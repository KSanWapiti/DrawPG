using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouvementjeanmichel : MonoBehaviour
{
    public float timeCounter;
    public float moveSpeed;
    private Rigidbody2D rigidbody;
    private Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        direction = new Vector2(1*moveSpeed, 1*moveSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        timeCounter = timeCounter - Time.deltaTime;
        if (timeCounter > 0)
        {
        rigidbody.velocity = direction;
        }
        else 
        { rigidbody.velocity = new Vector2(0, 0); }
    }
}
