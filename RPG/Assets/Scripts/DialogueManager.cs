using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{

    public GameObject dialogueBox;
    public Text dialogueText;
    public Text chatText;


    public bool dialogueActive;


    // Start is called before the first frame update
    void Start()
    {
        dialogueActive = false;
        dialogueBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(dialogueActive && Input.GetKeyDown(KeyCode.Space))
        {
            dialogueBox.SetActive(false);
            dialogueActive = false;

        }
    }

    public void ShowDialogue(string dialogue)
    {
        dialogueActive = true;
        dialogueBox.SetActive(true);
        dialogueText.text = dialogue;

    }

    public void ShowText( string text)
    {
        chatText.text = text;
    }
}
