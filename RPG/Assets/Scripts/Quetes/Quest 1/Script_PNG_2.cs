using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_PNG_2 : MonoBehaviour
{
    public bool showGUI,is_ready,is_click1,is_sceau,is_fight = false;
    public GameObject ennemy1_Quest1;
    public GameObject ennemy2_Quest1;
    public GameObject ennemy3_Quest1;
    public int killed_mobs;





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
        if (showGUI && is_ready)
        {
            if (is_click1 == false)
            {
                GUI.BeginGroup(new Rect(Screen.width / 2 - 150, Screen.height / 2 + 15, 300, 145));
                GUI.Box(new Rect(0, 0, 300, 145), "Mage");
                GUI.Label(new Rect(20, 30, 250, 120), "Aiofura..  Hé, tu ne vois pas que tu me déranges ? N'es-tu pas au courant du danger que représente ce portail magique?");
                if (GUI.Button(new Rect(50, 90, 200, 40), "Annoncer que vous êtes" + '\n' + "envoyé par le roi"))
                {
                    is_click1 = true;
                }
            }
            if (is_click1 == true)
            {
                if (is_sceau == false)
                {
                    GUI.BeginGroup(new Rect(Screen.width / 2 - 150, Screen.height / 2 + 15, 300, 145));
                    GUI.Box(new Rect(0, 0, 300, 145), "Mage");
                    GUI.Label(new Rect(20, 30, 250, 120), "Mmh, un être si vulnérable envoyé par le roi pour combattre la menace  ? Quel drôle de mensonge !");
                    if (GUI.Button(new Rect(45, 85, 210, 50), "S'en aller en grognant" + '\n' + "et retourner voir le messager"))
                    {
                        GameObject.FindObjectOfType<Script_PNG_un_bis>().enabled = true;
                        GameObject.FindObjectOfType<Script_PNG_un_bis>().is_mage = true;
                        GameObject.FindObjectOfType<Script_PNG_2>().enabled = false;


                    }
                }
                if (is_sceau == true)
                {
                    if (is_fight == false)
                    {
                        GUI.BeginGroup(new Rect(Screen.width / 2 - 150, Screen.height / 2 + 15, 300, 145));
                        GUI.Box(new Rect(0, 0, 300, 145), "Mage");
                        GUI.Label(new Rect(20, 30, 250, 120), "Examine le sceau .. Quoi! Tu possèdes le sceau du roi ?! Excuse ma méprise, de notre temps, il n'est pas rare de voir des imposteurs !");
                        if (GUI.Button(new Rect(45, 100, 210, 30), "Lui demander comment" + '\n' + "combattre les ennemis"))
                        {
                            is_fight = true;
                        }
                    }
                    if(is_fight==true)
                    {
                        if (killed_mobs < 3)
                        {
                            GUI.BeginGroup(new Rect(Screen.width / 2 - 150, Screen.height / 2 + 15, 300, 120));
                            GUI.Box(new Rect(0, 0, 300, 120), "Mage");
                            GUI.Label(new Rect(20, 30, 250, 110), "Quelle intrépidité ! Pour lancer un sort, réalise une figure (ligne, carré, cercle) en cliquant sur l'écran avec ta souris!");
                            if (GUI.Button(new Rect(45, 85, 210, 25), "Faire apparaître 3 créatures maléfiques"))
                            {
                                ennemy1_Quest1.SetActive(true);
                                ennemy2_Quest1.SetActive(true);
                                ennemy3_Quest1.SetActive(true);
                            }
                        }
                        if (killed_mobs>=3)
                        {
                            GUI.BeginGroup(new Rect(Screen.width / 2 - 150, Screen.height / 2 + 15, 300, 120));
                            GUI.Box(new Rect(0, 0, 300, 120), "Mage");
                            GUI.Label(new Rect(20, 30, 250, 110), "Bien joué!");
                        }

                    }


                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}