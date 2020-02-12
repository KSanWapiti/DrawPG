using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PNGMovementController : MonoBehaviour
{
    private Rigidbody2D playerBody;
    public float moveSpeed;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        playerBody.velocity = new Vector2(moveSpeed, 0);
        animator.SetBool("Moving", false);
        animator.SetFloat("PositionX", 0f);
        animator.SetFloat("PositionY", -1f);
    }
}
