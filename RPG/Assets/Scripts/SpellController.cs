using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellController : MonoBehaviour
{
    public float moveSpeed;
    private Rigidbody2D rigidbody;
    private float direction;

    // Start is called before the first frame update
    void Start()
    {

        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        switch (direction)
        {
            case 180f:
                rigidbody.velocity = new Vector2(-moveSpeed, 0);
                break;
            case 0f:
                rigidbody.velocity = new Vector2(moveSpeed, 0);
                break;
            case 90f:
                rigidbody.velocity = new Vector2(0, moveSpeed);
                break;
            case -90f:
                rigidbody.velocity = new Vector2(0, -moveSpeed);
                break;
            default:
                rigidbody.velocity = new Vector2(0, 0);
                break;
        }

    }

    public void castSpell(float z) => direction = z;


    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {

        }
        
    }
}
