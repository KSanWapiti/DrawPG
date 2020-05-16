using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public GameObject bubble;
    public float timer;
    private float a;
    // Start is called before the first frame update
    void Start()
    {
        a=timer;
        bubble.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
            if (Input.GetKeyDown(KeyCode.E)){
                bubble.gameObject.SetActive(true);
                timer=a;
            
            }
        if(timer == 0){
            bubble.gameObject.SetActive(false);
            timer=a;
            }else{
                timer-=1;
            }
        }      
}
