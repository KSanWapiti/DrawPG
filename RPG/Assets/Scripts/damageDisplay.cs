using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using UnityEngine.UI;

public class damageDisplay : MonoBehaviour
{
    public float moveSpeed;
    public Text textDisplay;
    public float damagetoDisplay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        textDisplay.text = "" + damagetoDisplay;
        transform.position = new Vector3(transform.position.x, transform.position.y + (moveSpeed * Time.deltaTime), transform.position.z);
        
    }

    public void setDamageToDisplay( int damage)
    {
        damagetoDisplay = damage;
    }
}
