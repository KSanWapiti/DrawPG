using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int currentXp;
    public int currentLvl;
    public int[] xpArray;
    public GameObject lvlEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentXp >= xpArray[currentLvl])
        {
            currentLvl += 1;
            Instantiate(lvlEffect, transform.position, transform.rotation);

        }
    }

    public void addXp(int xpToAdd)
    {
        currentXp += xpToAdd;
        
    }
}
