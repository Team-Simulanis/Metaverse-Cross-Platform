using System.Collections;
using System.Collections.Generic;
using Invector.vCamera;
using Unity.VisualScripting;
using UnityEngine;

public class MiniMapCam : MonoBehaviour
{
    void FixedUpdate()
    {
        if(Camera.main == null) return;
        var target = Camera.main.gameObject.transform.parent.GetComponent<vThirdPersonCamera>().mainTarget.transform;
        var pos = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
        transform.position = pos;
    }
}