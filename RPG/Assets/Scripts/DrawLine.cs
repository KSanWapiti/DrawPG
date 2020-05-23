using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{

    public GameObject linePrefab;
    public GameObject currentLine;

    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCollider;

    public List<Vector2> fingerPositions;
    public Transform Camera;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CreateLine();
        }
        if (Input.GetMouseButton(0))
        {
            Vector2 CameraPosition= Camera.position;
            float Xmilieu= Screen.width/2;
            float Ymilieu= Screen.height/2;
            Vector2 tempFingerPos = new Vector2 (CameraPosition.x+(Input.mousePosition.x-Xmilieu)/Screen.width*25,CameraPosition.y+(Input.mousePosition.y-Ymilieu)/Screen.height*11.4f);
            if (Vector2.Distance(tempFingerPos, fingerPositions[fingerPositions.Count - 1]) > .1f)
            {
                UpdateLine(tempFingerPos);
            }

        }
    }


    void CreateLine()
    {
        Vector2 CameraPosition= Camera.position;
        float Xmilieu= Screen.width/2;
        float Ymilieu= Screen.height/2;
        currentLine = Instantiate(linePrefab, new Vector2 (CameraPosition.x+(Input.mousePosition.x-Xmilieu)/Screen.width*25,CameraPosition.y+(Input.mousePosition.y-Ymilieu)/Screen.height*11.4f), Quaternion.identity);
        lineRenderer = currentLine.GetComponent<LineRenderer>();
        fingerPositions.Clear();
        fingerPositions.Add(new Vector2 (CameraPosition.x+(Input.mousePosition.x-Xmilieu)/Screen.width*25,CameraPosition.y+(Input.mousePosition.y-Ymilieu)/Screen.height*11.4f));

        lineRenderer.SetPosition(0, fingerPositions[0]);
        lineRenderer.SetPosition(1, fingerPositions[0]);
    }


    void UpdateLine(Vector2 newFingerPos)
    {
        fingerPositions.Add(newFingerPos);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, newFingerPos);


    }

}