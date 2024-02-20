using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendMessage : MonoBehaviour
{
    [SerializeField] bool send;

    private void OnValidate()
    {
/*        if (send)
        {
            SendText.instance.settrue();
            send = false;
        }*/
    }
}
