using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowEvent : MonoBehaviour
{

    void OnEnable()
    {
         DynamicArrows.onDestinationActive?.Invoke(this.gameObject);
    }
    void OnDisable()
    {
         DynamicArrows.onDestinationDeactive?.Invoke(this.gameObject);

    }
}
