using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_PNG_un_bis : MonoBehaviour
{
    public bool showGUI = false;
    Script_PNG_2 script_png_2;
    public bool is_mage = false;

    // Start is called before the first frame update
    void Start()
    {
        script_png_2 = GameObject.FindObjectOfType<Script_PNG_2>();
        
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
            if (is_mage == false)
            {
                GUI.BeginGroup(new Rect(Screen.width / 2 - 150, Screen.height / 2 + 15, 300, 145));
                GUI.Box(new Rect(0, 0, 300, 145), "Messager du roi");
                GUI.Label(new Rect(20, 30, 250, 120), "BRRH, il fait froid, tu ne trouves pas ? Pour la 1ère étape de ton initiation, va voir le mage, peut-être qu'il t'en apprendra plus sur la situation. ");
                if (GUI.Button(new Rect(60, 105, 180, 20), "Partir à la rencontre du mage"))
                {
                    script_png_2.is_ready = true;

                    GameObject.FindObjectOfType<Script_PNG_un_bis>().enabled = false;

                }
            }
            if (is_mage==true)
            {
                
                GUI.BeginGroup(new Rect(Screen.width / 2 - 150, Screen.height / 2 + 15, 300, 120));
                GUI.Box(new Rect(0, 0, 300, 120), "Messager du roi");
                GUI.Label(new Rect(20, 30, 250, 110), "Ce bougre de mage ne veut te donner du crédit malgré l'ordre du roi ? Montre lui cet objet et il t'accordera sa confiance.");
                if (GUI.Button(new Rect(60, 85, 180, 20), "Prendre le sceau du roi")) {
                    GameObject.FindObjectOfType<Script_PNG_2>().enabled = true;
                    script_png_2.is_sceau = true;
                    GameObject.FindObjectOfType<Script_PNG_un_bis>().enabled = false;

                }
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
