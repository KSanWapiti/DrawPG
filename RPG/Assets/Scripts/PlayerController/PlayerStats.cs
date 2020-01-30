using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int currentXp;
    public int currentLvl;
    public int[] xpArray;

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
        }
    }

    public void addXp(int xpToAdd)
    {
        currentXp += xpToAdd;
    }
}
