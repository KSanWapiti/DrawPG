using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellController : MonoBehaviour
{
    public float moveSpeed;
    private new Rigidbody2D rigidbody;
    private float direction;

    private DialogueManager text;

    // Start is called before the first frame update
    void Start()
    {
        text = FindObjectOfType<DialogueManager>();

        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        direction = transform.rotation.z;
        text.ShowText(" direction = " + direction.ToString() + " transform.rotation.z = " + transform.rotation.z.ToString());

        if (-0.02f < direction & direction < 0.02f)
        {
            rigidbody.velocity = new Vector2(-moveSpeed, 0);
        }
        else if( -0.8f < direction & direction < -0.6f)
        {
            rigidbody.velocity = new Vector2(0,moveSpeed);
        }
        else if ( -1.1f < direction & direction < -0.9f)
        {
            rigidbody.velocity = new Vector2(moveSpeed, 0);
        }
        else if ( 0.6f < direction & direction < 0.8f)
        {
            rigidbody.velocity = new Vector2(0, -moveSpeed);
        }
        /*
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
        */

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }

        
    }
}
