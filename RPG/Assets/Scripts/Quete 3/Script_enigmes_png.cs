using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_enigmes_png : MonoBehaviour
{
    public bool showGUI;
    public GameObject player;
    bool isAnswered = false;
    void Start()
    {
        
    }

    void OnGUI()
    {
        if (showGUI)
        {
            if (isAnswered==false)
            {
                GUI.BeginGroup(new Rect(Screen.width / 2 - 150, Screen.height / 2 + 15, 300, 185));
                GUI.Box(new Rect(0, 0, 300, 185), "Vieillard énigmatique");
                GUI.Label(new Rect(20, 30, 250, 120), "Combien y-a-il de planètes dans le système solaire ?  ");
                if (GUI.Button(new Rect(60, 70, 180, 30), "9"))
                {
                    player.transform.position = new Vector3(-152, 76, 0);

                }
                if (GUI.Button(new Rect(60, 105, 180, 30), "8"))
                {
                    isAnswered = true;
                }
                if (GUI.Button(new Rect(60, 140, 180, 30), "10")) {
                    player.transform.position = new Vector3(-154, 86, 0);
                }
            }
            if(isAnswered)
			{
                GUI.BeginGroup(new Rect(Screen.width / 2 - 150, Screen.height / 2 + 15, 300, 100));
                GUI.Box(new Rect(0, 0, 300, 185), "Vieillard énigmatique");
                GUI.Label(new Rect(20, 30, 250, 120), "Bravo! Tu sembles être bien parti pour devenir mon digne héritier ! ");

            }
        
        }
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

    // Update is called once per frame
    void Update()
    {

    }
}
