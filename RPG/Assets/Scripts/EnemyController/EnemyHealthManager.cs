using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public int enemyHealth;
    private PlayerStats thePlayer;
    public int xpToAdd;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {

        if(enemyHealth <= 0)
        {
            Destroy(gameObject);
            thePlayer.addXp(xpToAdd);
        }
    }

    public void hurtEnemy( int damage)
    {
        enemyHealth -= damage;
    }
}
