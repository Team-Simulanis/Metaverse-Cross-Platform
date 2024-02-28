using FishNet.Transporting.Bayou;
using FishNet.Transporting.Multipass;
using FishNet.Transporting.Tugboat;
using FishNet.Transporting.Bayou;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[RequireComponent(typeof(Multipass))]
public class transportsetter : MonoBehaviour
{
    [ReadOnly] public Multipass Multipass;
    void Start()
    {
        Multipass = GetComponent<Multipass>();
#if UNITY_WEBGL
        Multipass.SetClientTransport<Bayou>();
#else
        Multipass.SetClientTransport<Tugboat>();
#endif
    }
}
