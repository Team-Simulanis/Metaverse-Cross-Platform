using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class follow : MonoBehaviour
{
    [SerializeField] Transform hand, target;
    [SerializeField] Vector3 rotation, position;
    [SerializeField] bool takeRotation ;

    private void Start()
    {
        
    }
    void Update()
    {
        followPosition();
        if (takeRotation) { followRotation(); }
    }
    public void takeTarget(Transform Target)
    {
        target = Target;
    }
    void followRotation()
    {
        target.rotation = hand.rotation;
        target.rotation *= Quaternion.Euler(rotation.x,rotation.y,rotation.z);
    }
    void followPosition()
    {
        target.position = hand.position;
        target.position += new Vector3(position.x,position.y,position.z);
    }
}
