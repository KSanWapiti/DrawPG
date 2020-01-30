using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PLayerHealthController : MonoBehaviour
{
    public int playerCurrentHealth;
    public int playerMaxHealth;
    public float respawnTime;

    

    // Start is called before the first frame update
    void Start()
    {
        SetMaxHealth();
        
    }

    // Update is called once per frame
    void Update()
    {
        if( playerCurrentHealth <= 0)
        {
            //Destroy(gameObject);
            gameObject.SetActive(false);
            
            //gameObject.GetComponent<SlimeController>().getReloading();

            respawnTime -= Time.deltaTime;
            if(respawnTime <= 0)
            {
                SetMaxHealth();
                gameObject.SetActive(true);
                
                Application.LoadLevel(Application.loadedLevel);
            }
            //Application.LoadLevel(Application.loadedLevel);
            //SceneManager.LoadScene("OpenMap");
        }
    }

    public void HurtPlayer(int damageTaken)
    {
        playerCurrentHealth -= damageTaken;
    }

    public void SetMaxHealth()
    {
        playerCurrentHealth = playerMaxHealth;
    }

}
