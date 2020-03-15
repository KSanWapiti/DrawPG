using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JiaweiMouvement : MonoBehaviour
{
    public float moveSpeed;
    private Rigidbody2D rigid;
    private Vector2 direction;
    public float timeCounter;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        direction = new Vector2(1 * moveSpeed, 1 * moveSpeed);

    }

    // Update is called once per frame
    void Update()
    {
        timeCounter = timeCounter - Time.deltaTime;
        if (timeCounter > 0)
        {
            rigid.velocity = direction;
        }
        else
        {
            rigid.velocity = new Vector2(0, 0);
        }
    }
}
