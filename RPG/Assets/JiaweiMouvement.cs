using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JiaweiMouvement : MonoBehaviour
{
    public float moveSpeed;
    private Rigidbody2D rigid;
    private Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        direction = new Vector2(1 * moveSpeed, 1 * moveSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        rigid.velocity = direction;
    }
}
