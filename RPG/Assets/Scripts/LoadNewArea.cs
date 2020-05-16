using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewArea : MonoBehaviour { 

    public string nameArea;
    public string ExitPoint;
    private PlayerController thePlayer;
    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            //Application.LoadLevel(nameArea);
            SceneManager.LoadScene(nameArea);
            thePlayer.StartPoint=ExitPoint;
        }
    }
}

