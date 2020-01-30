using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public Slider slider;
    public Text textHP;
    public Text textXP;
    public Text textLvl;

    public PLayerHealthController pLayerHealth;
    private static bool UIExists;

    private PlayerStats thePlayer;
    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerStats>();

        if (!UIExists)
        {
            UIExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);


        }
    }

    // Update is called once per frame
    void Update()
    {
        slider.maxValue = pLayerHealth.playerMaxHealth;
        slider.value = pLayerHealth.playerCurrentHealth;
        textHP.text = "" + pLayerHealth.playerCurrentHealth + "/" + pLayerHealth.playerMaxHealth;

        textXP.text = "" + thePlayer.currentXp + "/" + thePlayer.xpArray[thePlayer.currentLvl + 1 ];
        textLvl.text = "" + thePlayer.currentLvl;
        
    }
}
