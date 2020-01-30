using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{
    public float timeDestroy;

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        timeDestroy -= Time.deltaTime;
        if(timeDestroy <= 0)
        {
            Destroy(gameObject);
        }
    }
}
