using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class LaserPointer : MonoBehaviour
{
    private RaycastHit hit;
    public Transform hitOutPosition;
    public Camera cam;
    public LineRenderer lineRender;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        lineRender = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
            DrawRayTowardPoint(true);
    }

    void DrawRayTowardPoint(bool showRay)
    {

        if (showRay)
        {
            lineRender.gameObject.SetActive(showRay);
            Vector3 mouse = Input.mousePosition;
            Ray castPoint = cam.ScreenPointToRay(mouse);
            RaycastHit hit;
            if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
            {
                lineRender.SetPosition(0, lineRender.transform.position);
                lineRender.SetPosition(1, hit.point);
            }
        }

        else 
        {
           
        }


    }
}



