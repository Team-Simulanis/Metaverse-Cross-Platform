using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
  

    private void FixedUpdate()
    {
        if (Camera.main)
            transform.LookAt(Camera.main.transform, Vector3.up);
    }
}