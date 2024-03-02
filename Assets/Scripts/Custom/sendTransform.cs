using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sendTransform : MonoBehaviour
{
    public static Transform thisTransform;
    void FixedUpdate()
    {
        thisTransform = this.transform;
    }
}
