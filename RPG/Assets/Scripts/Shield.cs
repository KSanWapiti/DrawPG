using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public GameObject player;
    public GameObject bubble;
    public float time;
    private float duréeBouclier;
    private bool etatBouclier;
    private SpriteRenderer spriteBouclier;
    // Start is called before the first frame update
    void Start()
    {
        duréeBouclier=time;
        bubble.SetActive(false);
        etatBouclier=false;
        spriteBouclier= bubble.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (etatBouclier){
            if(time == 0){
                bubble.SetActive(false);
                etatBouclier=false;
            }else{
                time-=1;
            }
        }else{
            time=duréeBouclier;
        }
    
        bubble.transform.position = player.transform.position;
        
        if (Input.GetKey("1") ){
            if (Input.GetKeyDown(KeyCode.E) ){
                bubble.SetActive(true);
                spriteBouclier.color=new Color(1,1,1,1);
                etatBouclier=true;
                }
        }   
        else if(Input.GetKey("2") ){
            if (Input.GetKeyDown(KeyCode.E) ){
                bubble.SetActive(true);
                spriteBouclier.color=new Color(1,0.5f,0,1);
                etatBouclier=true;
                }
        }
        else if(Input.GetKey("3") ){
            if (Input.GetKeyDown(KeyCode.E) ){
                bubble.SetActive(true);
                spriteBouclier.color=new Color(0,0.7f,1,1);
                etatBouclier=true;
                }
        }
        else if(Input.GetKey("4") ){
            if (Input.GetKeyDown(KeyCode.E) ){
                bubble.SetActive(true);
                spriteBouclier.color=new Color(1,0,0,1);
                etatBouclier=true;
                }
        }
    }
}
