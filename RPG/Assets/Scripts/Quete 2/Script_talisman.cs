using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_talisman : MonoBehaviour
{
    bool showGUI = false;
    public GameObject talisman;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(showGUI)
        {
            talisman.SetActive(false);
            GameObject.FindObjectOfType<Script_talisman_png>().is_talisman = true;
            GameObject.FindObjectOfType<Script_talisman_png>().enabled = true;

        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            showGUI = true;
        }

    }
}
