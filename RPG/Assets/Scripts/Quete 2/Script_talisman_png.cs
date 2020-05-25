using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_talisman_png : MonoBehaviour
{
    public bool showGUI=false;
    public GameObject talisman;
    public bool is_talisman;
    void Start()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            showGUI = true;
        }

    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            showGUI = false;
        }

    }

    void OnGUI()
    {
        if (showGUI)
        {
            if (is_talisman == false)
            {
                GUI.BeginGroup(new Rect(Screen.width / 2 - 150, Screen.height / 2 + 15, 300, 145));
                GUI.Box(new Rect(0, 0, 300, 145), "Bijoutière étourdie");
                GUI.Label(new Rect(20, 30, 250, 120), "(Cherche son talisman) Impossible de retrouver ce satané talisman ... Peut-être l'aurais-je égarer lors de ma balade dans la forêt ?");
                if (GUI.Button(new Rect(60, 95, 180, 40), "Lui promettre que vous allez" + "\n" + "jeter un oeil"))
                {
                    talisman.SetActive(true);
                    GameObject.FindObjectOfType<Script_talisman_png>().enabled = false;

                }
            }
            if(is_talisman==true)
            {
                GUI.BeginGroup(new Rect(Screen.width / 2 - 150, Screen.height / 2 + 15, 300, 145));
                GUI.Box(new Rect(0, 0, 300, 100), "Bijoutière étourdie");
                GUI.Label(new Rect(20, 30, 250, 90), "Mon précieux talisman! Tu l'as retrouvé!! Merci beaucoup, je ferai plus attention à l'avenir.");

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
