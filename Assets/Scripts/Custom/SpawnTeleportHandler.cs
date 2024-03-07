using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTeleportHandler : MonoBehaviour
{
    public Transform[] spawnPosition;    // Start is called before the first frame update
    void Start()
    {
        int randomIndex = Random.Range(0, spawnPosition.Length);
        Transform _RandomTransform = spawnPosition[randomIndex];
        
        this.transform.position = _RandomTransform.position;
    }

}
