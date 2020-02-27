using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Antoine_movement : MonoBehaviour
{
    public float moveSpeed;
    private Rigidbody2D rigi;
    private Vector2 dir;
    // Start is called before the first frame update
    void Start()
    {
        rigi = GetComponent<Rigidbody2D>();
        dir = new Vector2(moveSpeed*2,moveSpeed*0);
    }

    // Update is called once per frame
    void Update()
    {
        rigi.velocity = dir;

        
    }
}
