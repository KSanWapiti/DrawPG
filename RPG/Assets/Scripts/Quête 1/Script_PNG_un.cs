using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Script_PNG_2;

public class Script_PNG_un : MonoBehaviour
{
    public bool showGUI = false;
    public bool is_click = false;
    public GameObject PNG_1;
    public GameObject PNG_1_bis;

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
            if (is_click == false)
            {
                LabelWrite("Messager du roi", "Bonjour jeune Padawan! Un mystérieux portail est apparu cette nuit dans la forêt de Vuichen. Des créatures magiques commencent à envahir notre monde. Notre monde a besoin de toi, mais tout d'abord, il faut te former!", true);

                if (GUI.Button(new Rect(100, 140, 100,20 ), "Me former"))
                {
                    is_click = true;
                }
            }
            else
            {
                LabelWrite("Messager du roi", "Ta détermination semble être au rendez-vous ! Retrouve moi pres du chemin pour débuter ton apprentissage",false);
                if (GUI.Button(new Rect(55, 87, 190, 20), "Le retrouver pres du chemin"))
                {
                    GameObject.FindObjectOfType<Script_PNG_un>().enabled = false;
                    PNG_1_bis.SetActive(true);
                    PNG_1.SetActive(false);
                    
                }

            }
        }
    }

    void LabelWrite(string nom_PNG,string message_PNG,bool is_Button)
    {
        if (is_Button)
        {
            GUI.BeginGroup(new Rect(Screen.width / 2 - 150, Screen.height / 2 + 15, 300, 180));
            GUI.Box(new Rect(0, 0, 300, 180), nom_PNG);
            GUI.Label(new Rect(20, 30, 250, 180), message_PNG);
        }
        else
        {
            GUI.BeginGroup(new Rect(Screen.width / 2 - 150, Screen.height / 2 + 15, 300, 120));
            GUI.Box(new Rect(0, 0, 300, 120), nom_PNG);
            GUI.Label(new Rect(20, 30, 250, 120), message_PNG);

        }

    }
    // Update is called once per frame
    void Update()
    {
    }
}
