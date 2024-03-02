using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Doozy.Runtime.Colors.Models.RGB;

public class Locater : MonoBehaviour
{
    public Transform[] ChildGameobjects;
    void Start()
    {
        LoadAndInstantiatePrefab();
    }

   
    void LoadAndInstantiatePrefab()
    {
        Transform[] ts = GetComponentsInChildren<Transform>();
        for (int i = 0; i <= ts.Length; i++)
        {
          if (ts[i].name.Contains("Slot"))
            {
                string[] names = ts[i].name.Split('_');
                Debug.Log(names[1]);
                string ObjName = names[1];
                GameObject obj = Resources.Load<GameObject>(ObjName);
                GameObject activeObj = Instantiate(obj, ts[i]);

            }


        }
    }
}
