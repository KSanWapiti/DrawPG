using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartPoint : MonoBehaviour
{

    private PlayerController thePlayer;
    private CameraController theCamera;
    public Vector2 face;
    public string PointName;
    // Start is called before the first frame update
    void Start()
    {
        

        thePlayer = FindObjectOfType<PlayerController>();

        if(thePlayer.StartPoint == PointName){

            thePlayer.transform.position = transform.position;
            thePlayer.lastMove = face;
            theCamera = FindObjectOfType<CameraController>();
            theCamera.transform.position = new Vector3(transform.position.x, transform.position.y, theCamera.transform.position.z);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
