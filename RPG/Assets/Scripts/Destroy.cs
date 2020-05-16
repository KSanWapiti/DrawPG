using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public GameObject player;
    public SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = player.transform.position;
        if (Input.GetKey("1")){
            sprite.color=Color.red;
        }
        if (Input.GetKey("2")){
            sprite.color=new Color(255,255,255);
        }if (Input.GetKey("3")){
            sprite.color=new Color(176, 119, 48);
        }if (Input.GetKey("4")){
            sprite.color=Color.white;
        }
        
    }
}
