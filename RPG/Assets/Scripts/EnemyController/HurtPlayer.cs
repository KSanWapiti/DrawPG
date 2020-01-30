using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    public int damage;
    public GameObject damageDisplay;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Player")
        {
            /*
            Destroy(other.gameObject);
            other.gameObject.SetActive(false);
            enemyHealth = other.gameObject.GetComponent<PLayerHealthController>(playerHea)
            reloading = true;
            thePlayer = other.gameObject;
            */
            

            other.gameObject.GetComponent<PLayerHealthController>().HurtPlayer(damage);
            other.gameObject.GetComponent<PlayerController>().playerHurtEffect();


            //damageUIPosition = new Vector3(hitPoint.position.x, hitPoint.position.y + 1, hitPoint.position.z);
            var clone = (GameObject)Instantiate(damageDisplay, other.transform.position , Quaternion.Euler(Vector3.zero));
            clone.GetComponent<damageDisplay>().setDamageToDisplay(damage);



        }
    }
}
