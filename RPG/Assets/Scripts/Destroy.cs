﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0)){
            this.gameObject.SetActive(true);
            
        }else{
            this.gameObject.SetActive(false);
        }
    }
}
