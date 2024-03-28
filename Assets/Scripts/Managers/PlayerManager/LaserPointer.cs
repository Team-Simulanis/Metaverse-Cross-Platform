using FishNet.Object;
using LeTai.TrueShadow;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class LaserPointer : NetworkBehaviour
{
    //private RaycastHit hit;
    public Transform hitOutPosition;
    public Camera cam;
    public LineRenderer lineRender;
    public static LaserPointer instance;
    public GameObject syncedEndObject;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    private void OnEnable()
    {
        lineRender.enabled = true;
        syncedEndObject.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        //PointRay();
    }
    public void DrawRay(bool toggleOnOff)
    {
        PointRay(toggleOnOff);
    }

    [ServerRpc(RequireOwnership = false)]
    void DrawRayOnServer(bool toggleOnOf)
    {
        PointRay(toggleOnOf);
    }

    [ObserversRpc(ExcludeOwner = false, BufferLast = false)]
    void PointRay(bool toggleOnOff)
    {
        if(toggleOnOff)
        {
            lineRender.enabled = true;
            syncedEndObject.SetActive(false);
            Vector3 hitpoint = this.transform.position;
            Vector3 originPoint;
            lineRender.gameObject.SetActive(true);
            Vector3 mouse = Input.mousePosition;
            Ray castPoint = cam.ScreenPointToRay(mouse);
            RaycastHit hit;
            if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
            {
                originPoint = hitOutPosition.transform.position;
                hitpoint = hit.point;
            }
            syncedEndObject.transform.position = new Vector3(hitpoint.x, hitpoint.y, hitpoint.z);

            //DrawRayOnServer(true,originPoint,hitpoint);

            lineRender.positionCount = 2;
            lineRender.SetPosition(0, hitOutPosition.transform.position);
            lineRender.SetPosition(1, hitpoint);
        }

        else if (!toggleOnOff)
        {
            lineRender.enabled = false;
            syncedEndObject.SetActive(false);
        }
    }

    public void TakeOwner(LineRenderer _lineRenderer,GameObject _pointer)
    {
        lineRender = _lineRenderer;
        syncedEndObject = _pointer;
    }
    private void OnDisable()
    {
        lineRender.enabled = false;
        syncedEndObject.SetActive(false);
    }





    //public Transform objectToMove;
    /*    void Update()
        {
            Vector3 mouse = Input.mousePosition;
            Ray castPoint = Camera.main.ScreenPointToRay(mouse);
            RaycastHit hit;
            if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
            {
                objectToMove.transform.position = hit.point;
            }
        }*/
}



