using FishNet.Object;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class LaserPointer : NetworkBehaviour
{
    private RaycastHit hit;
    public Transform hitOutPosition;
    public Camera cam;
    public LineRenderer lineRender;
    public static LaserPointer instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        cam = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {
        if (hitOutPosition == null)
        {
            return;
        }
        else
        {


            if (CursorManager.instance.stopPlayer == true)
            {
                if (Mouse.current.rightButton.wasPressedThisFrame)
                {
                    PointRay(true, hitOutPosition.position);
                }
                if (Mouse.current.rightButton.wasReleasedThisFrame)
                {
                    PointRay(false, hitOutPosition.position);
                }
            }
            else
            {
                PointRay(false, hitOutPosition.position);
            }
        }
    }

    void PointRay(bool showRay, Vector3 originPoint)
    {
        DrawRayOnServer(showRay, originPoint);
    }

    [ServerRpc(RequireOwnership = false)]
    void DrawRayOnServer(bool showRay, Vector3 originPoint)
    {
        DrawRayOnObserver(showRay,originPoint);
    }

    [ObserversRpc(ExcludeOwner = false, BufferLast = false)]
    void DrawRayOnObserver(bool showRay,Vector3 originPoint)
    {

        if (showRay)
        {
            lineRender.gameObject.SetActive(showRay);
            Vector3 mouse = Input.mousePosition;
            Ray castPoint = cam.ScreenPointToRay(mouse);
            RaycastHit hit;
            if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
            {
                lineRender.SetPosition(0, originPoint);
                lineRender.SetPosition(1, hit.point);
            }
        }

        else 
        {
           lineRender.gameObject.SetActive (false);
        }
    }

    public void TakeOwner(Transform originPoint)
    {
        hitOutPosition = originPoint;
    }
}



